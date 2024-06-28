using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Services;

namespace TestServer;

public class OurCityWebApplicationFactory<TEntryPoint> : WebApplicationFactory<Program>
    where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // builder.UseEnvironment("LocalIntegrationTest");
        builder.ConfigureServices(services =>
        {
            ServiceDescriptor descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)
            )!;
            services.Remove(descriptor);

            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration["ConnectionStrings:DatabaseIntegrationTesting"]);
            });

            ServiceProvider sp = services.BuildServiceProvider();
            using IServiceScope scope = sp.CreateScope();
            IServiceProvider scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<ApplicationDbContext>();
            var seederService = scopedServices.GetRequiredService<ISeederService>();

            try
            {
                var seeder = new Seeder(context, seederService);

                Console.WriteLine("Delete database ...");
                seeder.EnsureDBDeleted();

                Console.WriteLine("Apply database migrations ...");
                seeder.MigrateDB();

                Console.WriteLine("Ensure database is created ...");
                seeder.EnsureDBCreated();

                Console.WriteLine("Insert test data into database ...");
                seeder.SeedTestData().Wait();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error during initialization of Database! Error: {exception.Message}!");
            }
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseContentRoot(Directory.GetCurrentDirectory());
        return base.CreateHost(builder);
    }
}

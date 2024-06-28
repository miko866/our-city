using System.Threading.RateLimiting;
using NLog;
using Npgsql;
using Server.Utils;

namespace Server.Config;

public static class ServiceExtensionConfig
{
    #region Public

    /// <summary>
    /// Test connection to DB per Entity Framework Core
    /// If no connection is established throw error
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="environmentName"></param>
    /// <param name="logger"></param>
    /// <exception cref="Exception">No connection to DB</exception>
    public static void TestConnectionEf(
        this IServiceCollection services,
        IConfiguration configuration,
        string environmentName,
        Logger logger
    )
    {
        string connectionString = configuration["ConnectionStrings:Database"]!;
        string connectionStringIntegrationTest = configuration["ConnectionStrings:DatabaseIntegrationTesting"]!;
        using var connection = new NpgsqlConnection(
            environmentName.Equals(Helpers.Constants.Environments.LocalIntegrationTest)
                ? connectionStringIntegrationTest
                : connectionString
        );
        try
        {
            connection.Open();

            switch (connection.State)
            {
                case System.Data.ConnectionState.Open when ShowConnectionString(environmentName):
                    Console.WriteLine(
                        @"INFO: ConnectionString: "
                            + connection.ConnectionString
                            + "\n DataBase: "
                            + connection.Database
                            + "\n DataSource: "
                            + connection.DataSource
                            + "\n ServerVersion: "
                            + connection.ServerVersion
                            + "\n TimeOut: "
                            + connection.ConnectionTimeout
                    );
                    break;
                case System.Data.ConnectionState.Open:
                    Console.WriteLine(
                        @"INFO: DataBase: "
                            + connection.Database
                            + "\n DataSource: "
                            + connection.DataSource
                            + "\n ServerVersion: "
                            + connection.ServerVersion
                            + "\n TimeOut: "
                            + connection.ConnectionTimeout
                    );
                    break;
            }
        }
        catch
        {
            logger.Error("Error: =========== No DB connection! ===========");
        }
    }

    /// <summary>
    /// Rate Limiter
    /// https://blog.maartenballiauw.be/post/2022/09/26/aspnet-core-rate-limiting-middleware.html
    /// </summary>
    /// <param name="services"></param>
    public static void RateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            // 429 Too Many Requests
            options.RejectionStatusCode = 429;
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 1000,
                        QueueLimit = 10,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromMinutes(1)
                    }
                )
            );
        });
    }

    #endregion Public

    #region Private

    /// <summary>
    /// Show Connection String only for some enviroments
    /// </summary>
    /// <param name="environmentName"></param>
    /// <returns></returns>
    private static bool ShowConnectionString(string environmentName)
    {
        return EnvironmentUtil.AllowDebugForEnvironments(environmentName);
    }

    #endregion Private
}

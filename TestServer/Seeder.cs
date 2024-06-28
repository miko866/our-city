using Data;
using Microsoft.EntityFrameworkCore;
using Server.Services;

namespace TestServer;

public class Seeder
{
    private readonly ApplicationDbContext _context;
    private readonly ISeederService _seederService;

    public Seeder(ApplicationDbContext context, ISeederService seederService)
    {
        _context = context;
        _seederService = seederService;
    }

    public void EnsureDBDeleted()
    {
        _context.Database.EnsureDeleted();
    }

    public void EnsureDBCreated()
    {
        _context.Database.EnsureCreated();
    }

    public void MigrateDB()
    {
        _context.Database.Migrate();
    }

    public async Task SeedTestData()
    {
        Console.WriteLine("Start seeding database ...");
        await _seederService.SeedDb(default);
    }
}

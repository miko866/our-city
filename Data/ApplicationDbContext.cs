using Data.Entities;
using Data.Models.Enums;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data;

/// <summary>
/// ApplicationDbContext
/// Base Application Context for EFCore
/// Inject Net. Identity for User
/// </summary>
public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>,
        IDataProtectionKeyContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ApplicationRole>? ApplicationRole { get; set; }
    public DbSet<ApplicationUser>? ApplicationUser { get; set; }
    public DbSet<Country>? Country { get; set; }
    public DbSet<District>? District { get; set; }
    public DbSet<FileItem>? FileItem { get; set; }
    public DbSet<FileItemType>? FileItemType { get; set; }
    public DbSet<Logs>? Logs { get; set; }
    public DbSet<Message>? Message { get; set; }
    public DbSet<MetaData>? MetaData { get; set; }
    public DbSet<ModuleEvent>? ModuleEvent { get; set; }
    public DbSet<ModuleEventFileItem>? ModuleEventFileItem { get; set; }
    public DbSet<ModuleEventTag>? ModuleEventTag { get; set; }
    public DbSet<ModuleMunicipalRadio>? ModuleMunicipalRadio { get; set; }
    public DbSet<ModuleMunicipalRadioMessage>? ModuleMunicipalRadioMessage { get; set; }
    public DbSet<ModuleNews>? ModuleNews { get; set; }
    public DbSet<ModuleNewsFileItem>? ModuleNewsFileItem { get; set; }
    public DbSet<ModuleNewsMetaData>? ModuleNewsMetaData { get; set; }
    public DbSet<ModuleNewsTag>? ModuleNewsTag { get; set; }
    public DbSet<ModuleService>? ModuleService { get; set; }
    public DbSet<ModuleSimplePage>? ModuleSimplePage { get; set; }
    public DbSet<ModuleSimplePageFileItem>? ModuleSimplePageFileItem { get; set; }
    public DbSet<ModuleSpecialAnnouncement>? ModuleSpecialAnnouncement { get; set; }
    public DbSet<Organisation>? Organisation { get; set; }
    public DbSet<OrganisationFileItem>? OrganisationFileItem { get; set; }
    public DbSet<OrganisationModuleService>? OrganisationModuleService { get; set; }
    public DbSet<Permission>? Permission { get; set; }
    public DbSet<State>? State { get; set; }
    public DbSet<Tag>? Tag { get; set; }
    public DbSet<UserFileItem>? UserFileItem { get; set; }
    public DbSet<UserOrganisation>? UserOrganisation { get; set; }
    public DbSet<UserOrganisationPermission>? UserOrganisationPermission { get; set; }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Allow use and overwrite ASP.NET.Identity Tables
        base.OnModelCreating(modelBuilder);

        // Convert enum to string
        modelBuilder
            .Entity<FileItemType>()
            .Property(t => t.Name)
            .HasConversion(new EnumToStringConverter<Enums.EnumFileType>());

        modelBuilder
            .Entity<ModuleSpecialAnnouncement>()
            .Property(t => t.Severity)
            .HasConversion(new EnumToStringConverter<Enums.EnumSeverity>());

        /*
         * For PostgreSql
         * https://medium.com/@aspram.shadyan.dev/identityserver4-ef-core-naming-conventions-adapted-for-postgresql-29a138bd26bb
         * See also ModelBuilderSnakeCaseExtension.cs
         */
        modelBuilder.ConvertToSnakeCase();
    }
}

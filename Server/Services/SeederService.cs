using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Helpers;
using Server.Utils;
using Constants = Shared.Helpers.Constants;
using ErrorCodes = Shared.Helpers.ErrorCodes;
using Path = System.IO.Path;

namespace Server.Services;

#region Interface

public interface ISeederService
{
    Task<string> SeedDb(CancellationToken cancellationToken);
}

#endregion Interface

#region Implementation

public class SeederService : ISeederService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SeederService> _logger;
    private readonly IErrorMessages _errorMessages;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly string _seederFilePath;
    private readonly string _seederFilePathLocalIntegrationTest;

    /// <summary>
    /// SeederService Constructor
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="hostEnvironment"></param>
    /// <param name="logger"></param>
    /// <param name="errorMessages"></param>
    public SeederService(
        IServiceProvider serviceProvider,
        IHostEnvironment hostEnvironment,
        ILogger<SeederService> logger,
        IErrorMessages errorMessages
    )
    {
        _serviceProvider = serviceProvider;
        _hostEnvironment = hostEnvironment;
        _logger = logger;
        _errorMessages = errorMessages;
        _userManager = GetUserManager();
        _roleManager = GetRoleManager();
        _context = GetApplicationDbContext();

        _seederFilePath = "../sql/seeders/";
        _seederFilePathLocalIntegrationTest = "../../../../sql/seeders/";
    }

    #region Public

    public async Task<string> SeedDb(CancellationToken cancellationToken)
    {
        if (!EnvironmentUtil.AllowDebugForEnvironments(_hostEnvironment.EnvironmentName))
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_CANNOT_RUN_ON_PRODUCTION())
                    .SetCode(ErrorCodes.CODE_SEEDER_ERROR_CANNOT_RUN_ON_PRODUCTION)
                    .Build()
            );
        }

        cancellationToken.ThrowIfCancellationRequested();

        await DeleteFromSql();

        await CreateDefaultRolesForDevelopment().ConfigureAwait(false);
        await CreateDefaultUsersForDevelopment().ConfigureAwait(false);

        await RealDataSeedFromSql();
        await MockDataSeedFromSql();

        _logger.LogInformation(
            "INFORMATION in CreateDefaultUsersAndSeed - Default Users have been created and Development Data is seeded"
        );
        return ("Default Users have been created and Development Data is seeded");
    }

    #endregion Public

    #region Private

    /// <summary>
    /// MockDataSeedFromSQL
    /// Seed dummy data to DB only for development
    /// </summary>
    private Task MockDataSeedFromSql()
    {
        string filepath = _hostEnvironment.EnvironmentName.Equals(Helpers.Constants.Environments.LocalIntegrationTest)
            ? $"{_seederFilePathLocalIntegrationTest}mock_data_seed.sql"
            : $"{_seederFilePath}mock_data_seed.sql";

        string fullPath = Path.GetFullPath(filepath);
        _logger.LogInformation(
            "INFORMATION in MockDataSeedFromSQL - Import Mock Data SQL file from: {FullPath}",
            fullPath
        );
        string sql = File.ReadAllText(fullPath);
        sql = sql.Replace("{}", "{{}}"); // Escape braces so that we still have valid SQL, but can still execute it here.
        _context.Database.ExecuteSqlRaw(sql);
        _logger.LogInformation("INFORMATION in MockDataSeedFromSQL - SQL dev mock data seed done, all done");
        return Task.CompletedTask;
    }

    /// <summary>
    /// RealDataSeedFromSQL
    /// Seed production data to DB
    /// </summary>
    private Task RealDataSeedFromSql()
    {
        string filepath = _hostEnvironment.EnvironmentName.Equals(Helpers.Constants.Environments.LocalIntegrationTest)
            ? $"{_seederFilePathLocalIntegrationTest}real_data_seed.sql"
            : $"{_seederFilePath}real_data_seed.sql";

        string fullPath = Path.GetFullPath(filepath);

        _logger.LogInformation(
            "INFORMATION in RealDataSeedFromSQL - Import Real Data SQL file from: {FullPath}",
            fullPath
        );
        string sql = File.ReadAllText(fullPath);
        sql = sql.Replace("{}", "{{}}"); // Escape braces so that we still have valid SQL, but can still execute it here.
        _context.Database.ExecuteSqlRaw(sql);
        _logger.LogInformation("INFORMATION in RealDataSeedFromSQL - SQL dev real data seed done, all done");
        return Task.CompletedTask;
    }

    /// <summary>
    /// DeleteFromSQL
    /// Delete DB only for development
    /// </summary>
    private Task DeleteFromSql()
    {
        string filepath = _hostEnvironment.EnvironmentName.Equals(Helpers.Constants.Environments.LocalIntegrationTest)
            ? $"{_seederFilePathLocalIntegrationTest}truncate_all.sql"
            : $"{_seederFilePath}truncate_all.sql";

        string fullPath = Path.GetFullPath(filepath);

        _logger.LogInformation("INFORMATION in DeleteFromSQL: {FullPath}", fullPath);

        string sql = File.ReadAllText(fullPath);
        sql = sql.Replace("{}", "{{}}"); // Escape braces so that we still have valid SQL, but can still execute it here.
        _context.Database.ExecuteSqlRaw(sql);
        _logger.LogInformation("INFORMATION in SQL delete done");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Create default roles
    /// </summary>
    private async Task CreateDefaultRolesForDevelopment()
    {
        // Make roles
        List<string> rolesDetails = [Constants.Roles.Admin, Constants.Roles.User];

        foreach (string roleName in rolesDetails)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
                continue;
            var myRole = new ApplicationRole { Name = roleName, NormalizedName = roleName.ToUpper() };
            await _roleManager.CreateAsync(myRole).ConfigureAwait(false);
        }
    }

    /// <summary>
    ///  Create default users
    /// </summary>
    /// <returns></returns>
    private async Task CreateDefaultUsersForDevelopment()
    {
        List<ApplicationUser> seedUsers =
        [
            new ApplicationUser
            {
                FirstName = "SysAdmin",
                LastName = "User",
                Email = "communityappourcity+sysadmin@gmail.com",
                UserName = "SysAdminUser",
                EmailConfirmed = true,
                Gender = Constants.Gender.Male,
                CreatedAt = DateTime.Now,
                CreatedBy = "OurCity"
            },
            new ApplicationUser
            {
                FirstName = "OrganisationAdminMoravany",
                LastName = "User",
                Email = "communityappourcity+adminmoravany@gmail.com",
                UserName = "OrganisationAdminMoravanyUser",
                EmailConfirmed = true,
                Gender = Constants.Gender.Male,
                CreatedAt = DateTime.Now,
                CreatedBy = "OurCity"
            },
            new ApplicationUser
            {
                FirstName = "OrganisationEditorMoravany",
                LastName = "User",
                Email = "communityappourcity+orgeditormoravany@gmail.com",
                UserName = "OrganisationEditorMoravanyUser",
                EmailConfirmed = true,
                Gender = Constants.Gender.Female,
                CreatedAt = DateTime.Now,
                CreatedBy = "OurCity"
            },
            new ApplicationUser
            {
                FirstName = "OrganisationAdminTO",
                LastName = "User",
                Email = "communityappourcity+adminto@gmail.com",
                UserName = "OrganisationAdminTOUser",
                EmailConfirmed = true,
                Gender = Constants.Gender.Male,
                CreatedAt = DateTime.Now,
                CreatedBy = "OurCity"
            },
            new ApplicationUser
            {
                FirstName = "OrganisationEditorTO",
                LastName = "User",
                Email = "communityappourcity+orgeditorto@gmail.com",
                UserName = "OrganisationEditorTOUser",
                EmailConfirmed = true,
                Gender = Constants.Gender.Female,
                CreatedAt = DateTime.Now,
                CreatedBy = "OurCity"
            },
            new ApplicationUser
            {
                FirstName = "OrganisationAdminNovoť",
                LastName = "User",
                Email = "communityappourcity+orgadminnovot@gmail.com",
                UserName = "OrganisationAdminNovotUser",
                EmailConfirmed = true,
                Gender = Constants.Gender.Male,
                CreatedAt = DateTime.Now,
                CreatedBy = "OurCity"
            },
            new ApplicationUser
            {
                FirstName = "TestAllAdmin",
                LastName = "User",
                Email = "communityappourcity+useralladmin@gmail.com",
                UserName = "TestUserAllAdmin",
                EmailConfirmed = true,
                Gender = Constants.Gender.Female,
                CreatedAt = DateTime.Now,
                CreatedBy = "OurCity"
            },
            new ApplicationUser
            {
                FirstName = "TestAllEditor",
                LastName = "User",
                Email = "communityappourcity+useralleditor@gmail.com",
                UserName = "TestUserAllEditor",
                EmailConfirmed = true,
                Gender = Constants.Gender.Female,
                CreatedAt = DateTime.Now,
                CreatedBy = "OurCity"
            }
        ];

        // Add Password and Role to users
        foreach (ApplicationUser myUser in seedUsers.Where(myUser => myUser.Email != null))
        {
            ApplicationUser? userExists = await _userManager.FindByEmailAsync(myUser.Email!).ConfigureAwait(false);
            if (userExists != null)
                continue;
            await _userManager.CreateAsync(myUser).ConfigureAwait(false);
            await _userManager.AddPasswordAsync(myUser, "Substring").ConfigureAwait(false);
            switch (myUser.UserName)
            {
                case "SysAdminUser":
                    await _userManager.AddToRoleAsync(myUser, Constants.Roles.User).ConfigureAwait(false);
                    await _userManager.AddToRoleAsync(myUser, Constants.Roles.Admin).ConfigureAwait(false);
                    break;
                default:
                    await _userManager.AddToRoleAsync(myUser, Constants.Roles.User).ConfigureAwait(false);
                    break;
            }
        }
    }

    private ApplicationDbContext GetApplicationDbContext()
    {
        return (ApplicationDbContext)_serviceProvider.GetService(typeof(ApplicationDbContext))!;
    }

    private UserManager<ApplicationUser> GetUserManager()
    {
        return (UserManager<ApplicationUser>)_serviceProvider.GetService(typeof(UserManager<ApplicationUser>))!;
    }

    private RoleManager<ApplicationRole> GetRoleManager()
    {
        return (RoleManager<ApplicationRole>)_serviceProvider.GetService(typeof(RoleManager<ApplicationRole>))!;
    }

    #endregion Private
}

#endregion Implementation

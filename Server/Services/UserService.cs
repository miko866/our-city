using System.Reflection;
using System.Security.Claims;
using System.Text.RegularExpressions;
using AutoMapper;
using Data;
using Data.Entities;
using FluentValidation.Results;
using HotChocolate.Resolvers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Helpers;
using Server.Models.JwtToken;
using Server.Security;
using Shared.Extensions;
using Shared.InputModels.User;
using Shared.Validators.UserValidator;
using Constants = Server.Helpers.Constants;
using ErrorCodes = Shared.Helpers.ErrorCodes;

namespace Server.Services;

#region Interface
public interface IUserService
{
    Task<string> AuthenticateUser(LoginDetailsInputModel loginDetailsInput, CancellationToken cancellationToken);

    Task<ApplicationUser> CreateUser(ApplicationUserInputModel data, CancellationToken cancellationToken);

    Task<ApplicationUser> GetCurrentUser(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken);
    Task<IEnumerable<ApplicationUser>> GetUsers(IResolverContext resolverContext, CancellationToken cancellationToken);

    Task<ApplicationUser> UpdateUser(
        ApplicationUserInputModel data,
        string applicationUserId,
        bool isAdmin,
        CancellationToken cancellationToken
    );
    Task<bool> SoftDeleteUser(string id, bool isDeleted, CancellationToken cancellationToken);
}
#endregion Interface

public partial class UserService : IUserService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly IErrorMessages _errorMessages;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    private ApplicationDbContext GetApplicationContext()
    {
        return (ApplicationDbContext)_serviceProvider.GetService(typeof(ApplicationDbContext))!;
    }

    private UserManager<ApplicationUser> GetUserManager()
    {
        return (UserManager<ApplicationUser>)_serviceProvider.GetService(typeof(UserManager<ApplicationUser>))!;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="signInManager"></param>
    /// <param name="jwtGenerator"></param>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    /// <param name="errorMessages"></param>
    public UserService(
        IServiceProvider serviceProvider,
        SignInManager<ApplicationUser> signInManager,
        IJwtGenerator jwtGenerator,
        IConfiguration configuration,
        ILogger<UserService> logger,
        IMapper mapper,
        IErrorMessages errorMessages
    )
    {
        _serviceProvider = serviceProvider;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _configuration = configuration;
        _mapper = mapper;
        _errorMessages = errorMessages;
        _logger = logger;
        _userManager = GetUserManager();
        _context = GetApplicationContext();
    }

    #region Implementation

    #region Public methods

    /// <summary>
    /// Login for user
    /// </summary>
    /// <param name="data"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<string> AuthenticateUser(LoginDetailsInputModel data, CancellationToken cancellationToken)
    {
        var validator = new AuthUserValidator();
        ValidationResult results = await validator.ValidateAsync(data, cancellationToken);
        if (!results.IsValid)
        {
            string errorMessage = results.Errors.Aggregate("", (current, error) => current + error.ErrorMessage);
            string errorCode = results.Errors.Aggregate("", (current, error) => current + error.ErrorCode);

            throw new GraphQLException(ErrorBuilder.New().SetMessage(errorMessage).SetCode(errorCode).Build());
        }

        string returnValue;

        ApplicationUser? user;
        // Check user exist in system or not
        if (string.IsNullOrEmpty(data.Email))
        {
            user = await _userManager.FindByNameAsync(data.UserName!).ConfigureAwait(false);
        }
        else if (string.IsNullOrEmpty(data.UserName))
        {
            user = await _userManager.FindByEmailAsync(data.Email).ConfigureAwait(false);
        }
        else
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_AUTHENTICATION())
                    .SetCode(ErrorCodes.CODE_AUTHORIZATION_ERROR_USED_EMAIL_USERNAME)
                    .Build()
            );
        }

        if (user.IsNull())
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_AUTHENTICATION())
                    .SetCode(ErrorCodes.CODE_AUTHORIZATION_ERROR_CANNOT_LOGGIN)
                    .Build()
            );
        }

        if (user!.IsDeleted)
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_AUTHENTICATION())
                    .SetCode(ErrorCodes.CODE_AUTHORIZATION_ERROR_USER_DELETED)
                    .Build()
            );
        }

        if (!await _userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false))
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_USER_EMAIL_NOT_CONFIRMED())
                    .SetCode(ErrorCodes.CODE_AUTHORIZATION_ERROR_NOT_CONFIRMED_EMAIL)
                    .Build()
            );
        }

        // Perform login operation
        SignInResult signInResult = await _signInManager
            .CheckPasswordSignInAsync(user, data.Password!, true)
            .ConfigureAwait(false);
        if (signInResult.Succeeded)
        {
            if (!user.UserName!.Equals("SysAdminUser"))
            {
                ICollection<UserOrganisation> userOrganisations = await _context
                    .UserOrganisation!.Where(x => x.ApplicationUserId == user.Id)
                    .Include(x => x.UserOrganisationPermissions)
                    .ThenInclude(x => x.Permission)
                    .Include(x => x.Organisation)
                    .AsSplitQuery()
                    .ToListAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                var tokenExtensionModel = new TokenExtensionModel
                {
                    Organisations = new List<TokenOrganisationModel>()
                };

                foreach (UserOrganisation organisation in userOrganisations)
                {
                    var tokenOrganisationModel = new TokenOrganisationModel()
                    {
                        OrganisationId = organisation.OrganisationId,
                        OrganisationName = organisation.Organisation.Name,
                        PrimaryStatus = organisation.IsPrimary,
                        Permissions = organisation
                            .UserOrganisationPermissions.Select(x => new TokenPermissionModel()
                            {
                                PermissionId = x.Permission.Id,
                                PermissionName = x.Permission.Name
                            })
                            .ToList()
                    };

                    tokenExtensionModel.Organisations.Add(tokenOrganisationModel);
                }

                string token = await _jwtGenerator.GetJwtSecurityToken(user, tokenExtensionModel).ConfigureAwait(false);
                returnValue = token;
            }
            else
            {
                string token = await _jwtGenerator.GetJwtSecurityToken(user).ConfigureAwait(false);
                returnValue = token;
            }
        }
        else
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_AUTHENTICATION())
                    .SetCode(ErrorCodes.CODE_AUTHORIZATION_ERROR_USERNAME_PASSWORD_IS_WRONG)
                    .Build()
            );
        }

        return returnValue;
    }

    /// <summary>
    /// Create new user from admin page
    /// Without Email confirmation
    /// Sys/OrganizationAdmin create password for user and user should change the initial password as soon as possible
    /// </summary>
    /// <param name="data"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ApplicationUser> CreateUser(ApplicationUserInputModel data, CancellationToken cancellationToken)
    {
        var validator = new CreateUserValidator();
        ValidationResult results = await validator.ValidateAsync(data, cancellationToken);
        if (!results.IsValid)
        {
            string errorMessage = results.Errors.Aggregate("", (current, error) => current + error.ErrorMessage);
            string errorCode = results.Errors.Aggregate("", (current, error) => current + error.ErrorCode);

            throw new GraphQLException(ErrorBuilder.New().SetMessage(errorMessage).SetCode(errorCode).Build());
        }

        return await CreateUserInternal(data, true, false);
    }

    /// <summary>
    /// Get current logged user by JWT Token
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ApplicationUser> GetCurrentUser(
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken
    )
    {
        string userId = claimsPrincipal.GetUserGuid();

        if (userId.IsNullOrEmpty())
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_USER_NOT_EXIST())
                    .SetCode(ErrorCodes.CODE_USER_ERROR_NOT_EXIST)
                    .Build()
            );
        }

        ApplicationUser? existingUser = await _context
            .ApplicationUser?.Where(u => u.Id == userId)
            .Include(x => x.UserOrganisations)
            .ThenInclude(x => x.Organisation)
            .Include(x => x.UserOrganisations)
            .ThenInclude(x => x.UserOrganisationPermissions)
            .ThenInclude(x => x.Permission)
            .Include(x => x.UserFileItems)
            .ThenInclude(x => x.FileItem)
            .ThenInclude(x => x.FileItemType)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)!;

        if (existingUser.IsNull())
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_USER_NOT_EXIST())
                    .SetCode(ErrorCodes.CODE_USER_ERROR_NOT_EXIST)
                    .Build()
            );
        }

        // Add roles to user
        ApplicationUser? userEmail = await _userManager.FindByEmailAsync(existingUser?.Email!);
        IList<string> roles = await _userManager.GetRolesAsync(userEmail!);
        existingUser!.Roles = roles.ToList();

        if (roles.Contains(Shared.Helpers.Constants.Roles.Admin))
            return existingUser;

        existingUser.CurrentOrganisationId = existingUser
            .UserOrganisations.Where(x => x.IsPrimary)
            .Select(x => x.OrganisationId)
            .FirstOrDefault();

        // Add permissions to user
        existingUser.CurrentPermissions = existingUser
            .UserOrganisations.Where(x => x.IsPrimary)
            .SelectMany(x => x.UserOrganisationPermissions)
            .Select(x => x.Permission.Name)
            .ToList();

        return existingUser;
    }

    /// <summary>
    /// Get all or some users
    /// </summary>
    /// <param name="resolverContext"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ApplicationUser>> GetUsers(
        IResolverContext resolverContext,
        CancellationToken cancellationToken
    )
    {
        IQueryable<ApplicationUser>? query = _context.ApplicationUser;

        query = query?.Filter(resolverContext).Sort(resolverContext);

        List<ApplicationUser> list = await query!
            .Include(x => x.UserOrganisations)
            .ThenInclude(x => x.Organisation)
            .Include(x => x.UserOrganisations)
            .ThenInclude(x => x.UserOrganisationPermissions)
            .ThenInclude(x => x.Permission)
            .Include(x => x.UserFileItems)
            .ThenInclude(x => x.FileItem)
            .ThenInclude(x => x.FileItemType)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        foreach (ApplicationUser user in list)
        {
            ApplicationUser? userByEmail = await _userManager.FindByEmailAsync(user.Email!);
            IList<string> roles = await _userManager.GetRolesAsync(userByEmail!);
            user.Roles = roles.ToList();

            user.CurrentOrganisationId = user
                .UserOrganisations.Where(x => x.IsPrimary)
                .Select(x => x.OrganisationId)
                .FirstOrDefault();

            // Add permissions to user
            user.CurrentPermissions = user
                .UserOrganisations.Where(x => x.IsPrimary)
                .SelectMany(x => x.UserOrganisationPermissions)
                .Select(x => x.Permission.Name)
                .ToList();
        }

        return list;
    }

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="data"></param>
    /// <param name="applicationUserId"></param>
    /// <param name="isAdmin"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ApplicationUser> UpdateUser(
        ApplicationUserInputModel data,
        string applicationUserId,
        bool isAdmin,
        CancellationToken cancellationToken
    )
    {
        var validator = new UpdateUserValidator();
        ValidationResult results = await validator.ValidateAsync(data!, cancellationToken);
        if (!results.IsValid)
        {
            string errorMessage = results.Errors.Aggregate("", (current, error) => current + error.ErrorMessage);
            string errorCode = results.Errors.Aggregate("", (current, error) => current + error.ErrorCode);

            throw new GraphQLException(ErrorBuilder.New().SetMessage(errorMessage).SetCode(errorCode).Build());
        }

        ApplicationUser user = await UpdateUserInternal(data, applicationUserId, cancellationToken);

        // ADMIN - That is only for update user role
        if (!isAdmin || data.Role!.IsNullOrEmpty())
            return user;

        ApplicationUser? newUser = await _userManager.FindByEmailAsync(user.Email!);
        IList<string> currentRole = await _userManager.GetRolesAsync(newUser!);
        await _userManager.RemoveFromRolesAsync(newUser!, currentRole);
        await _userManager.AddToRoleAsync(newUser!, data.Role!).ConfigureAwait(false);

        return user;
    }

    /// <summary>
    /// SoftDeleteUser - only set isDelete flag
    /// it can be toggled on/off
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isDeleted"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> SoftDeleteUser(string id, bool isDeleted, CancellationToken cancellationToken)
    {
        ApplicationUser? user = await _context
            .ApplicationUser!.FindAsync(new object?[] { id }, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (user == null)
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_USER_NOT_EXIST())
                    .SetCode(ErrorCodes.CODE_USER_ERROR_NOT_EXIST)
                    .Build()
            );
        }

        user.IsDeleted = isDeleted;
        user.UpdatedAt = DateTime.Now;

        try
        {
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            _logger.LogError("SoftDeleteUser: {Exception}", exception);
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_CANNOT_SAVE())
                    .SetCode(ErrorCodes.CODE_ERROR_CANNOT_SAVE)
                    .Build()
            );
        }

        return true;
    }

    #endregion Public methods

    #region Private methods

    /// <summary>
    /// Remove Google email alias
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    private static string RemoveAliases(string email)
    {
        return EmailAliasRegex().Replace(email, "");
    }

    /// <summary>
    /// Check if user exist
    /// </summary>
    /// <param name="email"></param>
    /// <param name="userName"></param>
    /// <param name="userManager"></param>
    /// <returns></returns>
    private static async Task<bool> UserExists(string email, string userName, UserManager<ApplicationUser> userManager)
    {
        return await UserExistsByEmail(email, userManager) && await UserExistsByUserName(userName, userManager);
    }

    /// <summary>
    /// Check if user exist depends on email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="userManager"></param>
    /// <returns></returns>
    private static async Task<bool> UserExistsByEmail(string email, UserManager<ApplicationUser> userManager)
    {
        ApplicationUser? exists = await userManager.FindByEmailAsync(email).ConfigureAwait(false);
        return exists != null;
    }

    /// <summary>
    /// Check if user exist depends on userName
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="userManager"></param>
    /// <returns></returns>
    private static async Task<bool> UserExistsByUserName(string userName, UserManager<ApplicationUser> userManager)
    {
        ApplicationUser? exists = await userManager.FindByNameAsync(userName).ConfigureAwait(false);
        return exists != null;
    }

    /// <summary>
    /// Generate unique user name for user
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private Task<string> GenerateUserName(ApplicationUserInputModel data)
    {
        if (data.FirstName.IsNullOrEmpty() || data.LastName.IsNullOrEmpty())
        {
            string sanitizedEmail = RemoveAliases(data.Email!);
            return Task.FromResult(sanitizedEmail.Split("@")[0].RemoveWhiteSpaces());
        }

        // Store the first possible name.
        string possibleUsername = $"{data.LastName}{data.FirstName![..1]}";

        // Don't hit the database N times, instead get all the possible names in one shot.
        List<ApplicationUser> existingUsers = _context
            .ApplicationUser!.Where(u => u.UserName!.StartsWith(possibleUsername))
            .ToList();

        // Find the first possible open username.
        if (existingUsers.Count <= 0)
            return Task.FromResult(possibleUsername.RemoveWhiteSpaces());
        {
            // Iterate through all the possible usernames and create it when a spot is open.
            for (int i = 1; i < existingUsers.Count; i++)
            {
                if (existingUsers.FirstOrDefault(u => u.UserName == $"{data.LastName}{data.LastName![..i]}") == null)
                {
                    possibleUsername = existingUsers[i].UserName!;
                }
            }
        }

        return Task.FromResult(possibleUsername.RemoveWhiteSpaces());
    }

    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="data"></param>
    /// <param name="isAdminInsert"></param>
    /// <param name="inviteNewUser"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task<ApplicationUser> CreateUserInternal(
        ApplicationUserInputModel data,
        bool isAdminInsert,
        bool inviteNewUser
    )
    {
        if (await UserExists(data.Email!, data.UserName!, _userManager))
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_USER_EXIST())
                    .SetCode(ErrorCodes.CODE_USER_ERROR_EXIST)
                    .Build()
            );
        }

        if (!OnlyValidRoles(data.Role!))
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_INVALID_ROLE())
                    .SetCode(ErrorCodes.CODE_USER_ERROR_INVALID_ROLE)
                    .Build()
            );
        }

        if (data.UserName.IsNullOrEmpty())
            data.UserName = await GenerateUserName(data);

        var user = new ApplicationUser();
        user = _mapper.Map(data, user!);

        user.Gender = user.Gender.FirstCharToUpperAsSpan();

        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;

        if (isAdminInsert)
            user.EmailConfirmed = true;

        IdentityResult createResult = await _userManager.CreateAsync(user).ConfigureAwait(false);

        if (!createResult.Succeeded)
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_CREATE_USER())
                    .SetCode(ErrorCodes.CODE_USER_ERROR_CANNOT_CREATE)
                    .Build()
            );
        }

        // If no password then generate random password
        // Case for Invite new user because he will create password per email confirmation
        if (string.IsNullOrEmpty(data.Password))
        {
            try
            {
                await _userManager
                    .AddPasswordAsync(user, Constants.CustomSecurity.PasswordLength.RandomString())
                    .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.LogError("Cannot save random password CreateUserInternal: {Exception}", exception);
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage(_errorMessages.ERROR_CREATE_USER())
                        .SetCode(ErrorCodes.CODE_ERROR_CANNOT_SAVE)
                        .Build()
                );
            }
        }
        else
        {
            try
            {
                await _userManager.AddPasswordAsync(user, data.Password).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.LogError("Cannot save user password CreateUserInternal: {Exception}", exception);
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage(_errorMessages.ERROR_CREATE_USER())
                        .SetCode(ErrorCodes.CODE_ERROR_CANNOT_SAVE)
                        .Build()
                );
            }
        }

        try
        {
            if (data.Role!.Equals(Shared.Helpers.Constants.Roles.Admin))
                await _userManager.AddToRoleAsync(user, Shared.Helpers.Constants.Roles.User).ConfigureAwait(false);

            await _userManager.AddToRoleAsync(user, data.Role!).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            _logger.LogError("Save role for user CreateUserInternal: {Exception}", exception);
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_CREATE_USER())
                    .SetCode(ErrorCodes.CODE_ERROR_CANNOT_SAVE)
                    .Build()
            );
        }

        return user;
    }

    /// <summary>
    /// Update user internal
    /// </summary>
    /// <param name="data"></param>
    /// <param name="applicationUserId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task<ApplicationUser> UpdateUserInternal(
        ApplicationUserInputModel data,
        string applicationUserId,
        CancellationToken cancellationToken
    )
    {
        // Checks -> abort with exception if necessary
        if (!data.Role.IsNullOrEmpty() && !OnlyValidRoles(data.Role!))
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_INVALID_ROLE())
                    .SetCode(ErrorCodes.CODE_USER_ERROR_INVALID_ROLE)
                    .Build()
            );
        }

        IEnumerable<ApplicationUser> applicationUserList = await _context
            .ApplicationUser!.Where(x => x.Id == applicationUserId)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        if (applicationUserList.Count() != 1)
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_USER_NOT_EXIST())
                    .SetCode(ErrorCodes.CODE_USER_ERROR_INVALID_ROLE)
                    .Build()
            );
        }

        ApplicationUser? user = applicationUserList.FirstOrDefault();
        user = _mapper.Map(data, user!);

        user!.UpdatedAt = DateTime.Now;

        if (!data.Password.IsNullOrEmpty())
        {
            try
            {
                await _userManager.AddPasswordAsync(user, data.Password!).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.LogError("Cannot save user password UpdateUserInternal: {Exception}", exception);
                throw new GraphQLException(
                    ErrorBuilder
                        .New()
                        .SetMessage(_errorMessages.ERROR_UPDATE_USER())
                        .SetCode(ErrorCodes.CODE_ERROR_CANNOT_SAVE)
                        .Build()
                );
            }
        }

        try
        {
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            _logger.LogError("Cannot update user UpdateUserInternal: {Exception}", exception);
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_UPDATE_USER())
                    .SetCode(ErrorCodes.CODE_ERROR_CANNOT_SAVE)
                    .Build()
            );
        }

        return user;
    }

    /// <summary>
    /// Check if role is valid
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    private static bool OnlyValidRoles(string roleName)
    {
        IEnumerable<string> values = typeof(Shared.Helpers.Constants.Roles)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(x => x is { IsLiteral: true, IsInitOnly: false })
            .Select(x => x.GetValue(null))
            .Cast<string>();

        return values.Contains(roleName.FirstCharToUpperAsSpan());
    }

    [GeneratedRegex(@"(\+[^\@]+)", RegexOptions.IgnoreCase, "sk-SK")]
    private static partial Regex EmailAliasRegex();

    #endregion Private methods

    #endregion  Implementation
}

using Data;
using Data.Entities;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;
using ErrorCodes = Shared.Helpers.ErrorCodes;

namespace Server.Services;

#region Interface
public interface IRoleService
{
    Task<IEnumerable<ApplicationRole>> GetRoles(CancellationToken cancellationToken);
    Task<ApplicationRole> GetRole(IResolverContext resolverContext, CancellationToken cancellationToken);
}
#endregion Interface

public class RoleService : IRoleService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ApplicationDbContext _context;

    private ApplicationDbContext GetApplicationContext()
    {
        return (ApplicationDbContext)_serviceProvider.GetService(typeof(ApplicationDbContext))!;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceProvider"></param>
    public RoleService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _context = GetApplicationContext();
    }

    #region Implementation

    #region Public methods

    /// <summary>
    /// Get roles
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ApplicationRole>> GetRoles(CancellationToken cancellationToken)
    {
        IQueryable<ApplicationRole> query = _context.Roles;
        return await query.AsNoTracking().ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get roles by filters
    /// </summary>
    /// <param name="resolverContext"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ApplicationRole> GetRole(IResolverContext resolverContext, CancellationToken cancellationToken)
    {
        ApplicationRole query =
            await _context
                .Roles.Filter(resolverContext)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false) ?? throw new Exception(ErrorCodes.CODE_NOT_FOUND_GRAPHQL_FILTER);
        return query;
    }

    #endregion Public methods

    #endregion  Implementation
}

using AutoMapper;
using Data;
using Data.Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.InputModels;
using Shared.Models;
using Shared.Models.ModuleSpecialAnnouncement;
using Shared.Validators.NewsModuleValidator;

namespace Server.Services;

#region Interface

public interface IModuleSpecialAnnouncementService
{
    Task<IEnumerable<ModuleSpecialAnnouncementMobileModel>> GetModuleSpecialAnnouncementMobile(
        ModuleGetInputModel data,
        CancellationToken cancellationToken
    );
}

#endregion Interface

#region Implementation

public class ModuleSpecialAnnouncementService : IModuleSpecialAnnouncementService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="mapper"></param>
    public ModuleSpecialAnnouncementService(IServiceProvider serviceProvider, IMapper mapper)
    {
        _serviceProvider = serviceProvider;
        _mapper = mapper;
        _context = GetApplicationDbContext();
    }

    private ApplicationDbContext GetApplicationDbContext()
    {
        return (ApplicationDbContext)_serviceProvider.GetService(typeof(ApplicationDbContext))!;
    }

    #region Public methods

    public async Task<IEnumerable<ModuleSpecialAnnouncementMobileModel>> GetModuleSpecialAnnouncementMobile(
        ModuleGetInputModel data,
        CancellationToken cancellationToken
    )
    {
        var validator = new GetModuleValidator();
        ValidationResult results = await validator.ValidateAsync(data, cancellationToken);
        if (!results.IsValid)
        {
            string errorMessage = results.Errors.Aggregate("", (current, error) => current + error.ErrorMessage);
            string errorCode = results.Errors.Aggregate("", (current, error) => current + error.ErrorCode);

            throw new GraphQLException(ErrorBuilder.New().SetMessage(errorMessage).SetCode(errorCode).Build());
        }

        OrganisationModuleService? orgModule = await _context
            .OrganisationModuleService!.Where(x =>
                x.ModuleServiceId.Equals(data.ModuleServiceId) && x.OrganisationId.Equals(data.OrganisationId)
            )
            .Include(x => x.ModuleSpecialAnnouncements)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (orgModule.IsNull())
            return [];

        var retVals = new List<ModuleSpecialAnnouncementMobileModel>();
        retVals = _mapper.Map(orgModule!.ModuleSpecialAnnouncements, retVals);

        return retVals;
    }

    #endregion Public methods

#endregion Implementation
}

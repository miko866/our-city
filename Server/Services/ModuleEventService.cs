using AutoMapper;
using Data;
using Data.Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Helpers;
using Shared.Extensions;
using Shared.InputModels.ModuleEvent;
using Shared.Models;
using Shared.Models.ModuleEvent;
using Shared.Validators.ModuleEvent;
using ErrorCodes = Shared.Helpers.ErrorCodes;

namespace Server.Services;

#region Interface

public interface IModuleEventService
{
    Task<IEnumerable<ModuleEventMobileModel>> GetModuleEventMobile(
        ModuleEventFilterInputModel getInput,
        CancellationToken cancellationToken
    );
    Task<ModuleEventMobileModel> GetModuleEventMobileByIdMobile(string id, CancellationToken cancellationToken);
}

#endregion Interface

#region Implementation

public class ModuleEventService : IModuleEventService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;
    private readonly IErrorMessages _errorMessages;
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="mapper"></param>
    /// <param name="errorMessages"></param>
    public ModuleEventService(IServiceProvider serviceProvider, IMapper mapper, IErrorMessages errorMessages)
    {
        _serviceProvider = serviceProvider;
        _mapper = mapper;
        _errorMessages = errorMessages;
        _context = GetApplicationDbContext();
    }

    private ApplicationDbContext GetApplicationDbContext()
    {
        return (ApplicationDbContext)_serviceProvider.GetService(typeof(ApplicationDbContext))!;
    }

    #region Public methods

    public async Task<IEnumerable<ModuleEventMobileModel>> GetModuleEventMobile(
        ModuleEventFilterInputModel data,
        CancellationToken cancellationToken
    )
    {
        var validator = new ModuleEventFilterInputValidator();
        ValidationResult results = await validator.ValidateAsync(data, cancellationToken);
        if (!results.IsValid)
        {
            string errorMessage = results.Errors.Aggregate("", (current, error) => current + error.ErrorMessage);
            string errorCode = results.Errors.Aggregate("", (current, error) => current + error.ErrorCode);

            throw new GraphQLException(ErrorBuilder.New().SetMessage(errorMessage).SetCode(errorCode).Build());
        }

        IQueryable<OrganisationModuleService> orgModule = _context
            .OrganisationModuleService!.Where(x =>
                x.ModuleServiceId.Equals(data.ModuleServiceId) && x.OrganisationId.Equals(data.OrganisationId)
            )
            .Include(x => x.ModuleEvents)
            .ThenInclude(x => x.TagModuleEvents)!
            .ThenInclude(x => x.Tag)
            .Include(x => x.ModuleEvents)
            .ThenInclude(x => x.ModuleEventFileItems)
            .ThenInclude(x => x.FileItem)
            .Include(x => x.ModuleEvents)
            .ThenInclude(x => x.TagModuleEvents)!
            .ThenInclude(x => x.Tag)
            .AsSplitQuery()
            .AsNoTracking();

        if (orgModule.IsNull())
            return [];

        IEnumerable<ModuleEvent>? filteredValues = [];

        if (data.DateValue.IsNotNull())
        {
            filteredValues = await orgModule
                .Select(x => x.ModuleEvents.Where(x => x.DateFrom >= data.DateValue))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        else if (data.TagValues.IsNotNull())
        {
            filteredValues = await orgModule
                .Select(x =>
                    x.ModuleEvents.Where(x => x.TagModuleEvents!.Any(x => data.TagValues!.Contains(x.Tag.Name)))
                )
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        else
        {
            filteredValues = await orgModule
                .Select(x => x.ModuleEvents)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        if (filteredValues.IsNullOrEmpty())
            return [];

        var retVals = new List<ModuleEventMobileModel>();
        retVals = _mapper.Map(filteredValues, retVals);

        foreach (ModuleEventMobileModel value in retVals)
        {
            value.FeaturedImage = filteredValues!
                .Where(x => x.Id.Equals(value.Id))
                .Select(x =>
                    x.ModuleEventFileItems.Where(x => x.IsFeaturedImage && x.ModuleEventId.Equals(value.Id))
                        .Select(x => x.FileItem.Href)
                        .FirstOrDefault()
                )
                .FirstOrDefault();

            // value.Gallery = filteredValues.SelectMany(x =>
            //         x.ModuleEventFileItems.Where(x => !x.IsFeaturedImage && x.ModuleEventId.Equals(value.Id)))
            //     .Select(x => new GalleryModel
            //     {
            //         Name = x.FileItem.FileOriginName,
            //         UrlLink = x.FileItem.Href
            //     }).ToList();

            value.Tags = filteredValues!
                .SelectMany(x => x.TagModuleEvents!.Where(x => x.ModuleEventId.Equals(value.Id)))
                .Select(x => new TagModel { Name = x.Tag.Name, Color = x.Tag.Color })
                .ToList();
        }

        return retVals;
    }

    public async Task<ModuleEventMobileModel> GetModuleEventMobileByIdMobile(
        string id,
        CancellationToken cancellationToken
    )
    {
        if (id.IsNullOrEmpty())
            ErrorBuilder
                .New()
                .SetMessage(_errorMessages.ERROR_NOT_NULL_OR_EMPTY())
                .SetCode(ErrorCodes.CODE_ERROR_NOT_NULL_OR_EMPTY)
                .Build();

        ModuleEvent? data = await _context
            .ModuleEvent!.Where(x => x.Id.Equals(id))
            .Include(x => x.TagModuleEvents)!
            .ThenInclude(x => x.Tag)
            .Include(x => x.ModuleEventFileItems)
            .ThenInclude(x => x.FileItem)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (data.IsNull())
            return null!;

        var retVal = new ModuleEventMobileModel();
        retVal = _mapper.Map(data, retVal);

        retVal.FeaturedImage = data!
            .ModuleEventFileItems.Where(x => x.IsFeaturedImage && x.ModuleEventId.Equals(id))
            .Select(x => x.FileItem.Href)
            .FirstOrDefault();

        retVal.Gallery = data
            .ModuleEventFileItems.Where(x => !x.IsFeaturedImage && x.ModuleEventId.Equals(id))
            .Select(x => new GalleryModel { Name = x.FileItem.FileOriginName, UrlLink = x.FileItem.Href })
            .ToList();

        retVal.Tags = data.TagModuleEvents!.Where(x => x.ModuleEventId.Equals(id))
            .Select(x => new TagModel { Name = x.Tag.Name, Color = x.Tag.Color })
            .ToList();

        return retVal;
    }

    #endregion Public methods

#endregion Implementation
}

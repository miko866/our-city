using AutoMapper;
using Data;
using Data.Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Helpers;
using Shared.Extensions;
using Shared.InputModels;
using Shared.Models;
using Shared.Models.ModuleNews;
using Shared.Models.ModuleSimplePage;
using Shared.Validators.NewsModuleValidator;
using ErrorCodes = Shared.Helpers.ErrorCodes;

namespace Server.Services;

#region Interface

public interface IModuleSimplePageService
{
    Task<IEnumerable<ModuleSimplePageMobileModel>> GetModuleSimplePageMobile(
        ModuleGetInputModel data,
        CancellationToken cancellationToken
    );
    Task<ModuleSimplePageMobileModel> GetModuleSimplePageMobileByIdMobile(
        string id,
        CancellationToken cancellationToken
    );
}

#endregion Interface

#region Implementation

public class ModuleSimplePageService : IModuleSimplePageService
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
    public ModuleSimplePageService(IServiceProvider serviceProvider, IMapper mapper, IErrorMessages errorMessages)
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

    public async Task<IEnumerable<ModuleSimplePageMobileModel>> GetModuleSimplePageMobile(
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
            .Include(x => x.ModuleSimplePages)
            .ThenInclude(x => x.ModuleSimplePageFileItems)
            .ThenInclude(x => x.FileItem)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (orgModule.IsNull())
            return [];

        var retVals = new List<ModuleSimplePageMobileModel>();
        retVals = _mapper.Map(orgModule!.ModuleSimplePages, retVals);

        foreach (ModuleSimplePageMobileModel value in retVals)
        {
            value.FeaturedImage = orgModule
                .ModuleSimplePages.Where(x => x.Id.Equals(value.Id))
                .Select(x =>
                    x.ModuleSimplePageFileItems.Where(x => x.IsFeaturedImage && x.ModuleSimplePageId.Equals(value.Id))
                        .Select(x => x.FileItem.Href)
                        .FirstOrDefault()
                )
                .FirstOrDefault();

            // value.Gallery = orgModule.ModuleSimplePages.SelectMany(x =>
            //         x.ModuleSimplePageFileItems.Where(x => x.ModuleSimplePageId.Equals(value.Id)))
            //     .Select(x => new GalleryModel
            //     {
            //         Name = x.FileItem.FileOriginName,
            //         UrlLink = x.FileItem.Href
            //     }).ToList();
        }

        return retVals;
    }

    public async Task<ModuleSimplePageMobileModel> GetModuleSimplePageMobileByIdMobile(
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

        ModuleSimplePage? data = await _context
            .ModuleSimplePage!.Where(x => x.Id.Equals(id))
            .Include(x => x.ModuleSimplePageFileItems)
            .ThenInclude(x => x.FileItem)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (data.IsNull())
            return null!;

        var retVal = new ModuleSimplePageMobileModel();
        retVal = _mapper.Map(data, retVal);

        retVal.FeaturedImage = data!
            .ModuleSimplePageFileItems.Where(x => x.IsFeaturedImage && x.ModuleSimplePageId.Equals(id))
            .Select(x => x.FileItem.Href)
            .FirstOrDefault();

        retVal.Gallery = data
            .ModuleSimplePageFileItems.Where(x => x.ModuleSimplePageId.Equals(id))
            .Select(x => new GalleryModel { Name = x.FileItem.FileOriginName, UrlLink = x.FileItem.Href })
            .ToList();

        return retVal;
    }

    #endregion Public methods

#endregion Implementation
}

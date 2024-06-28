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
using Shared.Validators.NewsModuleValidator;
using ErrorCodes = Shared.Helpers.ErrorCodes;

namespace Server.Services;

#region Interface

public interface IModuleNewsService
{
    Task<IEnumerable<ModuleNewsMobileModel>> GetModuleNewsMobile(
        ModuleGetInputModel moduleGetInput,
        CancellationToken cancellationToken
    );
    Task<ModuleNewsMobileModel> GetModuleNewsByIdMobile(string id, CancellationToken cancellationToken);
}

#endregion Interface

#region Implementation

public class ModuleNewsService : IModuleNewsService
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
    public ModuleNewsService(IServiceProvider serviceProvider, IMapper mapper, IErrorMessages errorMessages)
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

    public async Task<IEnumerable<ModuleNewsMobileModel>> GetModuleNewsMobile(
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
            .Include(x => x.ModuleNews)
            .ThenInclude(x => x.TagModuleNews)!
            .ThenInclude(x => x.Tag)
            .Include(x => x.ModuleNews)
            .ThenInclude(x => x.ModuleNewsFileItems)
            .ThenInclude(x => x.FileItem)
            .Include(x => x.ModuleNews)
            .ThenInclude(x => x.TagModuleNews)!
            .ThenInclude(x => x.Tag)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (orgModule.IsNull())
            return [];

        var retVals = new List<ModuleNewsMobileModel>();
        retVals = _mapper.Map(orgModule!.ModuleNews, retVals);

        foreach (ModuleNewsMobileModel value in retVals)
        {
            value.FeaturedImage = orgModule
                .ModuleNews.Where(x => x.Id.Equals(value.Id))
                .Select(x =>
                    x.ModuleNewsFileItems.Where(x => x.IsFeaturedImage && x.ModuleNewsId.Equals(value.Id))
                        .Select(x => x.FileItem.Href)
                        .FirstOrDefault()
                )
                .FirstOrDefault();

            // value.Gallery = orgModule.ModuleNews.SelectMany(x =>
            //         x.ModuleNewsFileItems.Where(x => !x.IsFeaturedImage && x.ModuleNewsId.Equals(value.Id)))
            //     .Select(x => new GalleryModel
            //     {
            //         Name = x.FileItem.FileOriginName,
            //         UrlLink = x.FileItem.Href
            //     }).ToList();

            value.Tags = orgModule
                .ModuleNews.SelectMany(x => x.TagModuleNews!.Where(x => x.ModuleNewsId.Equals(value.Id)))
                .Select(x => new TagModel { Name = x.Tag.Name, Color = x.Tag.Color })
                .ToList();
        }

        return retVals;
    }

    public async Task<ModuleNewsMobileModel> GetModuleNewsByIdMobile(string id, CancellationToken cancellationToken)
    {
        if (id.IsNullOrEmpty())
            ErrorBuilder
                .New()
                .SetMessage(_errorMessages.ERROR_NOT_NULL_OR_EMPTY())
                .SetCode(ErrorCodes.CODE_ERROR_NOT_NULL_OR_EMPTY)
                .Build();

        ModuleNews? data = await _context
            .ModuleNews!.Where(x => x.Id.Equals(id))
            .Include(x => x.TagModuleNews)!
            .ThenInclude(x => x.Tag)
            .Include(x => x.ModuleNewsFileItems)
            .ThenInclude(x => x.FileItem)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (data.IsNull())
            return null!;

        var retVal = new ModuleNewsMobileModel();
        retVal = _mapper.Map(data, retVal);

        retVal.FeaturedImage = data!
            .ModuleNewsFileItems.Where(x => x.IsFeaturedImage && x.ModuleNewsId.Equals(id))
            .Select(x => x.FileItem.Href)
            .FirstOrDefault();

        retVal.Gallery = data
            .ModuleNewsFileItems.Where(x => !x.IsFeaturedImage && x.ModuleNewsId.Equals(id))
            .Select(x => new GalleryModel { Name = x.FileItem.FileOriginName, UrlLink = x.FileItem.Href })
            .ToList();

        retVal.Tags = data.TagModuleNews!.Where(x => x.ModuleNewsId.Equals(id))
            .Select(x => new TagModel { Name = x.Tag.Name, Color = x.Tag.Color })
            .ToList();

        return retVal;
    }

    #endregion Public methods

#endregion Implementation
}

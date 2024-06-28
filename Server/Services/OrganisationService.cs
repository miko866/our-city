using AutoMapper;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Common;
using Server.Helpers;
using Shared.Models;
using Shared.Models.ModuleSimplePage;
using Shared.Models.ModuleSpecialAnnouncement;
using Shared.Models.Organisation;
using ErrorCodes = Shared.Helpers.ErrorCodes;

namespace Server.Services;

#region Interface

public interface IOrganisationService
{
    Task<IEnumerable<Organisation>> GetOrganisations(CancellationToken cancellationToken);
    Task<IEnumerable<OrganisationsListMobileModel>> GetOrganisationsListMobile(CancellationToken cancellationToken);
    Task<OrganisationMobileModel> GetOrganisationByIdMobile(string id, CancellationToken cancellationToken);
    // Task<Organisation> CreateMunicipality(OrganisationInputModel data, CancellationToken cancellationToken);
    // Task<Organisation> UpdateMunicipality(OrganisationInputModel data, int id, CancellationToken cancellationToken);
    // Task<bool> DeleteMunicipality(int id, CancellationToken cancellationToken);
}

#endregion Interface

#region Implementation

public class OrganisationService : IOrganisationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IGisCommon _gisCommon;
    private readonly ILogger<OrganisationService> _logger;
    private readonly IMapper _mapper;
    private readonly IErrorMessages _errorMessages;
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="httpClientFactory"></param>
    /// <param name="gisCommon"></param>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    public OrganisationService(
        IServiceProvider serviceProvider,
        IHttpClientFactory httpClientFactory,
        IGisCommon gisCommon,
        ILogger<OrganisationService> logger,
        IMapper mapper,
        IErrorMessages errorMessages
    )
    {
        _serviceProvider = serviceProvider;
        _httpClientFactory = httpClientFactory;
        _gisCommon = gisCommon;
        _logger = logger;
        _mapper = mapper;
        _errorMessages = errorMessages;
        _context = GetApplicationDbContext();
    }

    private ApplicationDbContext GetApplicationDbContext()
    {
        return (ApplicationDbContext)_serviceProvider.GetService(typeof(ApplicationDbContext))!;
    }

    #region Public methods

    /// <summary>
    /// Get emails
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IEnumerable<Organisation>> GetOrganisations(CancellationToken cancellationToken)
    {
        List<Organisation> organisations = await _context
            .Organisation!.Include(x => x.OrganisationFileItems)
            .ThenInclude(x => x.FileItem)
            .ThenInclude(x => x.FileItemType)
            .Include(x => x.OrganisationModuleServices)
            .ThenInclude(x => x.ModuleService)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return organisations;
    }

    public async Task<IEnumerable<OrganisationsListMobileModel>> GetOrganisationsListMobile(
        CancellationToken cancellationToken
    )
    {
        List<Organisation> organisations = await _context
            .Organisation!.Include(x => x.OrganisationFileItems)
            .ThenInclude(x => x.FileItem)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        var data = new List<OrganisationsListMobileModel>();
        data = _mapper.Map(organisations, data);

        foreach (OrganisationsListMobileModel value in data)
        {
            value.LogoMini = organisations
                .Where(x => x.Id.Equals(value.Id))
                .Select(x => x.OrganisationFileItems.FirstOrDefault(x => x.IsLogoMini))
                .Select(x => x!.FileItem.Href)
                .FirstOrDefault();

            value.Logo = organisations
                .Where(x => x.Id.Equals(value.Id))
                .Select(x => x.OrganisationFileItems.FirstOrDefault(x => x.IsLogo))
                .Select(x => x!.FileItem.Href)
                .FirstOrDefault();
        }

        return data;
    }

    public async Task<OrganisationMobileModel> GetOrganisationByIdMobile(string id, CancellationToken cancellationToken)
    {
        if (id.IsNullOrEmpty())
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_NOT_NULL_OR_EMPTY())
                    .SetCode(ErrorCodes.CODE_ERROR_NOT_NULL_OR_EMPTY)
                    .Build()
            );
        }

        Organisation organisation = (
            await _context
                .Organisation!.Where(x => x.Id.Equals(id))
                .Include(x => x.OrganisationFileItems)
                .ThenInclude(x => x.FileItem)
                .ThenInclude(x => x.FileItemType)
                .Include(x => x.OrganisationModuleServices)
                .ThenInclude(x => x.ModuleService)
                .Include(x => x.OrganisationModuleServices)
                .ThenInclude(x => x.ModuleSpecialAnnouncements)
                .Include(x => x.OrganisationModuleServices)
                .ThenInclude(x => x.ModuleSimplePages)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false)
        )!;

        var organisationMobileModel = _mapper.Map<OrganisationMobileModel>(organisation);

        organisationMobileModel.Logo = organisation
            .OrganisationFileItems.Where(x => x.IsLogo)
            .Select(x => x.FileItem.Href)
            .FirstOrDefault();

        organisationMobileModel.LogoMini = organisation
            .OrganisationFileItems.Where(x => x.IsLogoMini)
            .Select(x => x.FileItem.Href)
            .FirstOrDefault();

        List<ModuleServiceContentModel> data = organisation
            .OrganisationModuleServices.Select(x => new ModuleServiceContentModel
            {
                Id = x.ModuleService.Id,
                Name = x.ModuleService.Name,
                Icon = x.ModuleService.Icon,
                Description = x.ModuleService.Description,
                ModuleType = x.ModuleService.ModuleType,
                ModuleSimplePages = x
                    .ModuleSimplePages.Where(y => y.OrganisationModuleServiceId.Equals(x.Id))
                    .Select(y => new ModuleSimplePageMobileModel
                    {
                        Id = y.Id,
                        Title = y.Title,
                        Context = y.Context,
                        Icon = y.Icon,
                        UrlLink = y.UrlLink,
                        VideoLink = y.VideoLink,
                        CreatedBy = y.CreatedBy,
                        CreatedAt = y.CreatedAt
                    })
                    .ToList(),
                ModuleSpecialAnnouncements = x
                    .ModuleSpecialAnnouncements.Where(y => y.OrganisationModuleServiceId.Equals(x.Id))
                    .Select(y => new ModuleSpecialAnnouncementMobileModel
                    {
                        Id = y.Id,
                        TextMessage = y.TextMessage,
                        Severity = y.Severity.ToString(),
                        UrlLink = y.UrlLink!,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy
                    })
                    .ToList()
            })
            .ToList();

        organisationMobileModel.Modules = data;

        return organisationMobileModel;
    }

    // /// <summary>
    // /// CreateMunicipality
    // /// </summary>
    // /// <param name="data"></param>
    // /// <param name="cancellationToken"></param>
    // /// <returns></returns>
    // /// <exception cref="Exception"></exception>
    // public async Task<Organisation> CreateMunicipality(OrganisationInputModel data, CancellationToken cancellationToken)
    // {
    // var validator = new CreateOrganisationValidator();
    // ValidationResult results = await validator.ValidateAsync(data!, cancellationToken);
    // if (!results.IsValid)
    // {
    //     string errorMessage = results.Errors.Aggregate("", (current, error) => current + error.ErrorMessage);
    //     _logger.LogWarning("WARNING in CreateMunicipality - Validator! {ErrorMessage}", errorMessage);
    //     throw new Exception(errorMessage);
    // }
    //
    // bool checkDistrict = await _context.District?.AnyAsync(
    //     x => x.Id == data.DistrictId,
    //     cancellationToken: cancellationToken
    // )!;
    // if (!checkDistrict)
    // {
    //     _logger.LogWarning("WARNING in CreateMunicipality checkDistrict - {Id}", data.DistrictId);
    //     throw new Exception(ErrorCodes.CODE_NOT_FOUND);
    // }
    //
    // var municipality = new Organisation();
    // municipality = _mapper.Map(data, municipality!);
    //
    // var address = $"{data.Street} {data.StreetNr} {data.City} {data.Zip}";
    //
    // var coordinates = await _gisCommon.GetCoordinates(address, cancellationToken);
    //
    // municipality.Latitude = coordinates.Lat;
    // municipality.Longitude = coordinates.Long;
    // municipality.CreatedAt = DateTime.Now;
    // municipality.UpdatedAt = DateTime.Now;
    //
    // await _context.Organisation!.AddAsync(municipality, cancellationToken).ConfigureAwait(false);
    //
    // try
    // {
    //     await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    // }
    // catch (Exception exception)
    // {
    //     _logger.LogError("Error cannot save data for  - CreateMunicipality {Exception}", exception);
    //     throw new Exception(ErrorCodes.CODE_ERROR_CANNOT_SAVE);
    // }
    //
    // return municipality;
    //     throw new NotImplementedException();
    // }

    // /// <summary>
    // /// UpdateMunicipality
    // /// </summary>
    // /// <param name="data"></param>
    // /// <param name="resolverContext"></param>
    // /// <param name="id"></param>
    // /// <param name="cancellationToken"></param>
    // /// <returns></returns>
    // /// <exception cref="Exception"></exception>
    // public async Task<Organisation> UpdateMunicipality(OrganisationInputModel data, int id, CancellationToken
    //         cancellationToken)
    //   {
    //       var validator = new Organisation();
    //       ValidationResult results = await validator.ValidateAsync(data!, cancellationToken);
    //
    //       if (!results.IsValid)
    //       {
    //           string errorMessage = results.Errors.Aggregate("", (current, error) => current + error.ErrorMessage);
    //           _logger.LogWarning("WARNING in UpdateMunicipality - Validator! {ErrorMessage}", errorMessage);
    //           throw new Exception(errorMessage);
    //       }
    //
    //       var emailWhiteBlackList = await _context.Organisation!.FindAsync(new object?[] { id }, cancellationToken:
    //           cancellationToken).ConfigureAwait(false);
    //
    //       if (emailWhiteBlackList.IsNull())
    //       {
    //           _logger.LogWarning($"WARNING in UpdateMunicipality - The result is not clear or no result was found.");
    //           throw new Exception(ErrorCodes.NOT_AN_UNIQUE_RESULT);
    //       }
    //
    //       emailWhiteBlackList = _mapper.Map(data, emailWhiteBlackList);
    //
    //       emailWhiteBlackList!.UpdatedAt = DateTime.Now;
    //
    //       try
    //       {
    //           await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    //       }
    //       catch (Exception exception)
    //       {
    //           _logger.LogError("Error cannot save data for  - UpdateMunicipality {Exception}", exception);
    //           throw new Exception(ErrorCodes.ERROR_CANNOT_SAVE);
    //       }
    //
    //       return emailWhiteBlackList!;
    //   }
    //
    //   public async Task<bool> DeleteMunicipality(int id, CancellationToken cancellationToken)
    //   {
    //       var emailWhiteBlackList = await _context.Organisation!.FindAsync(new object?[] { id }, cancellationToken:
    //           cancellationToken).ConfigureAwait(false);
    //
    //       if (emailWhiteBlackList == null)
    //       {
    //           _logger.LogWarning("WARNING in DeleteMunicipality - ID doesn't exists: {Id}", emailWhiteBlackList!.Id);
    //           throw new Exception(ErrorCodes.NOT_FOUND);
    //       }
    //
    //       _context.Organisation?.Remove(emailWhiteBlackList);
    //
    //       try
    //       {
    //           await _context.SaveChangesAsync(cancellationToken);
    //       }
    //       catch (Exception exception)
    //       {
    //           _logger.LogError("Error cannot save data for  - DeleteMunicipality {Exception}", exception);
    //           throw new Exception(ErrorCodes.ERROR_CANNOT_SAVE);
    //       }
    //
    //       return true;
    //   }

    #endregion Public methods

    // #region Private methods
    //
    // #endregion Private methods

#endregion Implementation
}

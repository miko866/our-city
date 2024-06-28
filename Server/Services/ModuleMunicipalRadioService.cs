using AutoMapper;
using Data;
using Data.Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.InputModels;
using Shared.Models;
using Shared.Models.ModuleMunicipalRadio;
using Shared.Validators.NewsModuleValidator;

namespace Server.Services;

#region Interface

public interface IModuleMunicipalRadioService
{
    Task<IEnumerable<ModuleMunicipalRadioMobileModel>> GetModuleMunicipalRadioMobile(
        ModuleGetInputModel data,
        CancellationToken cancellationToken
    );
}

#endregion Interface

#region Implementation

public class ModuleMunicipalRadioService : IModuleMunicipalRadioService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="mapper"></param>
    public ModuleMunicipalRadioService(IServiceProvider serviceProvider, IMapper mapper)
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

    public async Task<IEnumerable<ModuleMunicipalRadioMobileModel>> GetModuleMunicipalRadioMobile(
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

        OrganisationModuleService? orgModuleRadio = await _context
            .OrganisationModuleService!.Where(x =>
                x.ModuleServiceId.Equals(data.ModuleServiceId) && x.OrganisationId.Equals(data.OrganisationId)
            )
            .Include(x => x.ModuleMunicipalRadios)
            .ThenInclude(x => x.ModuleMunicipalRadioMessages)
            .ThenInclude(x => x.Message)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (orgModuleRadio.IsNull())
            return [];

        var retVals = new List<ModuleMunicipalRadioMobileModel>();
        retVals = _mapper.Map(orgModuleRadio!.ModuleMunicipalRadios, retVals);

        foreach (ModuleMunicipalRadioMobileModel value in retVals)
        {
            value.Messages = orgModuleRadio
                .ModuleMunicipalRadios.SelectMany(x =>
                    x.ModuleMunicipalRadioMessages.Where(x => x.ModuleMunicipalRadioId.Equals(value.Id))
                )
                .Select(x => new MessageModel() { Message = x.Message.TextMessage, Category = x.Message.Category })
                .ToList();
        }

        return retVals;
    }

    #endregion Public methods

#endregion Implementation
}

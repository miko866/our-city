using AutoMapper;
using Shared.Models;
using Shared.Models.ModuleMunicipalRadio;

namespace Server.AutoMapper.ModelMapping.ModuleMunicipalRadio;

public class ModuleMunicipalRadioMobileModelMapping : Profile
{
    public ModuleMunicipalRadioMobileModelMapping()
    {
        MapModelToEntity();
        MapEntityToModel();
    }

    private void MapModelToEntity()
    {
        CreateMap<ModuleMunicipalRadioMobileModel, Data.Entities.ModuleMunicipalRadio>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    private void MapEntityToModel()
    {
        CreateMap<Data.Entities.ModuleMunicipalRadio, ModuleMunicipalRadioMobileModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

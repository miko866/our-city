using AutoMapper;
using Shared.Models;
using Shared.Models.ModuleEvent;

namespace Server.AutoMapper.ModelMapping.ModuleEvent;

public class ModuleEventMobileModelMapping : Profile
{
    public ModuleEventMobileModelMapping()
    {
        MapModelToEntity();
        MapEntityToModel();
    }

    private void MapModelToEntity()
    {
        CreateMap<ModuleEventMobileModel, Data.Entities.ModuleEvent>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    private void MapEntityToModel()
    {
        CreateMap<Data.Entities.ModuleEvent, ModuleEventMobileModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

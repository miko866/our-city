using AutoMapper;
using Shared.Models;
using Shared.Models.ModuleSimplePage;

namespace Server.AutoMapper.ModelMapping.ModuleSimplePage;

public class ModuleSimplePageModelMappingProfile : Profile
{
    public ModuleSimplePageModelMappingProfile()
    {
        MapModelToEntity();
        MapEntityToModel();
    }

    private void MapModelToEntity()
    {
        CreateMap<ModuleSimplePageMobileModel, Data.Entities.ModuleSimplePage>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    private void MapEntityToModel()
    {
        CreateMap<Data.Entities.ModuleSimplePage, ModuleSimplePageMobileModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

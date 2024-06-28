using AutoMapper;
using Shared.Models;
using Shared.Models.ModuleNews;

namespace Server.AutoMapper.ModelMapping.ModuleNews;

public class ModuleNewsMobileModelMapping : Profile
{
    public ModuleNewsMobileModelMapping()
    {
        MapModelToEntity();
        MapEntityToModel();
    }

    private void MapModelToEntity()
    {
        CreateMap<ModuleNewsMobileModel, Data.Entities.ModuleNews>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    private void MapEntityToModel()
    {
        CreateMap<Data.Entities.ModuleNews, ModuleNewsMobileModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

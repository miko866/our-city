using AutoMapper;
using Shared.Models;
using Shared.Models.ModuleSpecialAnnouncement;

namespace Server.AutoMapper.ModelMapping.ModuleSpecialAnnouncement;

public class ModuleSpecialAnnouncementMobileModelMapping : Profile
{
    public ModuleSpecialAnnouncementMobileModelMapping()
    {
        MapModelToEntity();
        MapEntityToModel();
    }

    private void MapModelToEntity()
    {
        CreateMap<ModuleSpecialAnnouncementMobileModel, Data.Entities.ModuleSpecialAnnouncement>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    private void MapEntityToModel()
    {
        CreateMap<Data.Entities.ModuleSpecialAnnouncement, ModuleSpecialAnnouncementMobileModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

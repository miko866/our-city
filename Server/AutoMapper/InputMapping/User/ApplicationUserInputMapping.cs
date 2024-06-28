using AutoMapper;
using Data.Entities;
using Shared.InputModels;
using Shared.InputModels.User;

namespace Server.AutoMapper.InputMapping.User;

public class ApplicationUserInputMapping : Profile
{
    public ApplicationUserInputMapping()
    {
        MapModelToEntity();
        MapEntityToModel();
    }

    private void MapModelToEntity()
    {
        CreateMap<ApplicationUserInputModel, ApplicationUser>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    private void MapEntityToModel()
    {
        CreateMap<ApplicationUser, ApplicationUserInputModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

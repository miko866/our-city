using AutoMapper;
using Shared.InputModels;
using Shared.InputModels.Organisation;

namespace Server.AutoMapper.InputMapping.Organisation;

public class OrganisationInputMapping : Profile
{
    public OrganisationInputMapping()
    {
        MapModelToEntity();
        MapEntityToModel();
    }

    private void MapModelToEntity()
    {
        CreateMap<OrganisationInputModel, Data.Entities.Organisation>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    private void MapEntityToModel()
    {
        CreateMap<Data.Entities.Organisation, OrganisationInputModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

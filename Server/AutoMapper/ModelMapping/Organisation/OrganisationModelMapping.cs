using AutoMapper;
using Shared.Models;
using Shared.Models.Organisation;

namespace Server.AutoMapper.ModelMapping.Organisation;

public class OrganisationModelMapping : Profile
{
    public OrganisationModelMapping()
    {
        MapModelToEntity();
        MapEntityToModel();
    }

    private void MapModelToEntity()
    {
        CreateMap<OrganisationsListMobileModel, Data.Entities.Organisation>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    private void MapEntityToModel()
    {
        CreateMap<Data.Entities.Organisation, OrganisationsListMobileModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

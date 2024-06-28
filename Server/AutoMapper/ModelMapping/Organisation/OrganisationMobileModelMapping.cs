using AutoMapper;
using Shared.Models;
using Shared.Models.Organisation;

namespace Server.AutoMapper.ModelMapping.Organisation;

public class OrganisationMobileModelMapping : Profile
{
    public OrganisationMobileModelMapping()
    {
        MapModelToEntity();
        MapEntityToModel();
    }

    private void MapModelToEntity()
    {
        CreateMap<OrganisationMobileModel, Data.Entities.Organisation>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    private void MapEntityToModel()
    {
        CreateMap<Data.Entities.Organisation, OrganisationMobileModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

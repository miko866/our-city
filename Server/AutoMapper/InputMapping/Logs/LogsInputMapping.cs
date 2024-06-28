using AutoMapper;
using Shared.InputModels;

namespace Server.AutoMapper.InputMapping.Logs;

public class LogsInputMapping : Profile
{
    public LogsInputMapping()
    {
        MapModelToEntity();
        MapEntityToModel();
    }

    private void MapModelToEntity()
    {
        CreateMap<LogsInputModel, Data.Entities.Logs>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    private void MapEntityToModel()
    {
        CreateMap<Data.Entities.Logs, LogsInputModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

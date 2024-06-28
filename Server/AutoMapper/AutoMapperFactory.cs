using AutoMapper;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Server.AutoMapper;

public interface IMapperFactory
{
    IMapper GetMapper();
}

public class AutoMapperFactory : IMapperFactory
{
    private readonly IConfigurationProvider _mapperConfiguration;

    public AutoMapperFactory(IEnumerable<Profile> mappingProfiles)
    {
        _mapperConfiguration = new MapperConfiguration(configuration =>
        {
            foreach (Profile profile in mappingProfiles)
            {
                configuration.AddProfile(profile);
            }
        });

        _mapperConfiguration.CompileMappings();
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    public IMapper GetMapper()
    {
        return _mapperConfiguration.CreateMapper();
    }
}

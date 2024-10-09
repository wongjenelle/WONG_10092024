using AutoMapper;
using UpStreamer.Server.Features.Videos.Profiles;

namespace UpStreamer.Test.Features.Videos.Helpers
{
    public class MockMapper
    {
        // todo: mock improvements needed
        public IMapper GetMapper()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VideoProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}

using AutoMapper;

namespace Example.UnitTests.Extensions
{
    public static class MapperExtensions
    {
        public static IMapper CreateMapper<T>(params T[] profiles) where T : Profile
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
            
            return config.CreateMapper();
        }
    }
}
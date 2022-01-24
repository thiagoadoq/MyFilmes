using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Corporate.MyFilmes.Schedule.Application.MappingProfiles
{
    public static class AutomapperExtensions
    {
        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(o =>
            {
                o.AddProfile(new EntityToDtoProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}

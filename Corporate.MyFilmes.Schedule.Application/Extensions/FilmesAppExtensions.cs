using Corporate.MyFilmes.Schedule.Application.Contracts.Applications;
using Corporate.MyFilmes.Schedule.Application.Contracts.Implements;
using Corporate.MyFilmes.Schedule.Application.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace Corporate.MyFilmes.Schedule.Application.Extensions
{
    public static class MyFilmesAppExtensions
    {
        public static IServiceCollection AddMyFilmesApplications(this IServiceCollection services)
        {
            #region [Application Services]
            
            services.AddScoped<IFilmesApplication, FilmesApplication>();    
            services.AddScoped<IUserApplication, UserAppication>();    
          

            #endregion

            #region [Automapper]

            services.AddAutomapper();

            #endregion

            return services;
        }
    }
}

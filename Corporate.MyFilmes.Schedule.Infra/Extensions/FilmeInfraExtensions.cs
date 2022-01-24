using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Corporate.MyFilmes.Schedule.Infra.Repositories;
using Microsoft.AspNetCore.Http;
using Corporate.MyFilmes.Schedule.Domain.Entities.Filmes.Repository;
using Corporate.MyFilmes.Schedule.Infra.Repositories.Base;
using Corporate.MyFilmes.Schedule.Infra.RabbitMq;
using System;
using Corporate.MyFilmes.Schedule.Infra.Services.Implements;
using Corporate.MyFilmes.Schedule.Infra.Services.Contracts;

namespace Corporate.MyFilmes.Schedule.Infra.Extensions
{
    public static class MyFilmesInfraExtensions
    {
        public static IServiceCollection AddMyFilmesInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region [DbContext]

            services.AddScoped<IMongoDBContext, MongoDBContext>();

            #endregion

            #region [Repositories]

            services.AddScoped<IFilmesRepository, FilmeRepository>();          

            #endregion

            #region [UnitOfWork]        

            #endregion

            #region [HttpContextAccessor]

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion

            #region [RabbitMq]

            services.AddSingleton<IMessageBus, MessageBus>();

            #endregion

            #region [Cache]

            services.AddRedis(configuration);
            services.AddSingleton<ICacheService, CacheService>();

            #endregion

            return services;
        }

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisEndpoint = configuration["RedisSettings:Endpoint"];
            var redisInstanceName = configuration["RedisSettings:InstanceName"];
            var redisSessionName = configuration["RedisSettings:SessionName"];
            var redisIdleTimeOutStr = configuration["RedisSettings:IdleTimeoutHoras"];

            var redisIdleTimeOut = double.Parse(redisIdleTimeOutStr);

            services.AddStackExchangeRedisCache(redisCache =>
            {
                redisCache.Configuration = redisEndpoint;
                redisCache.InstanceName = redisInstanceName;
            });

            services.AddSession(options =>
            {
                options.Cookie.Name = redisSessionName;
                options.IdleTimeout = TimeSpan.FromHours(redisIdleTimeOut);
            });

            return services;
        }
    }
}

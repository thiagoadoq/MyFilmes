using System;
using System.Threading.Tasks;

namespace Corporate.MyFilmes.Schedule.Infra.Services.Contracts
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task<T> SetAsync<T>(string key, T value, DateTimeOffset? timeToLive = null);
    }
}

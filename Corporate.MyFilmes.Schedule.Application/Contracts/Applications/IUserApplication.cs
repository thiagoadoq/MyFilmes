using Corporate.MyFilmes.Schedule.Application.Mapping.Response;
using Corporate.MyFilmes.Schedule.Domain.Contracts.Filter;
using System.Threading.Tasks;

namespace Corporate.MyFilmes.Schedule.Application.Contracts.Applications
{
    public interface IUserApplication
    {
        Task<GenerateAccessTokenResponse> GenerateAccessToken(GenerateAccessTokenFilter validateLoginFilter, bool encryptPassword = true);          
    }
}

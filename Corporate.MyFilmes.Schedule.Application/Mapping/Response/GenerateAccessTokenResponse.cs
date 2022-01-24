using Corporate.MyFilmes.Schedule.Domain.Entities.User;

namespace Corporate.MyFilmes.Schedule.Application.Mapping.Response
{
    public class GenerateAccessTokenResponse
    {
        public GenerateAccessTokenResponse(bool authorized)
        {
            Authorized = authorized;
        }

        public GenerateAccessTokenResponse(bool authorized, string token)
        {
            
            Authorized = authorized;
            Token = token;
        }

        public User User { get; set; }
        public bool Authorized { get; set; }
        public string Token { get; set; }
    }
}

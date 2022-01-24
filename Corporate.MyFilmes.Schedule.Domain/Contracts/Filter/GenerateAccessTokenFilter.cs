namespace Corporate.MyFilmes.Schedule.Domain.Contracts.Filter
{
    public sealed class GenerateAccessTokenFilter
    {
        public GenerateAccessTokenFilter(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}

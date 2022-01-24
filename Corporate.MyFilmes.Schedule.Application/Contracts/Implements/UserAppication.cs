using Corporate.MyFilmes.Schedule.Application.Contracts.Applications;
using Corporate.MyFilmes.Schedule.Application.Mapping.Response;
using Corporate.MyFilmes.Schedule.Domain.Contracts.Filter;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Corporate.MyFilmes.Schedule.Application.Contracts.Implements
{
    public class UserAppication : IUserApplication
    {
        private const int QuantityDaysForTokenExpiration = 1;
        private readonly IConfiguration _config;

        public UserAppication(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<GenerateAccessTokenResponse> GenerateAccessToken(GenerateAccessTokenFilter validateLoginFilter, bool encryptPassword = true)
        {
            var _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:Token"]));
            var _issuer = _config["AppSettings:Issuer"];
            var _audience = _config["AppSettings:Audience"];

            var signinCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return new GenerateAccessTokenResponse(true, tokenString);
        }      
    }
}

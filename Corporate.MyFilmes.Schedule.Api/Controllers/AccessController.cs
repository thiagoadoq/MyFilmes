using Corporate.MyFilmes.Schedule.Application.Contracts.Applications;
using Corporate.MyFilmes.Schedule.Domain.Contracts.Filter;
using Corporate.MyFilmes.Schedule.Domain.Entities.User;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Corporate.MyFilmes.Schedule.Api.Controllers
{
    [Route("api/access")]
    public class AccessController : Controller
    {
        private readonly IUserApplication _userApplication;

        public AccessController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [Route("getaccesstoken")]
        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            try
            {
                if (!loginUser.Email.Equals("admin@admin.com.br") && !loginUser.Password.Equals("123"))
                    return BadRequest("Dados informados invalidos");


                var validateLoginResult = await _userApplication.GenerateAccessToken(new GenerateAccessTokenFilter(loginUser.Email, loginUser.Password));

                if (!validateLoginResult.Authorized)
                {
                    return Unauthorized(401);
                }

                var user = validateLoginResult.User;

                return Ok(new
                {
                    token = validateLoginResult.Token,
                    user
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, loginUser });
            }
        }
    }
}

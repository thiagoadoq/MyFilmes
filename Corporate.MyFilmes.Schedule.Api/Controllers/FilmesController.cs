using Corporate.MyFilmes.Schedule.Api.Consumers.NewFilme;
using Corporate.MyFilmes.Schedule.Application.Contracts.Applications;
using Corporate.MyFilmes.Schedule.Application.Mapping.Dto.Filme;
using Corporate.MyFilmes.Schedule.Infra.RabbitMq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Corporate.MyFilmes.Schedule.Api.Controllers
{

    [Authorize]
    [Route("api/filmes")]
    public class FilmesController : Controller
    {
        private readonly IFilmesApplication _filmeApplication;
        private readonly IMessageBus _messageBus;

        public FilmesController(IFilmesApplication filme, IMessageBus messageBus)
        {
            _filmeApplication = filme;
            _messageBus = messageBus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filmeId"></param>
        /// <returns></returns>
        [HttpGet("filme-id")]        
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFilmeAsync([FromQuery] Guid filmeId)
        {
            try
            {
                if (filmeId == Guid.Empty) return BadRequest("Id informado esta invalido");

                var result =  _filmeApplication.GetById(filmeId);

                if (result is null) return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]        
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  IActionResult GetAllFilmeAsync()
        {
            try
            {
                var result =  _filmeApplication.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filme"></param>
        /// <returns></returns>
        [HttpPost]        
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> NewFilmeAsync([FromBody] NewFilmeMessage filme)
        {
            try
            {
                if (filme is null) return BadRequest("Filme inválido!");

                await _messageBus.PublishAsync(filme);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filme"></param>
        /// <returns></returns>
        [HttpPut]        
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFilmeAsync([FromBody] FilmeDto filme)
        {
            try
            {
                if (filme != null)
                {
                    var result = await _filmeApplication.UpdateAsync(filme);
                    return Ok(result);
                }
                else
                    return BadRequest("Não foi possivel efeturar operação");

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filmeId"></param>
        /// <returns></returns>
        [HttpDelete]        
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFilmeAsync([FromQuery] Guid filmeId)
        {
            try
            {
                if (filmeId != Guid.Empty)
                {
                    var result = await _filmeApplication.DeleteAsync(filmeId);
                    return Ok();
                }
                else
                    return BadRequest("Não foi possivel efeturar operação");

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


    }
}

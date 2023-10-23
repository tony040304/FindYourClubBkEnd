using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOS;
using Model.ViewModel;
using Service.IServices;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorServices _services;
        private readonly ILogger<JugadorController> _logger;

        public JugadorController(IJugadorServices services, ILogger<JugadorController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpPost("InsertarDatosJugador")]
        public ActionResult<JugadorDTO> InsertarDatos([FromBody] JugadorViewModel model)
        {
            try
            {
                var response = _services.InsertarDatos(model);

                string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                string apiAndEndpointUrl = $"/api/Jugador/InsertarDatosJugador";
                string locationUrl = $"{baseUrl}/{apiAndEndpointUrl}/{response.JugadorId}";
                return Created(locationUrl, response);
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine(innerException.Message);
                    innerException = innerException.InnerException;
                }
                return BadRequest($"{ex.Message}");
            }
            
        }
    }
}

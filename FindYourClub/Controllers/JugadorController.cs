using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using System.Security.Claims;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "2")]
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorServices _services;
        private readonly IFactoryMethJugadores _factoryMethJugadores;
        private readonly IFactory _factory;
        private readonly ILogger<JugadorController> _logger;

        public JugadorController(IJugadorServices services, ILogger<JugadorController> logger, IFactoryMethJugadores factoryMethJugadores, IFactory factory)
        {
            _services = services;
            _logger = logger;
            _factoryMethJugadores = factoryMethJugadores;
            _factory = factory;
        }

        [HttpPost("InsertarDatosJugador")]
        public ActionResult<string> InsertarDatos([FromBody] JugadorDTO jugador)
        {
            string response = string.Empty;
            try
            {
                response = _factory.InsertarDatosJugador(jugador);
                if (response == "ingrese nombre" || response == "Jugador existente")
                    return BadRequest(response);

            }
            catch (Exception ex)
            {
                _logger.LogError("Cree usuario", ex);
                return BadRequest($"{ex.Message}");
            }

            return Ok(response);

        }

        [HttpPost("CrearPostulacionJugador")]
        public ActionResult<string> CrearPostulaciones([FromBody] PostulacionDTO postu)
        {
            string response = string.Empty;
            try
            {
                response = _services.CrearPostulaciones(postu);
                if (response == "postulacion existente" || response == "Falta id equipo o id jugador")
                    return BadRequest(response);

            }
            catch (Exception ex)
            {
                _logger.LogError("Cree usuario", ex);
                return BadRequest($"{ex.Message}");
            }

            return Ok(response);
        }

        [HttpGet("GetListaEquipoXJugadores")]
        public ActionResult<List<EquipoDTO>> GetListaEquipo()
        {
            try
            {
                var response = _factoryMethJugadores.GetListaEquipo();
                if (response.Count == 0)
                {
                    NotFound("No existe ningun equipo");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAll", ex);
                return BadRequest($"{ex.Message}");
            }
        }
    }
}

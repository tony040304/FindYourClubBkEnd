using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using Service.IServices;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorServices _services;
        private readonly IFactoryPostulacion factoryPostulacion;
        private readonly ILogger<JugadorController> _logger;

        public JugadorController(IJugadorServices services, ILogger<JugadorController> logger, IFactoryPostulacion factoryPostulacion)
        {
            _services = services;
            _logger = logger;
            this.factoryPostulacion = factoryPostulacion;
        }

        [HttpPost("InsertarDatosJugador")]
        public ActionResult<string> InsertarDatos([FromBody] JugadorDTO jugador)
        {
            string response = string.Empty;
            try
            {
                response = _services.InsertarDatos(jugador);
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
                response = factoryPostulacion.CrearPostulaciones(postu);
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
    }
}

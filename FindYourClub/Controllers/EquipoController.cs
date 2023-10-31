using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOS;
using Service.IServices;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly IEquipoService _services;
        private readonly IFactoryPostulacion factoryPostulacion;
        private readonly ILogger<EquipoController> _logger;

        public EquipoController(IEquipoService services, ILogger<EquipoController> logger, IFactoryPostulacion factoryPostulacion)
        {
            _services = services;
            _logger = logger;
            this.factoryPostulacion = factoryPostulacion;
        }

        [HttpPost("InsertarDatosEquipo")]
        public ActionResult<string> InsertarDatosEquipo([FromBody] EquipoDTO equipo)
        {
            string response = string.Empty;
            try
            {
                response = _services.InsertarDatosEquipo(equipo);
                if (response == "ingrese nombre" || response == "Equipo existente")
                    return BadRequest(response);

            }
            catch (Exception ex)
            {
                _logger.LogError("Cree usuario", ex);
                return BadRequest($"{ex.Message}");
            }

            return Ok(response);
        }

        [HttpPost("CrearPostulacionEquipo")]
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

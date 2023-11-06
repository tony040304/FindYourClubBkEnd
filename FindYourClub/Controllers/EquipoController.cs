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
        private readonly ILogger<EquipoController> _logger;

        public EquipoController(IEquipoService services, ILogger<EquipoController> logger)
        {
            _services = services;
            _logger = logger;
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

    }
}

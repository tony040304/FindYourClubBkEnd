using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOS;
using Service.IServices;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "3")]
    public class EquipoController : ControllerBase
    {
        private readonly IEquipoService _services;
        private readonly IFactoryMethEquipo _factoryMethEquipo;
        private readonly IFactory _factory;
        private readonly ILogger<EquipoController> _logger;

        public EquipoController(IEquipoService services, ILogger<EquipoController> logger, IFactoryMethEquipo factoryMethEquipo, IFactory factory)
        {
            _services = services;
            _logger = logger;
            _factoryMethEquipo = factoryMethEquipo;
            _factory = factory;
        }

        [HttpPost("InsertarDatosEquipo")]
        public ActionResult<string> InsertarDatosEquipo([FromBody] EquipoDTO equipo)
        {
            string response = string.Empty;
            try
            {
                response = _factory.InsertarDatosEquipo(equipo);
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

        [HttpPost("CrearContrato")]
        public ActionResult<string> CrearContrato(ContratoDTO contrato)
        {
            string response = string.Empty;
            try
            {
                response = _factoryMethEquipo.CrearContrato(contrato);
                if (response == "Falta id equipo o id jugador" || response == "Contrato existente")
                    return BadRequest(response);

            }
            catch (Exception ex)
            {
                _logger.LogError("Cree usuario", ex);
                return BadRequest($"{ex.Message}");
            }

            return Ok(response);
        }
        [HttpGet("GetContratoListaxEquipo")]
        public ActionResult<List<ContratoDTO>> ContratoList()
        {
            try
            {
                var response = _factoryMethEquipo.ContratoList();
                if (response.Count == 0)
                {
                    NotFound("No hay usuarios");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAll", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPostulacionListaxEquipo")]
        public ActionResult<List<PostulacionDTO>> GetListaPostulacion()
        {
            try
            {
                var response = _factoryMethEquipo.GetListaPostulacion();
                if (response.Count == 0)
                {
                    NotFound("No hay usuarios");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAll", ex);
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("BorrarPostulacion/{id}")]
        public ActionResult DeletePostulacion([FromRoute] int id)
        {
            try
            {
                _factoryMethEquipo.DeletePostulacion(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpDelete("BorrarContrato/{id}")]
        public ActionResult DeleteContrato([FromRoute] int id)
        {
            try
            {
                _factoryMethEquipo.DeleteContrato(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest($"{ex.Message}");
            }
        }

    }
}

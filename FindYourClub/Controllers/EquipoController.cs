using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOS;
using Service.IServices;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "3")]
    public class EquipoController : ControllerBase
    {
        private readonly IEquipService _Equipo;
        private readonly IFactory _factory;
        private readonly ILogger<EquipoController> _logger;

        public EquipoController(ILogger<EquipoController> logger, IEquipService Equipo, IFactory factory)
        {
            _logger = logger;
            _Equipo = Equipo;
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
                response = _Equipo.CrearContrato(contrato);
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
        [HttpGet("GetContratoListaxEquipo/{id}")]
        public ActionResult<ContratoDTO> ContratoList([FromRoute] int id)
        {
            try
            {
                var response = _Equipo.ContratoList(id);
                if (response == null)
                {
                    NotFound($"No hay Postulaciones con el equipo(id): {id}");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAll", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPostulacionListaxEquipo/{UsuEquipoId}")]
        public ActionResult GetPostulacionbyTeam([FromRoute] int UsuEquipoId)
        {
            try
            {
                var response = _Equipo.GetPostulacionbyTeam(UsuEquipoId);
                if (response == null)
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
                _Equipo.DeletePostulacion(id);
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
                _Equipo.DeleteContrato(id);
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

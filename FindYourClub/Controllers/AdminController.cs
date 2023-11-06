using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOS;
using Model.Enum;
using Service.IServices;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _Service;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService Service, ILogger<AdminController> logger)
        {
            _Service = Service;
            _logger = logger;
        }

        [HttpGet("GetUsuarios")]
        public ActionResult<List<JugadorDTO>> GetUsuarios()
        {
            try
            {
                var response = _Service.GetUsuarios();
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

        [HttpGet("GetJugadores")]
        public ActionResult<List<JugadorDTO>> GetListaJugadores()
        {
            try
            {
                var response = _Service.GetListaJugadores();
                if (response.Count == 0)
                {
                    NotFound("No hay Jugadores");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAll", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetJugadoresById/{id}")]
        public ActionResult<JugadorDTO> GetJugadorByNombre([FromRoute] int id)
        {
            try
            {
                var response = _Service.GetJugadorByNombre(id);
                if (response == null)
                {
                    return NotFound($"No se encontro el jugador con el id {id}");
                }

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError("GetAll", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("BorrarJugador/{id}")]
        public ActionResult DeleteJugador([FromRoute] int id) 
        {
            try
            {
                _Service.DeleteJugador(id);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest($"{ex.Message}");
            }
        }



        [HttpGet("GetListaEquipo")]
        public ActionResult<List<EquipoDTO>> GetListaEquipo()
        {
            try
            {
                var response = _Service.GetListaEquipo();
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

        [HttpGet("GetEquipoById/{id}")]
        public ActionResult<EquipoDTO> GetEquipoById([FromRoute] int id)
        {
            try
            {
                var response = _Service.GetEquipoById(id);
                if (response == null)
                {
                    return NotFound($"No se encontro el equipo con el id {id}");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAll", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("BorrarEquipo/{id}")]
        public ActionResult DeleteEquipo([FromRoute] int id)
        {
            try
            {
                _Service.DeleteEquipo(id);
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

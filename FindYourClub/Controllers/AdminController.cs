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
    [Authorize(Roles = "1")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _Service;
        private readonly IFactoryMethJugadores _FactoryMethJugadores;
        private readonly IFactoryMethEquipo _factoryMethEquipo;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService Service, ILogger<AdminController> logger, IFactoryMethJugadores factoryMethJugadores, IFactoryMethEquipo factoryMethEquipo)
        {
            _Service = Service;
            _logger = logger;
            _FactoryMethJugadores = factoryMethJugadores;
            _factoryMethEquipo = factoryMethEquipo;
        }

        [HttpGet("GetUsuarios")]
        public ActionResult<List<UsuarioDTO>> GetUsuarios()
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

        [HttpGet("GetJugadoresById/{nombre}")]
        public ActionResult<JugadorDTO> GetJugadorByNombre([FromRoute] string nombre)
        {
            try
            {
                var response = _Service.GetJugadorByNombre(nombre);
                if (response == null)
                {
                    return NotFound($"No se encontro el jugador con el id {nombre}");
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



        [HttpGet("GetListaEquipoxAdmin")]
        public ActionResult<List<EquipoDTO>> GetListaEquipo()
        {
            try
            {
                var response = _FactoryMethJugadores.GetListaEquipo();
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

        [HttpGet("GetEquipoById/{nombre}")]
        public ActionResult<EquipoDTO> GetEquipoById([FromRoute] string nombre)
        {
            try
            {
                var response = _Service.GetEquipoById(nombre);
                if (response == null)
                {
                    return NotFound($"No se encontro el equipo con el id {nombre}");
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
        [HttpGet("GetContratoListaxAdmin")]
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
        [HttpGet("GetPostulacionListaxAdmin")]
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

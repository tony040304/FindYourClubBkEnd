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

        public AdminController(IAdminService Service)
        {
            _Service = Service;
        }

        
        [HttpGet("GetJugadores")]
        public ActionResult<List<UsuarioDTO>> GetListaJugadores()
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetJugadoresById/{nombre}")]
        public ActionResult<UsuarioDTO> GetJugadorByNombre([FromRoute] string nombre)
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
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpPost("CreateEquipo")]
        public ActionResult<string> CreateEquipo([FromBody] EquipoRegisterDTO equipo)
        {
            try
            {
                var response = _Service.CreateEquipo(equipo);
                if (response == "Equipo existente")
                {
                    return BadRequest(response);
                }
                return Ok("Creado correctamente");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetEquipo")]
        public ActionResult<EquipoDTO> GetEquipo()
        {
            try
            {
                var response = _Service.GetEquipo();
                if (response == null)
                {
                    return NotFound("No hay equipos");
                }
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

       
        [HttpGet("GetEquipoByName/{nombre}")]
        public ActionResult<EquipoDTO> GetEquipoByName([FromRoute] string nombre)
        {
            try
            {
                var response = _Service.GetEquipoByName(nombre);
                if (response == null)
                {
                    return NotFound($"No se encontro el equipo con el id {nombre}");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
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
                return BadRequest($"{ex.Message}");
            }
        }
        
        [HttpGet("GetContratoListaxAdmin")]
        public ActionResult<List<ContratoDTO>> ContratoList()
        {
            try
            {
                var response = _Service.ContratoList();
                if (response.Count == 0)
                {
                    NotFound("No hay usuarios");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetContratoByName")]
        public ActionResult<List<ContratoDTO>> GetContratoByName(string nombre)
        {
            try
            {
                var response = _Service.GetContratoByName(nombre);
                if (response.Count == 0)
                {
                    NotFound("No hay contratos con este nombre");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetPostulacionListaxAdmin")]
        public ActionResult<List<PostulacionDTO>> GetListaPostulacion()
        {
            try
            {
                var response = _Service.GetListaPostulacion();
                if (response.Count == 0)
                {
                    NotFound("No hay usuarios");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetPostulacionByName")]
        public ActionResult<List<PostulacionDTO>> GetPostulacionByName(string nombre)
        {
            try
            {
                var response = _Service.GetPostulacionByName(nombre);
                if (response.Count == 0)
                {
                    NotFound("No hay postulaciones con este nombre");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("BorrarPostulacion/{id}")]
        public ActionResult DeletePostulacion([FromRoute] int id)
        {
            try
            {
                _Service.DeletePostulacion(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpDelete("BorrarContrato/{id}")]
        public ActionResult DeleteContrato([FromRoute] int id)
        {
            try
            {
                _Service.DeleteContrato(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

    }
}

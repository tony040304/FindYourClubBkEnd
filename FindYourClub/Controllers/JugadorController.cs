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
    [Authorize(Roles = "2")]
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorServices _services;


        public JugadorController(IJugadorServices services)
        {
            _services = services;
        }


        [HttpGet("ListaEquipo")]
        public ActionResult<List<EquipoViewModel>> GetEquipo()
        {
            try
            {
                var id = User.FindFirst("NameIdentifier")?.Value;


                var response = _services.GetEquipo(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CrearPostulacionJugador")]
        public ActionResult<string> CrearPostulaciones([FromBody] PostulacionViewModel postu)
        {
            string response = string.Empty;
            try
            {
                var userId = User.FindFirst("NameIdentifier")?.Value;

                response = _services.CrearPostulaciones(postu, userId);
                if (response == "Este equipo no existe" || response == "Ya has realizado una postulación para este equipo")
                    return BadRequest(response);
                return Ok("Postulacion creada");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }            
        }
        [HttpGet("MisPostulaciones")]
        public ActionResult<TeamPostulacionDTO> MisPostulaciones()
        {
            try
            {
                var id = User.FindFirst("NameIdentifier")?.Value;
                var response = _services.MisPostulaciones(id);
                if (response == null)
                {
                    NotFound("No tiene postulaciones");
                }
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("MiEquipo")]
        public ActionResult<List<ContratoJugadorDTO>> MiContrato()
        {
            try
            {
                var id = User.FindFirst("NameIdentifier")?.Value;

                var response = _services.MiContrato(id);
                if(response == null)
                {
                    return BadRequest();
                }
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPatch("CambiarContraseña")]
        public ActionResult<string> ChangePassword(ChangePasswordViewModel password)
        {
            string response = string.Empty;
            try
            {
                var UsuerId = User.FindFirst("NameIdentifier")?.Value.ToString();
                response = _services.ChangePassword(password, UsuerId);
                if (response == "Credenciales incorrectas" || response == "Contraseñas diferentes")
                {
                    return BadRequest(response);
                }
                return Ok("Contraseñas cambiada");
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("BorrarPostulacion/(id)")]
        public ActionResult DeletePostulacion(int id)
        {
            try
            {
                _services.DeletePostulacion(id);
                return Ok("Borrado correctamente");
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("BorrarContrato/{id}")]
        public ActionResult DeleteContrato([FromRoute] int id)
        {
            try
            {
                _services.DeleteContrato(id);
                return Ok("Borrado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}

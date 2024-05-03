using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.DTOS;
using Model.ViewModel;
using Service.IServices;
using System.Security.Claims;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "3")]
    public class EquipoController : ControllerBase
    {
        private readonly IEquipService _Equipo;


        public EquipoController(IEquipService Equipo)
        {
            _Equipo = Equipo;
        }

        [HttpPost("CrearContrato")]
        public ActionResult<string> CrearContrato([FromBody] ContratoViewModel contrato, int idUser)
        {
            string response = string.Empty;
            try
            {
                
                var teamId = User.FindFirst("NameIdentifier")?.Value;

                // Crear el contrato en el servicio
                response = _Equipo.CrearContrato(contrato, teamId, idUser);

                if (response == "El equipo ya tiene un contrato con esta persona" || response == "Este equipo no existe")
                    return BadRequest(response);

                return Ok("Creado correctamente");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }

        }
        [HttpGet("GetContratoListaxEquipo")]
        public ActionResult<List<ContratoEquipoDTO>> ContratoList()
        {
            try
            {
                var id = User.FindFirst("NameIdentifier")?.Value;
                
                var response = _Equipo.ContratoList(id);
                if (response == null)
                {
                    NotFound($"No hay Postulaciones con el equipo(id): {id}");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetPostulacionListaxEquipo")]
        public ActionResult<List<UserPostulacionDTO>> GetPostulacionbyTeam()
        {
            try
            {
                var id = User.FindFirst("NameIdentifier")?.Value;


                var response = _Equipo.GetPostulacionbyTeam(int.Parse(id));
                if (response == null)
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
        [HttpGet("GetPlantel")]
        public ActionResult<List<JugadoresEquipoDTO>> GetPlantel()
        {
            try
            {
                var id = User.FindFirst("NameIdentifier")?.Value;


                var response = _Equipo.GetPlantel(id);
                if (response == null)
                {
                    NotFound("No hay jugadores");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateInfo")]
        public ActionResult<string> UpdateInfo(EquipoViewModel equipo)
        {
            
            try
            {
                var equipoid = User.FindFirst("NameIdentifier")?.Value.ToString();
                
                
                string response = _Equipo.UpdateInfo(equipo, equipoid);
                if (response == "Credenciales incorrectas")
                {
                    return ValidationProblem(response);
                }
                return Ok("Equipo actualizado");
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPatch("Changepassword")]
        public ActionResult<string> PasswordChange(ChangePasswordViewModel password)
        {
            string response = string.Empty;
            try
            {
                var equipoid = User.FindFirst("NameIdentifier")?.Value.ToString();

                response = _Equipo.PasswordChange(password, equipoid);
                if(response == "Credenciales incorrectas" ||  response == "Contraseñas diferentes")
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
        [HttpDelete("BorrarPostulacion/{id}")]
        public ActionResult DeletePostulacion([FromRoute] int id)
        {
            try
            {
                _Equipo.DeletePostulacion(id);
                return Ok("Borrado correctamente");
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
                _Equipo.DeleteContrato(id);
                return Ok("Borrado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}

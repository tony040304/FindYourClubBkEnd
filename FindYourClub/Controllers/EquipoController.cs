using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using Service.Services;
using Service.Services.FactoryMehod;
using System.Security.Claims;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "3")]
    public class EquipoController : ControllerBase
    {
        private readonly IEquipService _Equipo;
        private readonly FindYourClubContext _context;

        public EquipoController(IEquipService Equipo, FindYourClubContext context)
        {
            _Equipo = Equipo;
            _context = context;
        }

        [HttpPost("CrearContratoPrimera")]
        public IActionResult ContratoPrimera([FromBody] ContratoViewModel contrato, int idUser)
        {
            string response = string.Empty;
            try
            {
                
                var teamId = User.FindFirst("NameIdentifier")?.Value;
                var contratoPrimera = new ContratoPrimera(_context);
                // Crear el contrato en el servicio
                response = contratoPrimera.TipoContrato(contrato, teamId, idUser);
                _Equipo.DeletePostulacionAfterContract(idUser);

                if (response == "El equipo ya tiene un contrato con esta persona" || response == "Este jugador no existe")
                {
                    return BadRequest(response); 
                }

                return Ok("Creado correctamente");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }

        }
        [HttpPost("CrearContratoReserva")]
        public IActionResult ContratoReserva([FromBody] ContratoViewModel contrato, int idUser)
        {
            string response = string.Empty;
            try
            {

                var teamId = User.FindFirst("NameIdentifier")?.Value;
                var contratoReserva = new ContratoReserva(_context);
                // Crear el contrato en el servicio
                response = contratoReserva.TipoContrato(contrato, teamId, idUser);
                _Equipo.DeletePostulacionAfterContract(idUser);

                if (response == "El equipo ya tiene un contrato con esta persona" || response == "Este jugador no existe")
                {
                    return BadRequest(response);
                }

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
                    return NotFound($"No hay Postulaciones con el equipo(id): {id}");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al procesar la solicitud");
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
        [HttpPatch("UpdateInfo")]
        public ActionResult UpdateInfo(EquipoViewModel equipo)
        {
            
            try
            {
                var equipoid = User.FindFirst("NameIdentifier")?.Value;
                
                
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
        [HttpGet("MyTeam")]
        public ActionResult<List<EquipoDTO>> MyData()
        {
            try 
            {
                var id = User.FindFirstValue("NameIdentifier")?.ToString();
                var response = _Equipo.MyData(id);
                if (response == null)
                {
                    return NotFound("Error al mostrar el equipo");
                }
                return Ok(response);
            } catch
            {
                return BadRequest();
            }
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using Service.IServices;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService service, ILogger<AuthController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("register")]
        public ActionResult<string> Register([FromBody] UsuarioDTO User)
        {
            string response = string.Empty;
            try
            {
                response = _service.Register(User);
                if (response == "ingrese un usuario" || response == "Usuario existente")   
                    return BadRequest(response);
                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError("Cree usuario", ex);
                return BadRequest($"{ex.Message}");
            }

        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] AuthViewModel User)
        {
            string response = string.Empty;
            try
            {
                response = _service.Login(User);
                if (string.IsNullOrEmpty(response))
                {
                    return NotFound("Nombre/Contraseña incorrecta");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Login error: {ex}");
                return BadRequest($"{ex.Message}");
            }

            return Ok(response);
        }

    }
}

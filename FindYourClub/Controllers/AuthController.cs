using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOS;
using Model.Models;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Usuarios user = new Usuarios();

        [HttpPost("register")]
        public ActionResult<Usuarios> Register(UsuarioDTO request)
        {
            string ContraseniaHash 
                = BCrypt.Net.BCrypt.HashPassword(request.Contrasenia);

            user.Nombre = request.Nombre;
            user.Contrasenia = ContraseniaHash;

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<Usuarios> Login(UsuarioDTO request)
        {
            if (user.Nombre != request.Nombre)
            {
                return BadRequest("Usuario no necontrado");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Contrasenia, user.Contrasenia))
            {
                return BadRequest("Contrasenia incorrecta");
            }

            return Ok(user);
        }

    }
}

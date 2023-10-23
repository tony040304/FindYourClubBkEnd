using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.DTOS;
using Model.Helper;
using Model.Models;
using Model.ViewModel;
using Service.Helper;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthServices : IAuthService
    {
        private readonly FindYourClubContext _context;
        private readonly AppSttings _sttings;

        public AuthServices(FindYourClubContext context, IOptions<AppSttings> appSetings)
        {
            _context = context;
            _sttings = appSetings.Value;
        }


        public string Register(UsuarioDTO User)
        {
            if (string.IsNullOrEmpty(User.Nombre))
            {
                return "Ingrese un usuario";
            }

            Usuarios? user = _context.Usuarios.FirstOrDefault(x => x.Nombre == User.Nombre);

            if (user != null)
            {
                return "Usuario existente";
            }

            _context.Usuarios.Add(new Usuarios()
            {
                Nombre = User.Nombre,
                Contrasenia = User.Contrasenia.GetSHA256(),
                Rol = User.Rol,
                Email = User.Email
            });
            _context.SaveChanges();

            string response = GetToken(_context.Usuarios.OrderBy(x => x.UsuarioId).Last());

            return response;
        }

        public string Login(AuthViewModel User)
        {
            Usuarios? user = _context.Usuarios.FirstOrDefault(x => x.Nombre == User.Nombre || x.Contrasenia == User.Password);

            if (user == null)
            {
                return string.Empty;
            }

            return GetToken(user);
        }

        private string GetToken(Usuarios user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_sttings.key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UsuarioId.ToString()),
                        new Claim(ClaimTypes.Name, user.Nombre),     
                        new Claim(ClaimTypes.Role, user.Rol.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}

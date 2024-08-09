using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.DTOS;
using Model.Helper;
using Model.Models;
using Model.ViewModel;
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
        private readonly IConfiguration _settings;

        public AuthServices(FindYourClubContext context, IConfiguration configuration)
        {
            _context = context;
            _settings = configuration;
        }


        public string Register(UsuarioDTO User)
        {
            if (string.IsNullOrEmpty(User.NombreApellido))
            {
                return "Ingrese un usuario";
            }

            Usuarios? user = _context.Usuarios.FirstOrDefault(x => x.NombreApellido.Trim().ToLower() == User.NombreApellido.Trim().ToLower());
            Equipo? equipo = _context.Equipo.FirstOrDefault(x => x.Nombre.Trim().ToLower() == User.NombreApellido.Trim().ToLower());

            if (user != null || equipo != null)
            {
                return "Usuario existente";
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(User.Contrasenia);

            _context.Usuarios.Add(new Usuarios()
            {
                NombreApellido = User.NombreApellido,
                Contrasenia = hashedPassword,  // Almacena la contraseña encriptada
                Posicion = User.Posicion.TrimEnd(),
                Email = User.Email,
                FechaNacimiento = User.FechaNacimiento
            });
            _context.SaveChanges();

            string response = GetToken(_context.Usuarios.OrderBy(x => x.UsuarioId).Last());

            return response;
        }

        public string Login(AuthViewModel User)
        {
            Usuarios? user = _context.Usuarios.FirstOrDefault(x => x.NombreApellido == User.Nombre);

            if (user != null && BCrypt.Net.BCrypt.Verify(User.Password, user.Contrasenia))
            {
                return GetToken(user);
            }

            Equipo? equipo = _context.Equipo.FirstOrDefault(x => x.Nombre == User.Nombre);

            if (equipo != null && BCrypt.Net.BCrypt.Verify(User.Password, equipo.Password))
            {
                return GetToken(equipo);
            }

            return string.Empty;
        }


        private string GetToken(Usuarios user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings["AppSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("NameIdentifier", user.UsuarioId.ToString()));
            claimsForToken.Add(new Claim("Nombre", user.NombreApellido));
            claimsForToken.Add(new Claim("Email", user.Email));
            claimsForToken.Add(new Claim("role", user.Rol.ToString()));

            var Sectoken = new JwtSecurityToken(_settings["AppSettings:Issuer"],
              _settings["AppSettings:Issuer"],
              claimsForToken,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            return token;
            
        }
        private string GetToken(Equipo equipo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings["AppSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("NameIdentifier", equipo.EquipoId.ToString()));
            claimsForToken.Add(new Claim("Nombre", equipo.Nombre));
            claimsForToken.Add(new Claim("Liga", equipo.Liga));
            claimsForToken.Add(new Claim("role", equipo.RolEquipo.ToString()));

            var Sectoken = new JwtSecurityToken(_settings["AppSettings:Issuer"],
              _settings["AppSettings:Issuer"],
              claimsForToken,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            return token;

        }
    }
}

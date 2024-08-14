using AutoMapper;
using Model.DTOS;
using Model.Enum;
using Model.Models;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Services
{
    public class AdminSerevice : IAdminService
    {
        private readonly FindYourClubContext _context;
        private readonly IMapper _mapper;

        public AdminSerevice(FindYourClubContext _context)
        {
            this._context = _context;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<UsuarioDTO> GetListaJugadores()
        {
            return _context.Usuarios.Where(x=>x.Rol == 2).ToList().Select(s => new UsuarioDTO() { UsuarioId = s.UsuarioId, NombreApellido = s.NombreApellido, FechaNacimiento = s.FechaNacimiento, Contrasenia = s.Contrasenia,Posicion = s.Posicion, Email = s.Email }).ToList();
        }

        public UsuarioDTO GetJugadorByNombre(string nombre)
        {
            return _mapper.Map<UsuarioDTO>(_context.Usuarios.Where(e => e.NombreApellido == nombre).First());
        }

        public void DeleteJugador(int id)
        {
            _context.Usuarios.Remove(_context.Usuarios.Single(f => f.UsuarioId == id));
            _context.SaveChanges();
        }
        public string CreateEquipo(EquipoRegisterDTO equipo)
        {
            Usuarios? user = _context.Usuarios.FirstOrDefault(x => x.NombreApellido == equipo.Nombre.Trim().ToLower());
            Equipo? equipo1 = _context.Equipo.FirstOrDefault(x => x.Nombre == equipo.Nombre.Trim().ToLower());

            if (user != null || equipo1 != null)
            {
                return "Equipo existente";
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(equipo.Password);
            _context.Equipo.Add(new Equipo()
            {
                Nombre = equipo.Nombre,
                Descripcion = equipo.Descripcion,
                Liga = equipo.Liga,
                PosiciónRequerida = equipo.PosiciónRequerida,
                Password = hashedPassword
            });
            _context.SaveChanges();

            var response = _context.Equipo.OrderBy(c=>c.EquipoId).Last();
            return response.ToString();
        }

        public List<EquipoDTO> GetEquipo()
        {
            return _mapper.Map<List<EquipoDTO>>(_context.Equipo.ToList());
        }

        public EquipoDTO GetEquipoByName(string nombre)
        {
            return _mapper.Map<EquipoDTO>(_context.Equipo.Where(e => e.Nombre == nombre).First());
        }

        public void DeleteEquipo(int id)
        {
            _context.Equipo.Remove(_context.Equipo.Single(f => f.EquipoId == id));
            _context.SaveChanges();
        }

        public List<ContratoEquipoUserDTO> ContratoList()
        {
            var query = from e in _context.Equipo
                        join c in _context.Contrato on e.EquipoId equals c.ContEquipoid
                        join u in _context.Usuarios on c.ContUserid equals u.UsuarioId
                        select new ContratoEquipoUserDTO
                        {
                            id = c.Id,
                            NombreEquipo = e.Nombre,
                            NombreApellido = u.NombreApellido,
                            Posicion = u.Posicion,
                            Liga = e.Liga,
                            FechaContrato = c.FechaContrato,
                            SalarioJugador = c.SalarioJugador
                        };
            return query.ToList();
        }
        public List<ContratoEquipoUserDTO> GetContratoByName(string nombre)
        {
            var resultado = from c in _context.Contrato
                            join e in _context.Equipo on c.ContEquipoid equals e.EquipoId
                            join u in _context.Usuarios on c.ContUserid equals u.UsuarioId
                            where e.Nombre == nombre || u.NombreApellido == nombre
                            select new ContratoEquipoUserDTO
                            {
                                id = c.Id,
                                NombreEquipo = e.Nombre,
                                NombreApellido = u.NombreApellido,
                                Posicion = u.Posicion,
                                Liga = e.Liga,
                                FechaContrato = c.FechaContrato,
                                SalarioJugador = c.SalarioJugador
                            };

            return resultado.ToList();
        }
        public List<PostulacionUserTeamDTO> GetListaPostulacion()
        {
            var resultado = from e in _context.Equipo
                            join p in _context.Postulacion on e.EquipoId equals p.PostuEquipoId
                            join u in _context.Usuarios on p.PostuJugadorId equals u.UsuarioId
                            select new PostulacionUserTeamDTO
                            {
                                Id = p.Idpostulacion,
                                Nombre = e.Nombre,
                                NombreApellido = u.NombreApellido,
                                PosisionJugador = u.Posicion,
                                Liga = e.Liga,
                                FechaPostulaciones = p.FechaPostulacion,
                            };

            return resultado.ToList();
        }
        public List<PostulacionUserTeamDTO> GetPostulacionByName(string nombre)
        {
            var contrato = from p in _context.Postulacion
                           join e in _context.Equipo
                           on p.PostuEquipoId equals e.EquipoId
                           join u in _context.Usuarios
                           on p.PostuJugadorId equals u.UsuarioId
                           where e.Nombre == nombre || u.NombreApellido == nombre
                           select new PostulacionUserTeamDTO
                           {
                               Id = p.Idpostulacion,
                               Nombre = e.Nombre,
                               NombreApellido = u.NombreApellido,
                               PosisionJugador = u.Posicion,
                               Liga = e.Liga,
                               FechaPostulaciones = p.FechaPostulacion,
                           };
            return contrato.ToList();
        }
        public void DeletePostulacion(int id)
        {
            _context.Postulacion.Remove(_context.Postulacion.Single(f => f.Idpostulacion == id));
            _context.SaveChanges();
        }
        public void DeleteContrato(int id)
        {
            _context.Contrato.Remove(_context.Contrato.Single(f => f.Id == id));
            _context.SaveChanges();
        }
    }
}

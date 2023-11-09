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

        public List<JugadorDTO> GetListaJugadores()
        {
            return _context.Jugador.ToList().Select(s => new JugadorDTO() { UsuarioId = s.UsuarioId, JugadorId = s.JugadorId, Nombre = s.Nombre, Apellido = s.Apellido, Descripcion = s.Descripcion, Posicion = s.Posicion }).ToList();
        }

        public JugadorDTO GetJugadorByNombre(string nombre)
        {
            return _mapper.Map<JugadorDTO>(_context.Jugador.Where(e => e.Nombre == nombre).First());
        }

        public void DeleteJugador(int id)
        {
            _context.Jugador.Remove(_context.Jugador.Single(f => f.JugadorId == id));
            _context.SaveChanges();
        }


        public EquipoDTO GetEquipoById(string nombre)
        {
            return _mapper.Map<EquipoDTO>(_context.Equipo.Where(e => e.Nombre == nombre).First());
        }

        public void DeleteEquipo(int id)
        {
            _context.Equipo.Remove(_context.Equipo.Single(f => f.EquipoId == id));
            _context.SaveChanges();
        }

        public List<UsuarioDTO> GetUsuarios()
        {
            return _context.Usuarios.ToList().Select(s => new UsuarioDTO() { Nombre = s.Nombre, Email = s.Nombre, Rol = (int)s.Rol }).ToList();
        }

    }
}

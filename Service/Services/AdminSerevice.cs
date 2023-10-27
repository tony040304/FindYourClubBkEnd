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
            return _context.Jugador.ToList().Select(s=> new JugadorDTO() { UsuarioId = s.UsuarioId, JugadorId=s.JugadorId, Nombre=s.Nombre, Apellido=s.Apellido, Descripcion=s.Descripcion, Posicion=s.Posicion}).ToList();
        }

        public JugadorDTO GetJugadorByNombre(int id)
        {
            return _mapper.Map<JugadorDTO>(_context.Jugador.Where(e => e.UsuarioId == id).First());
        }

        public void DeleteJugador(int id)
        {
            _context.Usuarios.Remove(_context.Usuarios.Single(f => f.UsuarioId == id));
            _context.SaveChanges();
        }

        public List<EquipoDTO> GetListaEquipo()
        {
            return _context.Equipo.ToList().Select(s => new EquipoDTO() { UsuarioId = s.UsuarioId, EquipoId = s.EquipoId, Nombre = s.Nombre, Descripcion = s.Descripcion}).ToList();
        }

        public EquipoDTO GetEquipoById(int id)
        {
            return _mapper.Map<EquipoDTO>(_context.Equipo.Where(e => e.UsuarioId == id).First());
        }

        public void DeleteEquipo(int id)
        {
            _context.Usuarios.Remove(_context.Usuarios.Single(f => f.UsuarioId == id));
            _context.SaveChanges();
        }
    }
}

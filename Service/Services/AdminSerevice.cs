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
            return _mapper.Map<List<JugadorDTO>>(_context.Usuarios.Where(e => e.Rol == (int)RolesEnum.Jugador).ToList());

        }

        public JugadorDTO GetJugadorByNombre(int id)
        {
            return _mapper.Map<JugadorDTO>(_context.Usuarios.Where(e => e.UsuarioId == id).First());
        }

        public void DeleteJugador(int id)
        {
            _context.Usuarios.Remove(_context.Usuarios.Single(f => f.UsuarioId == id));
            _context.SaveChanges();
        }

        public List<EquipoDTO> GetListaEquipo()
        {
            return _mapper.Map<List<EquipoDTO>>(_context.Usuarios.Where(e => e.Rol == (int)RolesEnum.Equipo).ToList());
        }

        public EquipoDTO GetEquipoById(int id)
        {
            return _mapper.Map<EquipoDTO>(_context.Usuarios.Where(e => e.UsuarioId == id).First());
        }

        public void DeleteEquipo(int id)
        {
            _context.Usuarios.Remove(_context.Usuarios.Single(f => f.UsuarioId == id));
            _context.SaveChanges();
        }
    }
}

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
            return _context.Usuarios.ToList().Select(s => new UsuarioDTO() { UsuarioId = s.UsuarioId, Nombre = s.Nombre, Email = s.Nombre, Rol = (int)s.Rol, Contrasenia = s.Contrasenia }).ToList();
        }

        public string CrearContrato(ContratoDTO contrato)
        {
            Contrato? contrato1 = _context.Contrato.FirstOrDefault(x => x.Id == contrato.Id);

            if (contrato1 != null)
            {
                return "Contrato existente";
            }


            if (contrato.UsuEquipoId == null || contrato.UsuJugadorId == null)
            {
                return "Falta id equipo o id jugador";
            }

            _context.Contrato.Add(new Contrato()
            {
                UsuEquipoId = contrato.UsuEquipoId,
                UsuJugadorId = contrato.UsuJugadorId,
                SalarioJugador = contrato.SalarioJugador,
                FechaContrato = contrato.FechaContrato
            });
            _context.SaveChanges();

            string lastContrato = _context.Contrato.OrderBy(x => x.Id).Last().ToString();

            return lastContrato;
        }

        public List<ContratoDTO> ContratoList()
        {
            return _context.Contrato.ToList().Select(s => new ContratoDTO() { UsuEquipoId = s.UsuEquipoId, UsuJugadorId = s.UsuJugadorId, FechaContrato = s.FechaContrato, SalarioJugador = s.SalarioJugador, Id = s.Id }).ToList();
        }
        public List<PostulacionDTO> GetListaPostulacion()
        {
            return _context.Postulacion.ToList().Select(s => new PostulacionDTO() { UsuEquipoId = s.UsuEquipoId, UsuJugadorId = s.UsuJugadorId, FechaPostulacion = s.FechaPostulacion, Idpostulacion = s.Idpostulacion }).ToList();
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

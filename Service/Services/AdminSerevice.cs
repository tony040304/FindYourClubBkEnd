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
            return _context.Usuarios.ToList().Select(s => new UsuarioDTO() { UsuarioId = s.UsuarioId, NombreApellido = s.NombreApellido, Posicion = s.Posicion, Email = s.Email }).ToList();
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
            _context.Equipo.Add(new Equipo()
            {
                Nombre = equipo.Nombre,
                Descripcion = equipo.Descripcion,
                Liga = equipo.Liga,
                PosiciónRequerida = equipo.PosiciónRequerida,
                Password = equipo.Password
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

        public List<ContratoDTO> ContratoList()
        {
            return _context.Contrato.ToList().Select(s => new ContratoDTO() { ContEquipoid = s.ContEquipoid, ContUserid = s.ContUserid, FechaContrato = s.FechaContrato, SalarioJugador = s.SalarioJugador, Id = s.Id }).ToList();
        }
        public List<ContratoDTO> GetContratoByName(string nombre)
        {
            var resultado = from c in _context.Contrato
                            join e in _context.Equipo on c.ContEquipoid equals e.EquipoId
                            join u in _context.Usuarios on c.ContUserid equals u.UsuarioId
                            where e.Nombre == nombre || u.NombreApellido == nombre
                            select new ContratoDTO{
                                Id = c.Id,
                                FechaContrato = c.FechaContrato,
                                SalarioJugador = c.SalarioJugador,
                                ContEquipoid = c.ContEquipoid,
                                ContUserid= c.ContUserid,
                            };

            return resultado.ToList();
        }
        public List<PostulacionDTO> GetListaPostulacion()
        {
            return _context.Postulacion.ToList().Select(s => new PostulacionDTO() { PostuEquipoId = s.PostuEquipoId, PostuJugadorId = s.PostuJugadorId, FechaPostulacion = s.FechaPostulacion, Idpostulacion = s.Idpostulacion }).ToList();
        }
        public List<PostulacionDTO> GetPostulacionByName(string nombre)
        {
            var contrato = from c in _context.Postulacion
                           join e in _context.Equipo
                           on c.PostuEquipoId equals e.EquipoId
                           join u in _context.Usuarios
                           on c.PostuJugadorId equals u.UsuarioId
                           where e.Nombre == nombre || u.NombreApellido == nombre
                           select new PostulacionDTO
                           {
                               Idpostulacion = c.Idpostulacion,
                               FechaPostulacion = c.FechaPostulacion,
                               PostuEquipoId = c.PostuEquipoId,
                               PostuJugadorId = c.PostuJugadorId
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

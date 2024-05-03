using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class JugadorServices : IJugadorServices
    {
        private readonly FindYourClubContext _context;
        private readonly IMapper _mapper;

        public JugadorServices(FindYourClubContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }


        public string CrearPostulaciones(PostulacionViewModel postu, string id)
        {
            int userId = int.Parse(id);

            var equipo = _context.Equipo.FirstOrDefault(e => e.Nombre == postu.Nombre);
            if (equipo == null)
            {
                return "Este equipo no existe";
            }
            int equipoId = equipo.EquipoId;

            var postulacionExistente = _context.Postulacion
                                                .FirstOrDefault(p => p.PostuJugadorId == userId && p.PostuEquipoId == equipoId);
            if (postulacionExistente != null)
            {
                return "Ya has realizado una postulación para este equipo";
            }

            _context.Postulacion.Add(new Postulacion()
            {
                PostuJugadorId = userId,
                PostuEquipoId = equipoId,
                FechaPostulacion = DateTime.Now,
            });
            _context.SaveChanges();

            string lastPostu = _context.Postulacion.OrderBy(x => x.Idpostulacion).Last().ToString();

            return lastPostu;
        }

        public List<ContratoJugadorDTO> MiContrato(string id)
        {
            int userId = int.Parse(id);
            var contrato = from e in _context.Equipo
                           join c in _context.Contrato on e.EquipoId equals c.ContEquipoid
                           where c.ContUserid == userId
                           select new ContratoJugadorDTO
                           {
                               idContrato = c.Id,
                               NombreEquipo = e.Nombre,
                               Liga = e.Liga,
                               SalarioJugador = c.SalarioJugador,
                               FechaContrato = c.FechaContrato
                           };
            return contrato.ToList();
        }

        public List<EquipoViewModel> GetEquipo(string id)
        {
            int userId = int.Parse(id);
            var equiposNoPostuladosNiContratados = from equipo in _context.Equipo
                                                   where !(from postulacion in _context.Postulacion
                                                           where postulacion.PostuJugadorId == userId
                                                           select postulacion.PostuEquipoId)
                                                           .Contains(equipo.EquipoId)
                                                         &&
                                                         !(from contrato in _context.Contrato
                                                           where contrato.ContUserid == userId
                                                           select contrato.ContEquipoid)
                                                           .Contains(equipo.EquipoId)
                                                   select new EquipoViewModel
                                                   {
                                                       Nombre = equipo.Nombre,
                                                       Liga = equipo.Liga,
                                                       Descripcion = equipo.Descripcion,
                                                       PosiciónRequerida = equipo.PosiciónRequerida
                                                   };

            return equiposNoPostuladosNiContratados.ToList();
        }

        public List<TeamPostulacionDTO> MisPostulaciones(string id)
        {
            int userId = int.Parse(id);
            var response = from p in _context.Postulacion
                           join e in _context.Equipo on p.PostuEquipoId equals e.EquipoId
                           join u in _context.Usuarios on p.PostuJugadorId equals u.UsuarioId
                           where p.PostuJugadorId == userId
                           select new TeamPostulacionDTO
                           {
                               Id = p.Idpostulacion,
                               Nombre = e.Nombre,
                               Posicion = e.PosiciónRequerida,
                               Liga = e.Liga,
                               FechaPostulaciones = (DateTime)p.FechaPostulacion
                           };
            return response.ToList();
        }
        public string ChangePassword(ChangePasswordViewModel password, string id)
        {
            Usuarios? usuarios = _context.Usuarios.Where(c=>c.UsuarioId == int.Parse(id)).FirstOrDefault();

            if (usuarios == null)
            {
                return "Credenciales incorrectas";
            }

            if (password.Password != password.CheckPassword)
            {
                return "Contraseñas diferentes";
            }

            usuarios.Contrasenia = password.Password;
            _context.SaveChanges();
            return "ok";
        }
        public void DeletePostulacion(int id)
        {
            _context.Postulacion.Remove(_context.Postulacion.Single(x=>x.Idpostulacion == id));
            _context.SaveChanges();
        }
        public void DeleteContrato(int id)
        {
            _context.Contrato.Remove(_context.Contrato.Single(f => f.Id == id));
            _context.SaveChanges();
        }
    }
}

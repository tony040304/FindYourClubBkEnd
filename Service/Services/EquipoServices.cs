using AutoMapper;

using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Services
{
    public class EquipoServices : IEquipService
    {
        private readonly FindYourClubContext _context;
        private readonly IMapper _mapper;

        public EquipoServices(FindYourClubContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public string CrearContrato(ContratoViewModel contrato, string teamId, int idUser)
        {
            int id = int.Parse(teamId);
            var User = _context.Usuarios.FirstOrDefault(e => e.UsuarioId == idUser);
            if (User == null)
            {
                return "Este jugador no existe";
            }
            int UsuarioId = User.UsuarioId;

            var contratoExistente = _context.Contrato.FirstOrDefault(c => c.ContEquipoid == id && c.ContUserid == UsuarioId);
            if (contratoExistente != null)
            {
                return "El equipo ya tiene un contrato con esta persona";
            }
            

            _context.Contrato.Add(new Contrato()
            {
                ContEquipoid = id,
                ContUserid = UsuarioId,
                SalarioJugador = contrato.SalarioJugador,
                FechaContrato = DateTime.Now
            });
            _context.SaveChanges();

            _context.Postulacion.Remove(_context.Postulacion.Single(f => f.PostuJugadorId == idUser));
            _context.SaveChanges();

            string lastContrato = _context.Contrato.OrderBy(x => x.Id).Last().ToString();

            return lastContrato;
        }

        public List<ContratoEquipoDTO> ContratoList(string id)
        {
            int equipoId = int.Parse(id);
            var query = from contrato in _context.Contrato
                        join usuario in _context.Usuarios on contrato.ContUserid equals usuario.UsuarioId
                        where contrato.ContEquipoid == equipoId
                        select new ContratoEquipoDTO
                        {
                            Id = contrato.Id,
                            SalarioJugador = contrato.SalarioJugador,
                            FechaContrato = contrato.FechaContrato,
                            NombreApellido = usuario.NombreApellido,
                            Posicion = usuario.Posicion
                        };

            var resultados = query.ToList();
            return resultados;
        }
        public List<UserPostulacionDTO> GetPostulacionbyTeam(int UsuEquipoId)
        {
            var query = from postulacion in _context.Postulacion
                        join usuarios in _context.Usuarios on postulacion.PostuJugadorId equals usuarios.UsuarioId
                        where postulacion.PostuEquipoId == UsuEquipoId
                        select new UserPostulacionDTO
                        {
                            idUser = usuarios.UsuarioId,
                            NombreApellido = usuarios.NombreApellido,
                            PosisionJugador = usuarios.Posicion,
                            idPostulacion = postulacion.Idpostulacion,
                            FechaPostulaciones = postulacion.FechaPostulacion
                        };

            return query.ToList();
        }
        public List<JugadoresEquipoDTO> GetPlantel(string id)
        {
            var resultado = from c in _context.Contrato
                            join e in _context.Equipo on c.ContEquipoid equals e.EquipoId
                            join u in _context.Usuarios on c.ContUserid equals u.UsuarioId
                            where e.EquipoId == int.Parse(id)
                            select new JugadoresEquipoDTO
                            {
                                NombreJugador = u.NombreApellido,
                                Posicion = u.Posicion,
                                edad = DateTime.Today.Year - ((DateTime)u.FechaNacimiento).Year -
                               (DateTime.Today.Month < ((DateTime)u.FechaNacimiento).Month ||
                               (DateTime.Today.Month == ((DateTime)u.FechaNacimiento).Month &&
                               DateTime.Today.Day < ((DateTime)u.FechaNacimiento).Day) ? 1 : 0)
                            };
            return resultado.ToList();
        }
        public string UpdateInfo(EquipoViewModel equipo, string id)
        {
            Equipo? equipoId = _context.Equipo.Where(x=>x.EquipoId==int.Parse(id)).FirstOrDefault();

            if (equipoId == null)
            {
                return "Credenciales incorrectas";
            }

            equipoId.Liga = equipo.Liga;
            equipoId.PosiciónRequerida = equipo.PosiciónRequerida;
            equipoId.Descripcion = equipo.Descripcion;

            _context.SaveChanges();

            return "ok";
        }
        public string PasswordChange(ChangePasswordViewModel password, string id)
        {
            Equipo? equipoId = _context.Equipo.Where(x => x.EquipoId == int.Parse(id)).FirstOrDefault();

            if (equipoId == null)
            {
                return "Credenciales incorrectas";
            }

            if (password.Password != password.CheckPassword)
            {
                return "Contraseñas diferentes";
            }

            equipoId.Password = password.Password;
            _context.SaveChanges();
            return "ok";
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

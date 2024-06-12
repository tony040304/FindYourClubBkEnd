using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.FactoryMehod
{
    public class ContratoReserva : ContratoBase
    {
        public ContratoReserva(FindYourClubContext context) : base(context)
        {
        }
        public override string TipoContrato(ContratoViewModel contrato, string teamId, int idUser)
        {
            int id = int.Parse(teamId);
            Usuarios? User = _context.Usuarios.FirstOrDefault(e => e.UsuarioId == idUser);
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
                CategoriaEquipo = "Equipo reserva",
                FechaContrato = DateTime.Now
            });
            _context.SaveChanges();


            string lastContrato = _context.Contrato.OrderByDescending(x => x.Id).FirstOrDefault().ToString();


            return lastContrato;
        }
        public override List<JugadoresEquipoDTO> GetPlantel(string id)
        {
            var resultado = from c in _context.Contrato
                            join e in _context.Equipo on c.ContEquipoid equals e.EquipoId
                            join u in _context.Usuarios on c.ContUserid equals u.UsuarioId
                            where e.EquipoId == int.Parse(id) && c.CategoriaEquipo == "Equipo reserva"
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
    }
}

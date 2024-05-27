using Model.Models;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.FactoryMehod
{
    public class ContratoPrimera : ContratoBase
    {
        public ContratoPrimera(FindYourClubContext context) : base(context)
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

            string categoria = "Primera divicion";
            _context.Contrato.Add(new Contrato()
            {
                ContEquipoid = id,
                ContUserid = UsuarioId,
                SalarioJugador = contrato.SalarioJugador,
                CategoriaEquipo = categoria,
                FechaContrato = DateTime.Now
            });
            _context.SaveChanges();


            string lastContrato = _context.Contrato.OrderByDescending(x => x.Id).FirstOrDefault().ToString();

            return lastContrato;
        }
    }
}

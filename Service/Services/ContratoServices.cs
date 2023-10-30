using Model.DTOS;
using Model.Models;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ContratoServices : IContratoServices
    {
        private readonly FindYourClubContext _context;

        public ContratoServices(FindYourClubContext context)
        {
            _context = context;
        }

        public string CrearContrato(ContratoDTO contrato)
        {
            Contrato? contrato1 = _context.Contrato.FirstOrDefault(x => x.ContratoId == contrato.ContratoId);

            if (contrato1 != null)
            {
                return "Contrato existente";
            }


            if (contrato.EquipoId == null || contrato.JugadorId == null)
            {
                return "Falta id equipo o id jugador";
            }

            _context.Contrato.Add(new Contrato()
            {
                EquipoId = contrato.EquipoId,
                JugadorId = contrato.JugadorId,
                Salario = contrato.Salario,
                Fecha = contrato.Fecha,
                MontoTraspaso = contrato.MontoTraspaso
            });
            _context.SaveChanges();

            string lastContrato = _context.Contrato.OrderBy(x => x.ContratoId).Last().ToString();

            return lastContrato;
        }
    }
}

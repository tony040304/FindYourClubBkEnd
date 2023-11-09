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
    public class FactoryEquipoServices : IFactoryMethEquipo
    {
        private readonly FindYourClubContext _context;

        public FactoryEquipoServices(FindYourClubContext context)
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

        public List<ContratoDTO> ContratoList()
        {
            return _context.Contrato.ToList().Select(s => new ContratoDTO() { EquipoId = s.EquipoId, JugadorId = s.JugadorId, Fecha = s.Fecha, MontoTraspaso = s.MontoTraspaso, Salario = s.Salario, ContratoId = s.ContratoId }).ToList();
        }
        public List<PostulacionDTO> GetListaPostulacion()
        {
            return _context.Postulacion.ToList().Select(s => new PostulacionDTO() { Idequipo = s.Idequipo, Idjugador = s.Idjugador, FechaPostulacion = s.FechaPostulacion, PostulacionId = s.PostulacionId  }).ToList();
        }
        public void DeletePostulacion(int id)
        {
            _context.Postulacion.Remove(_context.Postulacion.Single(f => f.PostulacionId == id));
            _context.SaveChanges();
        }
        public void DeleteContrato(int id)
        {
            _context.Contrato.Remove(_context.Contrato.Single(f => f.ContratoId == id));
            _context.SaveChanges();
        }

    }
}

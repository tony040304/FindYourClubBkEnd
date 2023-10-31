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
    public class EquipoPostulaciones : IFactoryPostulacion
    {
        private readonly FindYourClubContext _context;

        public EquipoPostulaciones(FindYourClubContext context)
        {
            _context = context;
        }
        public string CrearPostulaciones(PostulacionDTO postu)
        {
            Postulacion? postulacion = _context.Postulacion.FirstOrDefault(x => x.PostulacionId == postu.PostulacionId);

            if (postulacion != null)
            {
                return "Contrato existente";
            }


            if (postu.Idequipo == null || postu.Idjugador == null)
            {
                return "Falta id equipo o id jugador";
            }

            _context.Postulacion.Add(new Postulacion()
            {
                PostulacionId = postu.PostulacionId,
                Idequipo = postu.Idequipo,
                Idjugador = postu.Idjugador,
                FechaPostulacion = postu.FechaPostulacion
            });
            _context.SaveChanges();

            string lastPostu = _context.Postulacion.OrderBy(x => x.PostulacionId).Last().ToString();

            return lastPostu;
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public string CrearPostulaciones(PostulacionDTO postu)
        {
            Postulacion? postulacion = _context.Postulacion.FirstOrDefault(x => x.PostulacionId == postu.PostulacionId);

            if (postulacion != null)
            {
                return "postulacion existente";
            }

            if (postu.Idequipo == null || postu.Idjugador == 0)
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

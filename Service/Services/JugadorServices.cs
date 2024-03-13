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


        public string CrearPostulaciones(PostulacionDTO postu)
        {
            Postulacion? postulacion = _context.Postulacion.FirstOrDefault(x => x.Idpostulacion == postu.Idpostulacion);

            if (postulacion != null)
            {
                return "postulacion existente";
            }

            _context.Postulacion.Add(new Postulacion()
            {
                Idpostulacion = postu.Idpostulacion,
                UsuEquipoId = postu.UsuEquipoId,
                UsuJugadorId = postu.UsuJugadorId,
                FechaPostulacion = postu.FechaPostulacion
            });
            _context.SaveChanges();

            string lastPostu = _context.Postulacion.OrderBy(x => x.Idpostulacion).Last().ToString();

            return lastPostu;
        }

        

    }
}

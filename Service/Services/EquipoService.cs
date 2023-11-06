using AutoMapper;
using Model.DTOS;
using Model.Models;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EquipoService : IEquipoService
    {
        private readonly FindYourClubContext _context;
        private readonly IMapper _mapper;

        public EquipoService(FindYourClubContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public string InsertarDatosEquipo(EquipoDTO equipo)
        {
            if (string.IsNullOrEmpty(equipo.Nombre))
            {
                return "ingrese nombre";
            }

            Equipo? equipo1 = _context.Equipo.FirstOrDefault(x => x.Nombre == equipo.Nombre);

            if (equipo1 != null)
            {
                return "Equipo existente";
            }

            

            _context.Equipo.Add(new Equipo()
            {
                EquipoId = equipo.EquipoId,
                UsuarioId = equipo.UsuarioId,
                Nombre = equipo.Nombre,
                Descripcion = equipo.Descripcion,
                PosiciónRequerida = equipo.PosiciónRequerida
             });
            _context.SaveChanges();

            string lastEquipo = _context.Equipo.OrderBy(x => x.EquipoId).Last().ToString();

            return lastEquipo;
        }

      
    }
}

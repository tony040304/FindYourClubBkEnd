using AutoMapper;
using Model.DTOS;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping.Profiles
{
    public class EquipoProfile : Profile
    {
        public EquipoProfile()
        {
            CreateMap<Equipo, EquipoDTO>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<List<Equipo>, List<EquipoDTO>>()
                .ConvertUsing(src => src.Select(e => new EquipoDTO { Nombre = e.Nombre, EquipoId = e.EquipoId }).ToList());

        }
    }
}

using AutoMapper;
using Model.DTOS;
using Model.Models;
using Model.ViewModel;
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
                .ConvertUsing(src => src.Select(e => new EquipoDTO { Nombre = e.Nombre, EquipoId = e.EquipoId, Descripcion = e.Descripcion, Liga = e.Liga, PosiciónRequerida = e.PosiciónRequerida, Password = e.Password }).ToList());

            CreateMap<List<Equipo>, List<EquipoViewModel>>()
                .ConvertUsing(src => src.Select(e => new EquipoViewModel { Nombre = e.Nombre,  Descripcion = e.Descripcion, Liga = e.Liga, PosiciónRequerida = e.PosiciónRequerida }).ToList());

        }
    }
}

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
    public class JugadorProfile : Profile
    {
        public JugadorProfile()
        {
            CreateMap<Jugador, JugadorDTO>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<List<Jugador>, List<JugadorDTO>>()
                .ConvertUsing(src => src.Select(e => new JugadorDTO { Nombre = e.Nombre, JugadorId = e.JugadorId }).ToList());

        }
    }
}

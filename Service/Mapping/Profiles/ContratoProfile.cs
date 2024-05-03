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
    public class ContratoProfile : Profile
    {
        public ContratoProfile()
        {
            CreateMap<Contrato, ContratoDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<List<Contrato>, List<ContratoDTO>>()
                .ConvertUsing(src => src.Select(e => new ContratoDTO { Id = e.Id, ContEquipoid = e.ContEquipoid, ContUserid = e.ContUserid, FechaContrato = e.FechaContrato, SalarioJugador = e.SalarioJugador }).ToList());

        }
    }
}

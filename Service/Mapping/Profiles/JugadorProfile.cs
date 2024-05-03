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
            CreateMap<Usuarios, UsuarioDTO>()
                .ForMember(dest => dest.NombreApellido, opt => opt.MapFrom(src => src.NombreApellido));

            CreateMap<List<Usuarios>, List<UsuarioDTO>>()
                .ConvertUsing(src => src.Select(e => new UsuarioDTO { NombreApellido = e.NombreApellido, UsuarioId = e.UsuarioId, Email = e.Email, Posicion = e.Posicion }).ToList());

        }
    }
}

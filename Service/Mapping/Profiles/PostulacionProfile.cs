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
    public class PostulacionProfile : Profile
    {
        public PostulacionProfile() 
        {
            CreateMap<Postulacion, PostulacionDTO>()
                .ForMember(dest => dest.Idpostulacion, opt => opt.MapFrom(src => src.Idpostulacion));

            CreateMap<List<Postulacion>, List<PostulacionDTO>>()
                .ConvertUsing(src => src.Select(e => new PostulacionDTO { Idpostulacion = e.Idpostulacion }).ToList());
        }
    }
}

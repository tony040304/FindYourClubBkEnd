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

      
    }
}

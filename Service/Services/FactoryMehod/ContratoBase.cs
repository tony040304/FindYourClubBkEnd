using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using Service.IServices.FactoryMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.FactoryMehod
{
    public abstract class ContratoBase : IContratoFactory
    {
        protected readonly FindYourClubContext _context;

        public ContratoBase(FindYourClubContext context)
        {
            _context = context;
        }
        public abstract string TipoContrato(ContratoViewModel contrato, string teamId, int idUser);
        public abstract List<JugadoresEquipoDTO> GetPlantel(string id);
    }
}

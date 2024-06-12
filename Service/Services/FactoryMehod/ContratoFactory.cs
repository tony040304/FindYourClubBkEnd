using Model.Models;
using Service.IServices.FactoryMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.FactoryMehod
{
    public static class ContratoFactory
    {
        public static IContratoFactory CrearContrato(bool esPrimeraDivision, FindYourClubContext context)
        {
            if (esPrimeraDivision)
            {
                return new ContratoPrimera(context);
            }
            else
            {
                return new ContratoReserva(context);
            }
        }
        public static IContratoFactory TraerContrato(bool esPrimeraDivision, FindYourClubContext context)
        {
            if (esPrimeraDivision)
            {
                return new ContratoPrimera(context);
            }
            else
            {
                return new ContratoReserva(context);
            }
        }
    }
}

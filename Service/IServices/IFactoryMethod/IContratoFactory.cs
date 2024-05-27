using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices.FactoryMethod
{
    public interface IContratoFactory
    {
        string TipoContrato(ContratoViewModel contrato, string teamId, int idUser);
    }
}

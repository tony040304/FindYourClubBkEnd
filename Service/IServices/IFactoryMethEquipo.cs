using Model.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IFactoryMethEquipo
    {
        string CrearContrato(ContratoDTO contrato);
        List<ContratoDTO> ContratoList();
        List<PostulacionDTO> GetListaPostulacion();
        void DeletePostulacion(int id);
        void DeleteContrato(int id);
    }
}

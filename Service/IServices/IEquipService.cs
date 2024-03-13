using Model.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IEquipService
    {
        string CrearContrato(ContratoDTO contrato);
        ContratoDTO ContratoList(int id);
        List<JugadorPostulacionDTO> GetPostulacionbyTeam(int UsuEquipoId);
        void DeletePostulacion(int id);
        void DeleteContrato(int id);
    }
}

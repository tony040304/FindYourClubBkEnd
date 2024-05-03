using Model.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IAdminService
    {
        List<UsuarioDTO> GetListaJugadores();
        UsuarioDTO GetJugadorByNombre(string nombre);
        void DeleteJugador(int id);
        string CreateEquipo(EquipoRegisterDTO equipo);
        List<EquipoDTO> GetEquipo();
        EquipoDTO GetEquipoByName(string nombre);
        void DeleteEquipo(int  id);
        List<ContratoDTO> ContratoList();
        List<ContratoDTO> GetContratoByName(string nombre);              
        List<PostulacionDTO> GetListaPostulacion();
        List<PostulacionDTO> GetPostulacionByName(string nombre);
        void DeletePostulacion(int id);
        void DeleteContrato(int id);
    }
}

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
        List<JugadorDTO> GetListaJugadores();
        JugadorDTO GetJugadorByNombre(string nombre);
        void DeleteJugador(int id);

        EquipoDTO GetEquipoById(string nombre);
        void DeleteEquipo(int  id);
        List<UsuarioDTO> GetUsuarios();
    }
}

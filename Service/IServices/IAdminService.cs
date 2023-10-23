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
        JugadorDTO GetJugadorByNombre(int id);
        void DeleteJugador(int id);

        List<EquipoDTO> GetListaEquipo();
        EquipoDTO GetEquipoById(int id);
        void DeleteEquipo(int  id);
    }
}

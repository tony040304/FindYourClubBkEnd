using Model.DTOS;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IJugadorServices
    {
        List<EquipoViewModel> GetEquipo(string id);
        string CrearPostulaciones(PostulacionViewModel postu, string id);
        List<TeamPostulacionDTO> MisPostulaciones(string id);
        List<ContratoJugadorDTO> MiContrato(string id);
        string ChangePassword(ChangePasswordViewModel password, string id);
        void DeletePostulacion(int id);
        void DeleteContrato(int id);

    }
}

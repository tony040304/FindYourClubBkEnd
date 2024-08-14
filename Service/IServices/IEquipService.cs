using Microsoft.AspNetCore.Http;
using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IEquipService
    {
        void DeletePostulacionAfterContract(int idUser);
        List<ContratoEquipoDTO> ContratoList(string id);
        List<UserPostulacionDTO> GetPostulacionbyTeam(int UsuEquipoId);
        string UpdateInfo(EquipoViewModel equipo, string id);
        string PasswordChange(ChangePasswordViewModel password, string id);
        void DeletePostulacion(int id);
        void DeleteContrato(int id);
        List<EquipoDTO> MyData(string id);
    }
}

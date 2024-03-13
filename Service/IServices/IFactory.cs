using Model.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IFactory
    {
        string InsertarDatosJugador(JugadorDTO jugador);
        string InsertarDatosEquipo(EquipoDTO equipo);
    }
}

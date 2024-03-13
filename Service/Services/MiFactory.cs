using Microsoft.EntityFrameworkCore;
using Model.DTOS;
using Model.Models;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MiFactory : IFactory
    {
        private readonly FindYourClubContext context;
        public MiFactory(FindYourClubContext context)
        {
            this.context = context;
        }

        public string InsertarDatosJugador(JugadorDTO jugador)
        {
            if (string.IsNullOrEmpty(jugador.Nombre))
            {
                return "ingrese nombre";
            }

            


            Jugador? jugador1 = context.Jugador.FirstOrDefault(x => x.UsuarioId == jugador.UsuarioId);

            if (jugador1 != null)
            {
                return "Jugador existente";
            }

            context.Jugador.Add(new Jugador()
            {
                UsuarioId = jugador.UsuarioId,
                Nombre = jugador.Nombre,
                Apellido = jugador.Apellido,
                Descripcion = jugador.Descripcion,
                Posicion = jugador.Posicion
            });
            context.SaveChanges();

            string lastJugador = context.Jugador.OrderBy(x => x.JugadorId).Last().ToString();

            return lastJugador;
        }
        public string InsertarDatosEquipo(EquipoDTO equipo)
        {
            if (string.IsNullOrEmpty(equipo.Nombre))
            {
                return "ingrese nombre";
            }

            Equipo? equipo1 = context.Equipo.FirstOrDefault(x => x.Nombre == equipo.Nombre);

            if (equipo1 != null)
            {
                return "Equipo existente";
            }
            Equipo? equipoExistente = context.Equipo.FirstOrDefault(x => x.UsuarioId == equipo.UsuarioId);

            if (equipoExistente != null)
            {
                return "Equipo existente";
            }

            context.Equipo.Add(new Equipo()
            {
                EquipoId = equipo.EquipoId,
                UsuarioId = equipo.UsuarioId,
                Nombre = equipo.Nombre,
                Liga = equipo.Liga,
                PosiciónRequerida = equipo.PosiciónRequerida,
                Descripcion = equipo.Descripcion
            });
            context.SaveChanges();

            string lastEquipo = context.Equipo.OrderBy(x => x.EquipoId).Last().ToString();

            return lastEquipo;
        }
    }
}

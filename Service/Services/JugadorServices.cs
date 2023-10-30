using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class JugadorServices : IJugadorServices
    {
        private readonly FindYourClubContext _context;
        private readonly IMapper _mapper;

        public JugadorServices(FindYourClubContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public string InsertarDatos(JugadorDTO jugador)
        {
            if (string.IsNullOrEmpty(jugador.Nombre))
            {
                return "ingrese nombre";
            }

            Jugador? jugador1 = _context.Jugador.FirstOrDefault(x => x.Nombre == jugador.Nombre);

            if (jugador1 != null)
            {
                return "Jugador existente";
            }

            _context.Jugador.Add(new Jugador()
            {
                UsuarioId = jugador.UsuarioId,
                Nombre = jugador.Nombre,
                Apellido = jugador.Apellido,
                Descripcion = jugador.Descripcion,
                Posicion = jugador.Posicion

            });
            _context.SaveChanges();

            string lastJugador = _context.Jugador.OrderBy(x => x.JugadorId).Last().ToString();

            return lastJugador;
        }
    }
}

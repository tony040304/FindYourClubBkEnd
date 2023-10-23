using AutoMapper;
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

        public JugadorDTO InsertarDatos(JugadorViewModel model)
        {


            _context.Jugador.Add(new Jugador()
            {
                UsuarioId = _context.Usuarios.First(f => f.UsuarioId == model.UsuarioId).UsuarioId,
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Descripcion = model.Descripcion,
                Posicion = model.Posicion

            }); ;
            _context.SaveChanges();

            var lastJugador = _context.Jugador.OrderBy(x => x.JugadorId).Last();

            return _mapper.Map<JugadorDTO>(lastJugador);
        }
    }
}

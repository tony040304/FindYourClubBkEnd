using AutoMapper;
using Model.DTOS;
using Model.Models;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Services
{
    public class EquipoServices : IEquipService
    {
        private readonly FindYourClubContext _context;
        private readonly IMapper _mapper;

        public EquipoServices(FindYourClubContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public string CrearContrato(ContratoDTO contrato)
        {
            Contrato? contrato1 = _context.Contrato.FirstOrDefault(x => x.Id == contrato.Id);

            if (contrato1 != null)
            {
                return "Contrato existente";
            }


            if (contrato.UsuEquipoId == null || contrato.UsuJugadorId == null)
            {
                return "Falta id equipo o id jugador";
            }

            _context.Contrato.Add(new Contrato()
            {
                UsuEquipoId = contrato.UsuEquipoId,
                UsuJugadorId = contrato.UsuJugadorId,
                SalarioJugador = contrato.SalarioJugador,
                FechaContrato = contrato.FechaContrato
            });
            _context.SaveChanges();

            string lastContrato = _context.Contrato.OrderBy(x => x.Id).Last().ToString();

            return lastContrato;
        }

        public ContratoDTO ContratoList(int id)
        {
            return _mapper.Map<ContratoDTO>(_context.Contrato.Where(e => e.UsuEquipoId == id).First());
        }
        public List<JugadorPostulacionDTO> GetPostulacionbyTeam(int UsuEquipoId)
        {
            var query = from postulacion in _context.Postulacion
                        join jugador in _context.Jugador on postulacion.UsuJugadorId equals jugador.UsuarioId
                        where postulacion.UsuEquipoId == UsuEquipoId
                        select new JugadorPostulacionDTO
                        {
                            NombreJugador = jugador.Nombre,
                            ApellidoJugador = jugador.Apellido,
                            PosisionJugador = jugador.Posicion,
                            idPostulacion = postulacion.Idpostulacion,
                            UsuJugadorId = jugador.UsuarioId,
                            FechaPostulaciones = postulacion.FechaPostulacion
                        };

            // Ejecutar la consulta y devolver los resultados mapeados
            return query.ToList();
        }
        public void DeletePostulacion(int id)
        {
            _context.Postulacion.Remove(_context.Postulacion.Single(f => f.Idpostulacion == id));
            _context.SaveChanges();
        }
        public void DeleteContrato(int id)
        {
            _context.Contrato.Remove(_context.Contrato.Single(f => f.Id == id));
            _context.SaveChanges();
        }

    }
}

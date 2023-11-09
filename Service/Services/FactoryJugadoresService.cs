using Model.DTOS;
using Model.Models;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class FactoryJugadoresService : IFactoryMethJugadores
    {
        private readonly FindYourClubContext _context;

        public FactoryJugadoresService(FindYourClubContext context)
        {
            _context = context;
        }

        public List<EquipoDTO> GetListaEquipo()
        {
            return _context.Equipo.ToList().Select(s => new EquipoDTO() { UsuarioId = s.UsuarioId, EquipoId = s.EquipoId, Nombre = s.Nombre, Descripcion = s.Descripcion }).ToList();
        }
    }
}

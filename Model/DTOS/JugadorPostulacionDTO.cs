using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class JugadorPostulacionDTO
    {
        public string NombreJugador { get; set; } = string.Empty;
        public string ApellidoJugador { get; set; } = string.Empty;
        public string PosisionJugador { get; set; } = string.Empty;
        public DateTime? FechaPostulaciones { get; set; }
        public int idPostulacion { get; set; }
        public int? UsuJugadorId { get; set; }
    }
}

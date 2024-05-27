using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class ContratoJugadorDTO
    {
        public int idContrato { get; set; }
        public string NombreEquipo { get; set; }
        public string Liga { get; set; }
        public decimal? SalarioJugador { get; set; }
        public DateTime? FechaContrato { get; set; }
        public string CategoriaEquipo { get; set; }
    }
}

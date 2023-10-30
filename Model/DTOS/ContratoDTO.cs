using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class ContratoDTO
    {
        public int ContratoId { get; set; }

        public int? EquipoId { get; set; }

        public decimal? Salario { get; set; }

        public DateTime? Fecha { get; set; }

        public decimal? MontoTraspaso { get; set; }

        public int? JugadorId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class ContratoDTO
    {
        public int Id { get; set; }

        public decimal? SalarioJugador { get; set; }

        public DateTime? FechaContrato { get; set; }

        public int? ContEquipoid { get; set; }

        public int? ContUserid { get; set; }
        public string CategoriaEquipo { get; set; }
    }
}

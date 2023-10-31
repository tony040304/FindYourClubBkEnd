using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class PostulacionDTO
    {
        public int PostulacionId { get; set; }

        public DateTime? FechaPostulacion { get; set; }

        public int? Idjugador { get; set; }

        public int? Idequipo { get; set; }
    }
}

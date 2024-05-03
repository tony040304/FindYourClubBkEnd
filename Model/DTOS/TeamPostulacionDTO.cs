using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class TeamPostulacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Liga { get; set; }
        public string Posicion { get; set; }
        public DateTime FechaPostulaciones { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class PostulacionUserTeamDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Liga { get; set; }
        public string NombreApellido { get; set; }
        public string PosisionJugador { get; set; } = string.Empty;
        public DateTime? FechaPostulaciones { get; set; }
    }
}

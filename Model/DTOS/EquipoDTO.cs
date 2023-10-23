using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class EquipoDTO
    {
        public int EquipoId { get; set; }

        public int? UsuarioId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }
}

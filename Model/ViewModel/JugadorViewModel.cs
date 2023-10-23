using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class JugadorViewModel
    {
        public int JugadorId { get; set; }

        public int? UsuarioId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Descripcion { get; set; }

        public string Posicion { get; set; }
    }
}

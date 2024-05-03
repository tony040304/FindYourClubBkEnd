using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class EquipoRegisterDTO
    {
        public string Nombre { get; set; }
        public string Password { get; set; }

        public string Descripcion { get; set; }

        public string PosiciónRequerida { get; set; }

        public string Liga { get; set; }
    }
}

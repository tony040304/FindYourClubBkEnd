﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class UsuarioDTO
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int? Rol { get; set; }

        public string Contrasenia { get; set; }

    }
}
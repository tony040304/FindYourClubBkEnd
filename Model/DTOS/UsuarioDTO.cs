﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string NombreApellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Contrasenia { get; set; }

        public string Email { get; set; }

        public string Posicion { get; set; }

    }
}

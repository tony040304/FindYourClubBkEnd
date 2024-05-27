﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOS
{
    public class ContratoEquipoDTO
    {
        public int Id { get; set; }

        public decimal? SalarioJugador { get; set; }

        public DateTime? FechaContrato { get; set; }

        public string NombreApellido { get; set; }

        public string Posicion { get; set; }
        public string CategoriaEquipo { get; set; }
    }
}

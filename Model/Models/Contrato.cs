﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class Contrato
{
    public int ContratoId { get; set; }

    public int? UsuarioId { get; set; }

    public int? EquipoId { get; set; }

    public decimal? Salario { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? MontoTraspaso { get; set; }

    public virtual Equipo Equipo { get; set; }

    public virtual Usuarios Usuario { get; set; }
}
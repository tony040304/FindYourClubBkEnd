﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class Equipo
{
    public int EquipoId { get; set; }

    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public string PosiciónRequerida { get; set; }

    public string Liga { get; set; }

    public int? RolEquipo { get; set; }

    public string Password { get; set; }

    public virtual ICollection<Contrato> Contrato { get; set; } = new List<Contrato>();

    public virtual ICollection<Postulacion> Postulacion { get; set; } = new List<Postulacion>();
}
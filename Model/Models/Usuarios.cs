﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class Usuarios
{
    public int UsuarioId { get; set; }

    public string NombreApellido { get; set; }

    public string Contrasenia { get; set; }

    public string Email { get; set; }

    public string Posicion { get; set; }

    public int? Rol { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public virtual ICollection<Contrato> Contrato { get; set; } = new List<Contrato>();

    public virtual ICollection<Postulacion> Postulacion { get; set; } = new List<Postulacion>();
}
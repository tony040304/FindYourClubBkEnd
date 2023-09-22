﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Model.Models.Configurations;
using System;
using System.Collections.Generic;
#nullable disable

namespace Model.Models;

public partial class FindYourClubContext : DbContext
{
    public FindYourClubContext(DbContextOptions<FindYourClubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipo { get; set; }

    public virtual DbSet<Jugador> Jugador { get; set; }

    public virtual DbSet<Mensaje> Mensaje { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.ApplyConfiguration(new Configurations.EquipoConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.JugadorConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.MensajeConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
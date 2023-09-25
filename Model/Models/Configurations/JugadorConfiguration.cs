﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;
using System;
using System.Collections.Generic;

namespace Model.Models.Configurations
{
    public partial class JugadorConfiguration : IEntityTypeConfiguration<Jugador>
    {
        public void Configure(EntityTypeBuilder<Jugador> entity)
        {
            entity.HasKey(e => e.JugadorId).HasName("PK__Jugador__4B575242CB989070");

            entity.Property(e => e.JugadorId).HasColumnName("JugadorID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Posicion).HasMaxLength(50);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Jugador)
            .HasForeignKey(d => d.UsuarioId)
            .HasConstraintName("FK__Jugador__Usuario__5441852A");

            entity.HasMany(d => d.Equipo).WithMany(p => p.Jugador)
            .UsingEntity<Dictionary<string, object>>(
                "JugadorEquipo",
                r => r.HasOne<Equipo>().WithMany()
                    .HasForeignKey("EquipoId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JugadorEq__Equip__5AEE82B9"),
                l => l.HasOne<Jugador>().WithMany()
                    .HasForeignKey("JugadorId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JugadorEq__Jugad__59FA5E80"),
                j =>
                {
                    j.HasKey("JugadorId", "EquipoId").HasName("PK__JugadorE__A6BFF2FD525D7049");
                    j.IndexerProperty<int>("JugadorId").HasColumnName("JugadorID");
                    j.IndexerProperty<int>("EquipoId").HasColumnName("EquipoID");
                });

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Jugador> entity);
    }
}

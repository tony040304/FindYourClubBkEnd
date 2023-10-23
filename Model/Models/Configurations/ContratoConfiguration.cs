﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;
using System;
using System.Collections.Generic;

namespace Model.Models.Configurations
{
    public partial class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> entity)
        {
            entity.HasKey(e => e.ContratoId).HasName("PK__Contrato__B238E953333135DE");

            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.EquipoId).HasColumnName("EquipoID");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.MontoTraspaso).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Salario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Equipo).WithMany(p => p.Contrato)
            .HasForeignKey(d => d.EquipoId)
            .HasConstraintName("FK_Equipo_Contrato");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Contrato)
            .HasForeignKey(d => d.UsuarioId)
            .HasConstraintName("FK_Usuario_Contrato");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Contrato> entity);
    }
}

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
            entity.HasKey(e => e.Id).HasName("PK__contrato__3213E83FB48D71ED");

            entity.ToTable("contrato");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContEquipoid).HasColumnName("Cont_Equipoid");
            entity.Property(e => e.ContUserid).HasColumnName("Cont_Userid");
            entity.Property(e => e.FechaContrato)
            .HasColumnType("date")
            .HasColumnName("fecha_contrato");
            entity.Property(e => e.SalarioJugador)
            .HasColumnType("decimal(10, 2)")
            .HasColumnName("salario_jugador");

            entity.HasOne(d => d.ContEquipo).WithMany(p => p.Contrato)
            .HasForeignKey(d => d.ContEquipoid)
            .HasConstraintName("FK_contrato_Equipo");

            entity.HasOne(d => d.ContUser).WithMany(p => p.Contrato)
            .HasForeignKey(d => d.ContUserid)
            .HasConstraintName("FK_contrato_Usuarios");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Contrato> entity);
    }
}

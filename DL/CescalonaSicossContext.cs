﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class CescalonaSicossContext : DbContext
{
    public CescalonaSicossContext()
    {
    }

    public CescalonaSicossContext(DbContextOptions<CescalonaSicossContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Historial> Historials { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= CEscalonaSicoss; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Historial>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__Historia__9CC7DBB40952ADA5");

            entity.ToTable("Historial");

            entity.Property(e => e.FechaHora).HasColumnType("date");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Historials)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Historial__IdUsu__1367E606");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97B8DB6456");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.UserName, "UQ__Usuario__C9F28456275162F8").IsUnique();

            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

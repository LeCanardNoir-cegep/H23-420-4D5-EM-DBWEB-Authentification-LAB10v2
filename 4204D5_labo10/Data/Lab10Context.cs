using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using _4204D5_labo10.Models;

namespace _4204D5_labo10.Data
{
    public partial class Lab10Context : DbContext
    {
        public Lab10Context()
        {
        }

        public Lab10Context(DbContextOptions<Lab10Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Changelog> Changelogs { get; set; } = null!;
        public virtual DbSet<Chanson> Chansons { get; set; } = null!;
        public virtual DbSet<Chanteur> Chanteurs { get; set; } = null!;
        public virtual DbSet<VwChanteurNbChanson> VwChanteurNbChansons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Lab10");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Changelog>(entity =>
            {
                entity.ToTable("changelog", "Musique");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Checksum)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("checksum");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.InstalledBy)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("installed_by");

                entity.Property(e => e.InstalledOn)
                    .HasColumnType("datetime")
                    .HasColumnName("installed_on")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Success).HasColumnName("success");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Version)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("version");
            });

            modelBuilder.Entity<Chanson>(entity =>
            {
                entity.ToTable("Chanson", "Musique");

                entity.Property(e => e.ChansonId).HasColumnName("ChansonID");

                entity.Property(e => e.ChanteurId).HasColumnName("ChanteurID");

                entity.Property(e => e.Nom).HasMaxLength(100);

                entity.HasOne(d => d.Chanteur)
                    .WithMany(p => p.Chansons)
                    .HasForeignKey(d => d.ChanteurId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Chanson_ChanteurID");
            });

            modelBuilder.Entity<Chanteur>(entity =>
            {
                entity.ToTable("Chanteur", "Musique");

                entity.Property(e => e.ChanteurId).HasColumnName("ChanteurID");

                entity.Property(e => e.DateNaissance).HasColumnType("date");

                entity.Property(e => e.Nom).HasMaxLength(50);
            });

            modelBuilder.Entity<VwChanteurNbChanson>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ChanteurNbChansons", "Musique");

                entity.Property(e => e.ChanteurId).HasColumnName("ChanteurID");

                entity.Property(e => e.DateDeNaissance)
                    .HasColumnType("date")
                    .HasColumnName("Date de naissance");

                entity.Property(e => e.Nom).HasMaxLength(50);

                entity.Property(e => e.NombreDeChansons).HasColumnName("Nombre de chansons");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

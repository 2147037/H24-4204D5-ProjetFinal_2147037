using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjetFinal_2147037.Models;

namespace ProjetFinal_2147037.Data
{
    public partial class ProjetFinal_2147037Context : DbContext
    {
        public ProjetFinal_2147037Context()
        {
        }

        public ProjetFinal_2147037Context(DbContextOptions<ProjetFinal_2147037Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Acteur> Acteurs { get; set; } = null!;
        public virtual DbSet<Adresse> Adresses { get; set; } = null!;
        public virtual DbSet<Changelog> Changelogs { get; set; } = null!;
        public virtual DbSet<Courriel> Courriels { get; set; } = null!;
        public virtual DbSet<EmissionTelevision> EmissionTelevisions { get; set; } = null!;
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<Personnage> Personnages { get; set; } = null!;
        public virtual DbSet<Plateforme> Plateformes { get; set; } = null!;
        public virtual DbSet<Serie> Series { get; set; } = null!;
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; } = null!;
        public virtual DbSet<VwActeurPersonnageEmission> VwActeurPersonnageEmissions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ProjetFinal_2147037");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.HasOne(d => d.Utilisateur)
                    .WithMany(p => p.Adresses)
                    .HasForeignKey(d => d.UtilisateurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Adresse_UtilisateurID");
            });

            modelBuilder.Entity<Changelog>(entity =>
            {
                entity.Property(e => e.InstalledOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Courriel>(entity =>
            {
                entity.HasOne(d => d.Utilisateur)
                    .WithMany(p => p.Courriels)
                    .HasForeignKey(d => d.UtilisateurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Courriel_UtilisateurID");
            });

            modelBuilder.Entity<EmissionTelevision>(entity =>
            {
                entity.HasOne(d => d.Plateforme)
                    .WithMany(p => p.EmissionTelevisions)
                    .HasForeignKey(d => d.PlateformeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmissionTelevision_PlateformeID");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasOne(d => d.EmissionTelevision)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.EmissionTelevisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Film_EmissionTelevisionID");
            });

            modelBuilder.Entity<Personnage>(entity =>
            {
                entity.Property(e => e.EstVivant).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Acteur)
                    .WithMany(p => p.Personnages)
                    .HasForeignKey(d => d.ActeurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Personnage_ActeurID");

                entity.HasOne(d => d.EmissionTelevision)
                    .WithMany(p => p.Personnages)
                    .HasForeignKey(d => d.EmissionTelevisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Personnage_EmissionTelevisionID");
            });

            modelBuilder.Entity<Serie>(entity =>
            {
                entity.HasOne(d => d.EmissionTelevision)
                    .WithMany(p => p.Series)
                    .HasForeignKey(d => d.EmissionTelevisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Serie_EmissionTelevisionID");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.Property(e => e.NoTelephone).IsFixedLength();

                entity.HasOne(d => d.Plateforme)
                    .WithMany(p => p.Utilisateurs)
                    .HasForeignKey(d => d.PlateformeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Utilisateur_PlateformeID");
            });

            modelBuilder.Entity<VwActeurPersonnageEmission>(entity =>
            {
                entity.ToView("vw_ActeurPersonnageEmission", "Personne");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

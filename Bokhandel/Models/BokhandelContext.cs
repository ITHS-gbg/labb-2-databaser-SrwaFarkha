using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bokhandel.Models
{
    public partial class BokhandelContext : DbContext
    {
        public BokhandelContext()
        {
        }

        public BokhandelContext(DbContextOptions<BokhandelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Butiker> Butiker { get; set; } = null!;
        public virtual DbSet<Böcker> Böcker { get; set; } = null!;
        public virtual DbSet<Författare> Författare { get; set; } = null!;
        public virtual DbSet<Kund> Kund { get; set; } = null!;
        public virtual DbSet<LagerSaldo> LagerSaldo { get; set; } = null!;
        public virtual DbSet<Ordrar> Ordrar { get; set; } = null!;
        public virtual DbSet<TitlarPerFörfattare> TitlarPerFörfattare { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-BJNMTCA;Initial Catalog=Bokhandel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Butiker>(entity =>
            {
                entity.HasKey(e => e.ButikId);

                entity.ToTable("Butiker");

                entity.Property(e => e.ButikId)
                    .ValueGeneratedNever()
                    .HasColumnName("ButikID");

                entity.Property(e => e.Adress).HasMaxLength(50);

                entity.Property(e => e.Namn).HasMaxLength(50);

                entity.Property(e => e.Stad).HasMaxLength(50);
            });

            modelBuilder.Entity<Böcker>(entity =>
            {
                entity.HasKey(e => e.Isbn13);

                entity.ToTable("Böcker");

                entity.Property(e => e.Isbn13)
                    .HasMaxLength(13)
                    .HasColumnName("ISBN13");

                entity.Property(e => e.FörfattarId).HasColumnName("FörfattarID");

                entity.Property(e => e.Pris).HasColumnType("smallmoney");

                entity.Property(e => e.Språk).HasMaxLength(50);

                entity.Property(e => e.Titel).HasMaxLength(50);

                entity.Property(e => e.Utgivningsdatum).HasColumnType("date");

                entity.HasOne(d => d.Författar)
                    .WithMany(p => p.Böckers)
                    .HasForeignKey(d => d.FörfattarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Böcker_Författare");
            });

            modelBuilder.Entity<Författare>(entity =>
            {
                entity.ToTable("Författare");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Efternamn).HasMaxLength(50);

                entity.Property(e => e.Födelsedatum).HasColumnType("date");

                entity.Property(e => e.Förnamn).HasMaxLength(50);
            });

            modelBuilder.Entity<Kund>(entity =>
            {
                entity.ToTable("Kund");

                entity.Property(e => e.KundId)
                    .ValueGeneratedNever()
                    .HasColumnName("KundID");

                entity.Property(e => e.Adress).HasMaxLength(50);

                entity.Property(e => e.Efternamn).HasMaxLength(50);

                entity.Property(e => e.Förnamn).HasMaxLength(50);

                entity.Property(e => e.Stad).HasMaxLength(50);
            });

            modelBuilder.Entity<LagerSaldo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LagerSaldo");

                entity.Property(e => e.ButikId).HasColumnName("ButikID");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(13)
                    .HasColumnName("ISBN");

                entity.HasOne(d => d.Butik)
                    .WithMany()
                    .HasForeignKey(d => d.ButikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LagerSaldo_Butiker");

                entity.HasOne(d => d.IsbnNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Isbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LagerSaldo_Böcker");
            });

            modelBuilder.Entity<Ordrar>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("Ordrar");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderID");

                entity.Property(e => e.ButikId).HasColumnName("ButikID");

                entity.Property(e => e.KundId).HasColumnName("KundID");

                entity.Property(e => e.LeveransAdress).HasMaxLength(50);

                entity.Property(e => e.LeveransDatum).HasColumnType("date");

                entity.Property(e => e.OrderDatum).HasColumnType("date");

                entity.Property(e => e.Stad).HasMaxLength(50);

                entity.HasOne(d => d.Butik)
                    .WithMany(p => p.Ordrars)
                    .HasForeignKey(d => d.ButikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ordrar_Butiker");

                entity.HasOne(d => d.Kund)
                    .WithMany(p => p.Ordrars)
                    .HasForeignKey(d => d.KundId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ordrar_Kund");
            });

            modelBuilder.Entity<TitlarPerFörfattare>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TitlarPerFörfattare");

                entity.Property(e => e.Lagervärde).HasColumnType("money");

                entity.Property(e => e.Namn).HasMaxLength(101);

                entity.Property(e => e.Titlar).HasMaxLength(33);

                entity.Property(e => e.Ålder).HasMaxLength(33);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

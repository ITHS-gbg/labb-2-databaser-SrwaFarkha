using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bokhandel.Model
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-BJNMTCA;Database=Bokhandel;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Butiker>(entity =>
            {
                entity.HasKey(e => e.ButikId)
                    .HasName("PK__Butiker__B5D66BFAB32AC8DD");

                entity.ToTable("Butiker");

                entity.Property(e => e.ButikId).HasColumnName("ButikID");

                entity.Property(e => e.Adress).HasMaxLength(55);

                entity.Property(e => e.Namn).HasMaxLength(55);

                entity.Property(e => e.Stad).HasMaxLength(55);
            });

            modelBuilder.Entity<Böcker>(entity =>
            {
                entity.HasKey(e => e.Isbn13)
                    .HasName("PK__Böcker__3BF79E032D482AA6");

                entity.ToTable("Böcker");

                entity.Property(e => e.Isbn13)
                    .HasMaxLength(13)
                    .HasColumnName("ISBN13");

                entity.Property(e => e.FörfattarId).HasColumnName("FörfattarID");

                entity.Property(e => e.Pris).HasColumnType("smallmoney");

                entity.Property(e => e.Språk).HasMaxLength(55);

                entity.Property(e => e.Titel).HasMaxLength(55);

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

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Efternamn).HasMaxLength(55);

                entity.Property(e => e.Födelsedatum).HasColumnType("date");

                entity.Property(e => e.Förnamn).HasMaxLength(55);
            });

            modelBuilder.Entity<Kund>(entity =>
            {
                entity.ToTable("Kund");

                entity.Property(e => e.KundId).HasColumnName("KundID");

                entity.Property(e => e.Adress).HasMaxLength(55);

                entity.Property(e => e.Efternamn).HasMaxLength(55);

                entity.Property(e => e.Förnamn).HasMaxLength(55);

                entity.Property(e => e.Stad).HasMaxLength(55);
            });

            modelBuilder.Entity<LagerSaldo>(entity =>
            {
                entity.ToTable("LagerSaldo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(13)
                    .HasColumnName("ISBN");

                entity.HasOne(d => d.Butik)
                    .WithMany(p => p.LagerSaldos)
                    .HasForeignKey(d => d.ButikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LagerSaldo_Butiker");

                entity.HasOne(d => d.IsbnNavigation)
                    .WithMany(p => p.LagerSaldos)
                    .HasForeignKey(d => d.Isbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LagerSaldo_Böcker");
            });

            modelBuilder.Entity<Ordrar>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Ordrar__C3905BAF8E4ADDFD");

                entity.ToTable("Ordrar");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ButikId).HasColumnName("ButikID");

                entity.Property(e => e.KundId).HasColumnName("KundID");

                entity.Property(e => e.Leveransadress).HasMaxLength(55);

                entity.Property(e => e.Leveransdatum).HasColumnType("date");

                entity.Property(e => e.OrderDatum).HasColumnType("date");

                entity.Property(e => e.Stad).HasMaxLength(55);

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

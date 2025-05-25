using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZLManage.DomainModel.Models;

public partial class ZLContext : DbContext
{
    public ZLContext(DbContextOptions<ZLContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrator { get; set; }

    public virtual DbSet<Klijent> Klijent { get; set; }

    public virtual DbSet<Korisnik> Korisnik { get; set; }

    public virtual DbSet<Let> Let { get; set; }

    public virtual DbSet<Pista> Pista { get; set; }

    public virtual DbSet<Zaposlenik> Zaposlenik { get; set; }

    public virtual DbSet<Zrakoplov> Zrakoplov { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_100_CI_AS_SC_UTF8");

        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.Admin_id).HasName("PK__Administ__4A311D2FBCA65065");

            entity.Property(e => e.Admin_id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Klijent>(entity =>
        {
            entity.HasKey(e => e.Id_korisnika).HasName("PK__Klijent__19BF5D610AF26490");

            entity.Property(e => e.Id_korisnika).ValueGeneratedNever();
            entity.Property(e => e.Tip_klijenta)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Id_korisnikaNavigation).WithOne(p => p.Klijent)
                .HasForeignKey<Klijent>(d => d.Id_korisnika)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Klijent_Korisnik");
        });

        modelBuilder.Entity<Korisnik>(entity =>
        {
            entity.HasKey(e => e.Id_korisnika).HasName("PK__Korisnik__19BF5D6117A72151");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ime)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Prezime)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Let>(entity =>
        {
            entity.HasKey(e => e.Broj_leta).HasName("PK__Let__758C1AA627E25346");

            entity.HasOne(d => d.Id_pisteNavigation).WithMany(p => p.Let)
                .HasForeignKey(d => d.Id_piste)
                .HasConstraintName("FK_Let_Pista");

            entity.HasOne(d => d.Id_zrakoplovaNavigation).WithMany(p => p.Let)
                .HasForeignKey(d => d.Id_zrakoplova)
                .HasConstraintName("FK_Let_Zrakoplov");

            entity.HasOne(d => d.Kreirao_gaNavigation).WithMany(p => p.Let)
                .HasForeignKey(d => d.Kreirao_ga)
                .HasConstraintName("FK_Let_Zaposlenik");
        });

        modelBuilder.Entity<Pista>(entity =>
        {
            entity.HasKey(e => e.Id_piste).HasName("PK__Pista__08186B15F93A5BC8");

            entity.Property(e => e.Oznaka)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Zaposlenik>(entity =>
        {
            entity.HasKey(e => e.Id_zaposlenika).HasName("PK__Zaposlen__176ADAAEAD6ADFE7");

            entity.Property(e => e.Id_zaposlenika).ValueGeneratedNever();
            entity.Property(e => e.Pozicija)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Id_zaposlenikaNavigation).WithOne(p => p.Zaposlenik)
                .HasForeignKey<Zaposlenik>(d => d.Id_zaposlenika)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zaposlenik_Korisnik");

            entity.HasOne(d => d.Kreirao_ga_adminNavigation).WithMany(p => p.Zaposlenik)
                .HasForeignKey(d => d.Kreirao_ga_admin)
                .HasConstraintName("FK_Zaposlenik_Administrator");
        });

        modelBuilder.Entity<Zrakoplov>(entity =>
        {
            entity.HasKey(e => e.Id_zrakoplova).HasName("PK__Zrakoplo__12D4C97B01ADDBEC");

            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Registracija)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

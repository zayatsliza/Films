using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Films
{
    public partial class DBFilmContext : DbContext
    {
        public DBFilmContext()
        {
        }

        public DBFilmContext(DbContextOptions<DBFilmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<FilmGenre> FilmGenres { get; set; }
        public virtual DbSet<FilmMember> FilmMembers { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Position> Positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = DESKTOP-IMLCLRI\\SQLEXPRESS; Database=DBFilm; Trusted_Connection = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.Idfilm);

                entity.Property(e => e.Idfilm).HasColumnName("IDfilm");

                entity.Property(e => e.Descript)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<FilmGenre>(entity =>
            {
                entity.HasKey(e => e.IdfilmGenres);

                entity.Property(e => e.IdfilmGenres).HasColumnName("IDfilmGenres");

                entity.Property(e => e.Idfilm).HasColumnName("IDfilm");

                entity.Property(e => e.Idgenres).HasColumnName("IDgenres");

                entity.HasOne(d => d.IdfilmNavigation)
                    .WithMany(p => p.FilmGenres)
                    .HasForeignKey(d => d.Idfilm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmGenres_Films");

                entity.HasOne(d => d.IdgenresNavigation)
                    .WithMany(p => p.FilmGenres)
                    .HasForeignKey(d => d.Idgenres)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmGenres_Genres1");
            });

            modelBuilder.Entity<FilmMember>(entity =>
            {
                entity.HasKey(e => e.Idfilmem);

                entity.Property(e => e.Idfilmem).HasColumnName("IDfilmem");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DateOfDeath).HasColumnType("date");

                entity.Property(e => e.Idfilm).HasColumnName("IDFilm");

                entity.Property(e => e.Idgender).HasColumnName("IDGender");

                entity.Property(e => e.Idpost).HasColumnName("IDPost");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdfilmNavigation)
                    .WithMany(p => p.FilmMembers)
                    .HasForeignKey(d => d.Idfilm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmMembers_Films");

                entity.HasOne(d => d.IdgenderNavigation)
                    .WithMany(p => p.FilmMembers)
                    .HasForeignKey(d => d.Idgender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmMembers_Genders1");

                entity.HasOne(d => d.IdpostNavigation)
                    .WithMany(p => p.FilmMembers)
                    .HasForeignKey(d => d.Idpost)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmMembers_Positions1");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.Idgender);

                entity.Property(e => e.Idgender).HasColumnName("IDgender");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.Idgenres);

                entity.Property(e => e.Idgenres).HasColumnName("IDgenres");

                entity.Property(e => e.Descript).HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.Idpost);

                entity.Property(e => e.Idpost).HasColumnName("IDpost");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

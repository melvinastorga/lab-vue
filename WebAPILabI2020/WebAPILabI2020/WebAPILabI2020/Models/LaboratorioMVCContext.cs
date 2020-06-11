using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPILabI2020.Models
{
    public partial class LaboratorioMVCContext : DbContext
    {
        public LaboratorioMVCContext()
        {
        }

        public LaboratorioMVCContext(DbContextOptions<LaboratorioMVCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Major> Major { get; set; }
        public virtual DbSet<Nationality> Nationality { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=163.178.173.148;Initial Catalog=2020LabMVC6_B56438; User ID=estudiantesrp;Password=estudiantesrp");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Major>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MajorNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.Major)
                    .HasConstraintName("Major_fk");

                entity.HasOne(d => d.NationalityNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.Nationality)
                    .HasConstraintName("Nationality_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

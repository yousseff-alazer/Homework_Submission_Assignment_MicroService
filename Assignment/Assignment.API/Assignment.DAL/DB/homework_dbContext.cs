using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Assignment.API.Assignment.DAL.DB
{
    public partial class homework_dbContext : DbContext
    {
        public homework_dbContext()
        {
        }

        public homework_dbContext(DbContextOptions<homework_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3310;database=homework_db;user id=root;password=1a456#idgj_5f@sj*du7fg78@;treattinyasboolean=false", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.22-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.ToTable("assignment");

                entity.UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.GradeId, "Fk_Assignment_Grade_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FileAttachment)
                    .HasMaxLength(250)
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.GradeId).HasColumnType("bigint(20)");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(250)
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.SubmissionDate).HasColumnType("datetime");

                entity.Property(e => e.TeachersNote)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("Fk_Assignment_Grade");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("grade");

                entity.UseCollation("utf8_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .UseCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

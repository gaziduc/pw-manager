using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PWManager.Models
{
    public partial class PWManagerContext : DbContext
    {
        public PWManagerContext()
        {
        }

        public PWManagerContext(DbContextOptions<PWManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ServiceCredential> ServiceCredentials { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\MTI;Database=PWManager;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ServiceCredential>(entity =>
            {
                entity.ToTable("service_credentials");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.IsFavorite).HasColumnName("is_favorite");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Url)
                    .HasMaxLength(2048)
                    .HasColumnName("url");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ServiceCredentials)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_service_credentials_categories");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ServiceCredentials)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_service_credentials_users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Window_Services_SendEmail.DataModels
{
    public partial class imagesdbContext : DbContext
    {
        public imagesdbContext()
        {
        }

        public imagesdbContext(DbContextOptions<imagesdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BirthdayUser> BirthdayUser { get; set; }
        public virtual DbSet<ImgRecords> ImgRecords { get; set; }
        public virtual DbSet<UserData> UserData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=PCA172\\SQL2017;Database=imagesdb;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=sa;Password=Tatva@123;Integrated Security=False;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BirthdayUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("birthday_user");

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImgRecords>(entity =>
            {
                entity.HasKey(e => e.Rno)
                    .HasName("PK__imgRecor__C2B7F59BA7656BBF");

                entity.ToTable("imgRecords");

                entity.Property(e => e.Rno).HasColumnName("rno");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasColumnType("text");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Userno).HasColumnName("userno");
            });

            modelBuilder.Entity<UserData>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__userData__B9BE370FE61B6211");

                entity.ToTable("userData");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.ExpirationTime).HasColumnType("datetime");

                entity.Property(e => e.RequestToken).HasColumnName("Request_token");

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasMaxLength(50);

                entity.Property(e => e.UserPassword)
                    .HasColumnName("user_password")
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

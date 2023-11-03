using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace CatDogLoverManagement.Repository.Models
{
    public partial class CatDogLoveManagementContext : DbContext
    {
        public CatDogLoveManagementContext()
        {
        }

        public CatDogLoveManagementContext(DbContextOptions<CatDogLoveManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; } = null!;
        public virtual DbSet<BlogPost> BlogPosts { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<TimeFrame> TimeFrames { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
            var strConn = config["ConnectionStrings:CatDogLoverManagementDb"];

            return strConn;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.ToTable("Animal");

                entity.Property(e => e.AnimalId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AnimalName).HasMaxLength(100);

                entity.Property(e => e.AnimalType).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(100);
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("PK__BlogPost__AA1260180C400C7B");

                entity.ToTable("BlogPost");

                entity.Property(e => e.PostId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.BlogPosts)
                    .HasForeignKey(d => d.AnimalId)
                    .HasConstraintName("FK__BlogPost__Animal__59063A47");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.BlogPosts)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__BlogPost__Servic__59FA5E80");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BlogPosts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__BlogPost__UserId__5812160E");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CommentMessage).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__PostId__5EBF139D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__UserId__5DCAEF64");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.BookedDate).HasColumnType("datetime");

                entity.Property(e => e.BookedTime).HasMaxLength(200);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AnimalId)
                    .HasConstraintName("FK_Order_Animal");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.OrderBuyers)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__BuyerId__6383C8BA");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.OrderSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__SellerId__6477ECF3");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_Order_Service");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.ServiceId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Note).HasMaxLength(100);

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ServiceName).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<TimeFrame>(entity =>
            {
                entity.ToTable("TimeFrame");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.TimeFrames)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TimeFrame__Servi__6D0D32F4");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.TransactionId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Order__693CA210");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username).HasMaxLength(100);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__RoleId__4E88ABD4");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__UserRole__8AFACE1A5FEC9F06");

                entity.ToTable("UserRole");

                entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.RoleName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

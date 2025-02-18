using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStoreApp.API.Data
{
    public partial class BookstoreContext : DbContext
    {
        public BookstoreContext()
        {
        }

        public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Bookstore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Author)
                    .HasMaxLength(255)
                    .HasColumnName("author");

                entity.Property(e => e.AvailabilityStatus)
                    .HasMaxLength(20)
                    .HasColumnName("availability_status");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Genre)
                    .HasMaxLength(255)
                    .HasColumnName("genre");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("image_url");

                entity.Property(e => e.Inventory).HasColumnName("inventory");

                entity.Property(e => e.OriginPrice).HasColumnName("origin_price");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("cart_items");

                entity.Property(e => e.CartItemId).HasColumnName("cart_item_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__cart_item__book___48CFD27E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__cart_item__user___47DBAE45");
            });



            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__orders__user_id__412EB0B6");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");

                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__order_ite__book___44FF419A");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__order_ite__order__440B1D61");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStoreApp.API.Data
{
    public partial class BookstoreContext : IdentityDbContext<ApiUser>
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
            base.OnModelCreating(modelBuilder);

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
                entity.Property(e => e.UserId)
            .HasMaxLength(450);

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

              
            });



            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.Property(e => e.UserId)
            .HasMaxLength(450);

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("user_id");

              
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

          

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "8343074e-8623-4e1a-b0c1-84fb8678c8f3"
                },
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = "c7ac6cfe-1f10-4baf-b604-cde350db9554"
                }
            );

            var hasher = new PasswordHasher<ApiUser>();

            modelBuilder.Entity<ApiUser>().HasData(
                new ApiUser
                {
                    Id = "8e448afa-f008-446e-a52f-13c449803c2e",
                    Email = "admin@bookstore.com",
                    NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                    UserName = "admin@bookstore.com",
                    NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    ConcurrencyStamp = "bad07111-7087-481a-a61d-a1eebf986a8e",
                    SecurityStamp = "bfeae509-d4e8-4100-bca8-0139cb4383fd",
                    PasswordHash = "AQAAAAIAAYagAAAAEPwU7seKUtkPq9EFERgu6gaJLuFK0o4YxwdVWaJEA4ZAya3V/YSHnCMDYc+PgNJpRg==",
                },
                new ApiUser
                {
                    Id = "30a24107-d279-4e37-96fd-01af5b38cb27",
                    Email = "user@bookstore.com",
                    NormalizedEmail = "USER@BOOKSTORE.COM",
                    UserName = "user@bookstore.com",
                    NormalizedUserName = "USER@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "User",
                    ConcurrencyStamp = "bad07111-7087-481a-a61d-a1eebf986a8e",
                    SecurityStamp = "bfeae509-d4e8-4100-bca8-0139cb4383fd",
                    PasswordHash = "AQAAAAIAAYagAAAAECtdDCtDFAM5xsOc0bJ7PUePlcYjz9vadQHV243cwfT1E4HgV3n6W93XCe0xvjUSWw==",
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "8343074e-8623-4e1a-b0c1-84fb8678c8f3",
                    UserId = "30a24107-d279-4e37-96fd-01af5b38cb27"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "c7ac6cfe-1f10-4baf-b604-cde350db9554",
                    UserId = "8e448afa-f008-446e-a52f-13c449803c2e"
                }
            );


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

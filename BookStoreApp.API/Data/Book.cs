using System;
using System.Collections.Generic;

namespace BookStoreApp.API.Data
{
    public partial class Book
    {
        public Book()
        {
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public int? Price { get; set; }
        public string? AvailabilityStatus { get; set; }
        public int? OriginPrice { get; set; }
        public int? Inventory { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}

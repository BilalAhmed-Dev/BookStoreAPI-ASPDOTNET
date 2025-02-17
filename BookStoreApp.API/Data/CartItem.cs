using System;
using System.Collections.Generic;

namespace BookStoreApp.API.Data
{
    public partial class CartItem
    {
        public int CartItemId { get; set; }
        public string? UserId { get; set; }
        public int? BookId { get; set; }
        public int? Amount { get; set; }

        public virtual Book? Book { get; set; }
        public virtual User? User { get; set; }
    }
}

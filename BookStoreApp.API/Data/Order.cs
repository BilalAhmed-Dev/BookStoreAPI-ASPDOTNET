using System;
using System.Collections.Generic;

namespace BookStoreApp.API.Data
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}

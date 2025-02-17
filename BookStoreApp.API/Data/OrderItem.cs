using System;
using System.Collections.Generic;

namespace BookStoreApp.API.Data
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int? OrderId { get; set; }
        public int? BookId { get; set; }
        public int? Amount { get; set; }
        public int? Price { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Order? Order { get; set; }
    }
}

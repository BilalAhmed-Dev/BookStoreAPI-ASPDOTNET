using System;
using System.Collections.Generic;

namespace BookStoreApp.API.Data
{
    public partial class JwtBlacklist
    {
        public long Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
    }
}

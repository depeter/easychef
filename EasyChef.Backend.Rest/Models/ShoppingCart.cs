using System;
using System.Collections.Generic;

namespace EasyChef.Backend.Rest.Models
{
    public class ShoppingCart
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public IList<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public DateTime? LastScan { get; set; }
        public User User { get; set; }
    }
}

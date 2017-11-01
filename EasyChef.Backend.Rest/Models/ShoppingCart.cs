using System;
using System.Collections.Generic;

namespace EasyChef.Backend.Rest.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        
        public IList<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public DateTime? LastScan { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}

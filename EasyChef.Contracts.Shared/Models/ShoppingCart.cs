using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChef.Shared.Models
{
    public class ShoppingCart
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public IList<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyChef.Backend.Rest.Models
{
    public class ShoppingCartProduct
    {
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public int ShoppingCartId { get; set; }
    }
}

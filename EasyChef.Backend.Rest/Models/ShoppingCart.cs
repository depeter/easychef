using System.Collections.Generic;

namespace EasyChef.Backend.Rest.Models
{
    public class ShoppingCart
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public IList<Backend.Rest.Models.Product> Products { get; set; }
    }
}

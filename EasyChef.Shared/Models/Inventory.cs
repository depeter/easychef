using System.Collections.Generic;

namespace EasyChef.Shared.Models
{
    public class Inventory
    {
        public IList<Product> ProductsInStock { get; set; }
        public HouseHold HouseHold { get; set; }
    }
}

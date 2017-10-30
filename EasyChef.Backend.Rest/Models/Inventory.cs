using System.Collections.Generic;
using EasyChef.Shared.Models;

namespace EasyChef.Backend.Rest.Models
{
    public class Inventory
    {
        public IList<Product> ProductsInStock { get; set; }
        public Backend.Rest.Models.HouseHold HouseHold { get; set; }
    }
}

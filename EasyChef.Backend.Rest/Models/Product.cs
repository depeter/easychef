using System;
using System.Collections.Generic;

namespace EasyChef.Backend.Rest.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string UnitPrice { get; set; }
        public string Unit { get; set; }
        public DateTime? LastScan { get; set; }

        public virtual IList<ShoppingCartProduct> ShoppingCartProduct { get; set; }
    }
}

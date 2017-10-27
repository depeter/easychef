using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyChef.Shared.Models
{
    public class Inventory
    {
        public IList<Product> ProductsInStock { get; set; }

        [ForeignKey("HouseHoldId")]
        public HouseHold HouseHold { get; set; }

        [ForeignKey("HouseHold")]
        public long HouseHoldId { get; set; }
    }
}

using System.Collections.Generic;
using EasyChef.Shared.Models;

namespace EasyChef.Contracts.Shared.Models
{
    public class InventoryDTO
    {
        public IList<ProductDTO> ProductsInStock { get; set; }
        public HouseHoldDTO HouseHoldDto { get; set; }
    }
}

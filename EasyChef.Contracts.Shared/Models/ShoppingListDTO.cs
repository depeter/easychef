using System.Collections.Generic;

namespace EasyChef.Contracts.Shared.Models
{
    public class ShoppingListDTO
    {
        public IList<ProductDTO> Products { get; set; }
    }
}

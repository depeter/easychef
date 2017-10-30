using System.Collections.Generic;

namespace EasyChef.Contracts.Shared.Models
{
    public class ShoppingCartDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public IList<ProductDTO> Products { get; set; }
    }
}

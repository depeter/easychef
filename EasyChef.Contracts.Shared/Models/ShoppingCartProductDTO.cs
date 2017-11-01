using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EasyChef.Contracts.Shared.Models
{
    public class ShoppingCartProductDTO
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public ShoppingCartDTO ShoppingCart { get; set; }
        public ProductDTO Product { get; set; }
    }
}

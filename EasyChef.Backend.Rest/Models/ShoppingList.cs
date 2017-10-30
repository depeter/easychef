using System.Collections.Generic;

namespace EasyChef.Backend.Rest.Models
{
    public class ShoppingList
    {
        public IList<Backend.Rest.Models.Product> Products { get; set; }
    }
}

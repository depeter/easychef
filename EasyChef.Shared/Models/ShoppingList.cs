using System.Collections.Generic;

namespace EasyChef.Shared.Models
{
    public class ShoppingList
    {
        public virtual IList<Product> Products { get; set; }
    }
}

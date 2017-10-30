using EasyChef.Shared.Infrastructure;
using EasyChef.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using EasyChef.Contracts.Shared.Models;

namespace EasyChef.Shared.Messages
{
    public class AddItemsToShoppingCartRequest : MessageBusMessage
    {
        public long userId { get; set; }
        public IList<ProductDTO> Products { get; set; }
    }
}

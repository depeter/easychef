using EasyChef.Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChef.Shared.Messages
{
    public class AddItemsToShoppingCartRequest : MessageBusMessage
    {
        public long userId { get; set; }
    }
}

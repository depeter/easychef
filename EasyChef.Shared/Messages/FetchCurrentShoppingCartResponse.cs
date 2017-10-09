using EasyChef.Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using EasyChef.Shared.Models;

namespace EasyChef.Shared.Messages
{
    public class FetchCurrentShoppingCartResponse : MessageBusMessage
    {
        public ShoppingCart ShoppingCart { get; set; }
    }
}

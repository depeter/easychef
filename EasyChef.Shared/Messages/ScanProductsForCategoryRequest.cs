using EasyChef.Shared.Infrastructure;
using EasyChef.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChef.Shared.Messages
{
    public class ScanProductsForCategoryRequest : MessageBusMessage
    {
        public string Category { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChef.Shared.Models
{
    public class ShoppingCart
    {
        public long UserId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

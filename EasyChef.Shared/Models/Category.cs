using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChef.Shared.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool HasProducts { get; set; }
        public IList<Category> Children { get; set; }
        public Category Parent { get; set; }
        public string Link { get; set; }
    }
}

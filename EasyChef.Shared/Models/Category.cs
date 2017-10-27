using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyChef.Shared.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public bool HasProducts { get; set; }

        //public virtual IList<Category> Children { get; set; }

        //public virtual Category Parent { get; set; }

        //[ForeignKey("Parent")]
        //public long? ParentId { get; set; }

        public string Link { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyChef.Shared.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        public long ExternalId { get; set; }
        public string Name { get; set; }
        public bool HasProducts { get; set; }
        public IList<Category> Children { get; set; }
        public Category Parent { get; set; }
        public string Link { get; set; }
    }
}


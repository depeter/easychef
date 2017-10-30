using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyChef.Backend.Rest.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public long ExternalId { get; set; }
        public string Name { get; set; }
        public bool HasProducts { get; set; }
        public virtual IList<Category> Children { get; set; }
        public Category Parent { get; set; }
        public string Link { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}


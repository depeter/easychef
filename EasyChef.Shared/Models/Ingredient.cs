using System.ComponentModel.DataAnnotations.Schema;

namespace EasyChef.Shared.Models
{
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}

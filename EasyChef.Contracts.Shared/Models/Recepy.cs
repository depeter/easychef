using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyChef.Shared.Models
{
    public class Recepy
    {
        public long Id { get; set; }
        public string Title { get; set; }
        [ForeignKey("PreparationId")]
        public virtual RecepyPreparation Preparation { get; set; }
        [ForeignKey("Preparation")]
        public long PreparationId { get; set; }
        public IList<Ingredient> Ingredients { get; set; }
    }
}

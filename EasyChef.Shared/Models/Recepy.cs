using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyChef.Shared.Models
{
    public class Recepy
    {
        public long Id { get; set; }
        public string Title { get; set; }

        [ForeignKey("RecepyPreparationId")]
        public virtual RecepyPreparation RecepyPreparation { get; set; }

        public long RecepyPreparationId { get; set; }

        public virtual IList<Ingredient> Ingredients { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EasyChef.Shared.Models;

namespace EasyChef.Backend.Rest.Models
{
    public class Recepy
    {
        public long Id { get; set; }
        public string Title { get; set; }
        [ForeignKey("PreparationId")]
        public virtual RecepyPreparation Preparation { get; set; }
        [ForeignKey("Preparation")]
        public long PreparationId { get; set; }
        public IList<Backend.Rest.Models.Ingredient> Ingredients { get; set; }
    }
}

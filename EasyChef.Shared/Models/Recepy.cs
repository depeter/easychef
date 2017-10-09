using System.Collections.Generic;

namespace EasyChef.Shared.Models
{
    public class Recepy
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public RecepyPreparation Preparation { get; set; }
        public IList<Ingredient> Ingredients { get; set; }
    }
}

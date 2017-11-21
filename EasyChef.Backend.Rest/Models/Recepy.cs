using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EasyChef.Shared.Models;

namespace EasyChef.Backend.Rest.Models
{
    public class Recepy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Base64Image { get; set; }
        public string CookingDuration { get; set; }
        public string WorkDuration { get; set; }
        public string TotalDuration { get; set; }
        public int NumberOfPeople { get; set; }

        public IList<RecepyPreparation> RecepyPreparations { get; set; }
        public IList<Ingredient> Ingredients { get; set; }
        
    }
}

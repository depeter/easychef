﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EasyChef.Shared.Models;

namespace EasyChef.Contracts.Shared.Models
{
    public class RecepyDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Base64Image { get; set; }
        public string CookingDuration { get; set; }
        public string WorkDuration { get; set; }
        public string TotalDuration { get; set; }
        public int NumberOfPeople { get; set; }

        public IList<RecepyPreparationDTO> RecepyPreparations { get; set; }
        public IList<IngredientDTO> Ingredients { get; set; }
    }
}

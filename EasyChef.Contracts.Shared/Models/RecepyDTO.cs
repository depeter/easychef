using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EasyChef.Shared.Models;

namespace EasyChef.Contracts.Shared.Models
{
    public class RecepyDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public virtual RecepyPreparationDTO Preparation { get; set; }
        public long PreparationId { get; set; }
        public IList<IngredientDTO> Ingredients { get; set; }
    }
}

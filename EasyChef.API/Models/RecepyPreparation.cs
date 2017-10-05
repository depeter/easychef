using System.Collections.Generic;

namespace EasyChef.API.Models
{
    public class RecepyPreparation
    {
        public long Id { get; set; }
        public IList<PreparationStep> Steps { get; set; }
    }
}

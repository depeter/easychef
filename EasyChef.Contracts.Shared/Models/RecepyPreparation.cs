using System.Collections.Generic;

namespace EasyChef.Shared.Models
{
    public class RecepyPreparation
    {
        public long Id { get; set; }
        public IList<PreparationStep> Steps { get; set; }
    }
}

using System.Collections.Generic;

namespace EasyChef.Backend.Rest.Models
{
    public class RecepyPreparation
    {
        public long Id { get; set; }
        public IList<Backend.Rest.Models.PreparationStep> Steps { get; set; }
    }
}

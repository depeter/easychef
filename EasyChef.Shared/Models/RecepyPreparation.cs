using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyChef.Shared.Models
{
    public class RecepyPreparation
    {
        public long Id { get; set; }

        public virtual IList<PreparationStep> Steps { get; set; }

        [ForeignKey("RecepyId")]
        public virtual Recepy Recepy {get;set;}

        [ForeignKey("Recepy")]
        public long RecepyId { get;set; }
    }
}

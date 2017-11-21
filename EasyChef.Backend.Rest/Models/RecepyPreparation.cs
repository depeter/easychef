using System.Collections.Generic;

namespace EasyChef.Backend.Rest.Models
{
    public class RecepyPreparation
    {
        public int Id { get; set; }

        public int Step { get; set; }
        public string Explanation { get; set; }

        public int RecepyId { get; set; }
        public virtual Recepy Recepy { get; set; }
    }
}

using System.Collections.Generic;
using EasyChef.Shared.Models;

namespace EasyChef.Contracts.Shared.Models
{
    public class RecepyPreparationDTO
    {
        public int Id { get; set; }
        public int Step { get; set; }
        public string Explanation { get; set; }
        public RecepyDTO Recepy { get; set; }
    }
}

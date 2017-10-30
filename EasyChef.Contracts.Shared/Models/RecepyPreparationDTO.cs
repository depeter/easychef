using System.Collections.Generic;
using EasyChef.Shared.Models;

namespace EasyChef.Contracts.Shared.Models
{
    public class RecepyPreparationDTO
    {
        public long Id { get; set; }
        public IList<PreparationStepDTO> Steps { get; set; }
    }
}

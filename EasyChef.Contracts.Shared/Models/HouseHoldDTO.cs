using System.Collections.Generic;
using EasyChef.Shared.Models;

namespace EasyChef.Contracts.Shared.Models
{
    public class HouseHoldDTO
    {
        public IList<UserDTO> Residents { get; set; }
        public InventoryDTO InventoryDto { get; set; }
    }
}

using System.Collections.Generic;

namespace EasyChef.API.Models
{
    public class HouseHold
    {
        public IList<User> Residents { get; set; }
        public Inventory Inventory { get; set; }
    }
}

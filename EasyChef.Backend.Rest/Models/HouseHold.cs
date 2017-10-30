using System.Collections.Generic;
using EasyChef.Shared.Models;

namespace EasyChef.Backend.Rest.Models
{
    public class HouseHold
    {
        public IList<User> Residents { get; set; }
        public Inventory Inventory { get; set; }
    }
}

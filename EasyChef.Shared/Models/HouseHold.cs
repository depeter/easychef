using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyChef.Shared.Models
{
    public class HouseHold
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public IList<User> Residents { get; set; }

        [ForeignKey("InventoryId")]
        public virtual Inventory Inventory { get; set; }

        [ForeignKey("Inventory")]
        public long InventoryId { get; set; }
    }
}

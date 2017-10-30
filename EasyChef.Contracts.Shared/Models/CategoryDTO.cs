using System.Collections.Generic;

namespace EasyChef.Contracts.Shared.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public long ExternalId { get; set; }
        public string Name { get; set; }
        public bool HasProducts { get; set; }
        public IList<CategoryDTO> Children { get; set; }
        public CategoryDTO Parent { get; set; }
        public string Link { get; set; }
    }
}


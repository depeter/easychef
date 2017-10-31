using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyChef.Contracts.Shared.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public long ExternalId { get; set; }
        public string Name { get; set; }
        public bool HasProducts { get; set; }
        public IList<CategoryDTO> Children { get; set; }
        [JsonIgnore]
        public CategoryDTO Parent { get; set; }
        public int? ParentId { get; set; }
        public string Link { get; set; }
        public bool HasChildren { get; set; }
        public string AvailableOnUrl { get; set; }
    }
}


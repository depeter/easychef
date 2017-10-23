namespace EasyChef.Shared.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
    }
}

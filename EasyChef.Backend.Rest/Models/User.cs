namespace EasyChef.Backend.Rest.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}

namespace EasyChef.Contracts.Shared.Messages
{
    public class AddItemsToShoppingCartMessage
    {
        public int[] SkuNumbers { get; set; }
        public int UserId { get; set; }
    }
}
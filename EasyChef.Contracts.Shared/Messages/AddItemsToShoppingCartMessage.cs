using EasyChef.Contracts.Shared.Infrastructure;

namespace EasyChef.Contracts.Shared.Messages
{
    public class AddItemsToShoppingCartMessage : MessageBusMessage
    {
        public int[] SkuNumbers { get; set; }
        public int UserId { get; set; }
    }

    public class VerifyLoginMessage : MessageBusMessage
    {
        
    }

    public class ScanRecepiesOnsKookboekMessage : MessageBusMessage
    {
        
    }

    public class ScanRecepiesLibelleLekkerMessage : MessageBusMessage
    {

    }
}

using EasyChef.Contracts.Shared.Infrastructure;

namespace EasyChef.Contracts.Shared.Messages
{
    public class SyncCurrentShoppingCartMessage : MessageBusMessage
    {
        public int UserId { get; set; }
    }
}
using EasyChef.Contracts.Shared.Infrastructure;

namespace EasyChef.Contracts.Shared.Messages
{
    public class ScanProductsForCategoryMessage : MessageBusMessage
    {
        public int CategoryId { get; set; }
    }
}
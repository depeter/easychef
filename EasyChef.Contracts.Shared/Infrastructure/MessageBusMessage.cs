using EasyChef.Shared.Infrastructure;

namespace EasyChef.Contracts.Shared.Infrastructure
{
    public abstract class MessageBusMessage : IMessageBusMessage
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return GetType().Name + "\r\n--------------------------------------\r\n" + this.ReportAllProperties();
        }
    }
}

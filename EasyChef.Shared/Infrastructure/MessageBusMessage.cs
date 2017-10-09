using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChef.Shared.Infrastructure
{
    public abstract class MessageBusMessage : IMessageBusMessage
    {
        public override string ToString()
        {
            return GetType().Name + "\r\n--------------------------------------\r\n" + this.ReportAllProperties();
        }
    }
}

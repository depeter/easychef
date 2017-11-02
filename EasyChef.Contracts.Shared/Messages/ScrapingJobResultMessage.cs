using System;

namespace EasyChef.Contracts.Shared.Messages
{
    public class ScrapingJobResultMessage
    {
        public bool Success { get; set; }
        public Guid? MessageId { get; set; }
    }
}

using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Messages.Events
{
    public class ResponseStockInfo : IEvent
    {
        public string Message { get; set; }
        public string ChatRoomId { get; set; }
    }
}

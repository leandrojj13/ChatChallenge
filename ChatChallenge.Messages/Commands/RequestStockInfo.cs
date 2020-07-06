using NServiceBus;

namespace ChatChallenge.Messages.Commands
{
    public class RequestStockInfo : ICommand
    {
        public string ChatRoomId { get; set; }
        public string Code { get; set; }
    }
}

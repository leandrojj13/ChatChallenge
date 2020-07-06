using ChatChallenge.Core.IoC.Config;
using ChatChallenge.Hubs;
using ChatChallenge.Messages.Events;
using ChatChallenge.Model.Entities.Chat;
using ChatChallenge.Services.Services.Chat;
using Microsoft.AspNetCore.SignalR;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatChallenge.Handlers
{
    public class ResponseStockInfoHandler : IHandleMessages<ResponseStockInfo>
    {
        readonly ILog log = LogManager.GetLogger<ResponseStockInfoHandler>();
        readonly IHubContext<ChatRoomHub> _hubContext;

        public ResponseStockInfoHandler(IHubContext<ChatRoomHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(ResponseStockInfo message, IMessageHandlerContext context)
        {
            try
            {
                await _hubContext.Clients.Group(message.ChatRoomId)
                  .SendAsync("StockInfo", message);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}

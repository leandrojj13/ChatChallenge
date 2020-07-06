using ChatChallenge.Messages.Commands;
using ChatChallenge.Messages.Events;
using ChatChallenge.Services.Services.Stock;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallenge.Bus.Handlers
{
    public class RequestStockInfoHandler : IHandleMessages<RequestStockInfo>
    {
        static ILog log = LogManager.GetLogger<RequestStockInfoHandler>();

        readonly StockService _stockService;
        public RequestStockInfoHandler()
        {
            _stockService = new StockService();
        }
        
        public async Task Handle(RequestStockInfo message, IMessageHandlerContext context)
        {
            try
            {
                var result = await _stockService.GetStockInfoByCodeAsync(message.Code);
                await context.Publish(new ResponseStockInfo() { Message = $"{result.Symbol} quote is {result.Close} per share", ChatRoomId= message.ChatRoomId });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }
    }
}

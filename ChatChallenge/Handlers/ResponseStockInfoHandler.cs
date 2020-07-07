using ChatChallenge.Core.IoC.Config;
using ChatChallenge.Core.Models;
using ChatChallenge.Messages.Events;
using Newtonsoft.Json;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace ChatChallenge.Handlers
{
    public class ResponseStockInfoHandler : IHandleMessages<ResponseStockInfo>
    {
        readonly ILog log = LogManager.GetLogger<ResponseStockInfoHandler>();

        public string ServiceUrl = "/api/ChatRoomMessage/SendStockInfo";

        public async Task Handle(ResponseStockInfo message, IMessageHandlerContext context)
        {
            try
            {
                var appSettings = (AppSettings) Dependency.ServiceProvider.GetService(typeof(AppSettings));
                var apiUrl = appSettings.ApiUrl + ServiceUrl;
                var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                using (await httpClient.PostAsync(apiUrl, content)) { } ;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }
    }
}

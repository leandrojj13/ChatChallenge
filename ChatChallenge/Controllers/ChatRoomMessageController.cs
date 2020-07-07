using ChatChallenge.Bl.Dto.Chat;
using ChatChallenge.Hubs;
using ChatChallenge.Messages.Commands;
using ChatChallenge.Messages.Events;
using ChatChallenge.Model.Entities.Chat;
using ChatChallenge.Services.Services.Chat;
using ChatChallenge.Services.Services.Stock;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NServiceBus;
using System.Threading.Tasks;

namespace ChatChallenge.Controllers
{
    [Route("api/[controller]")]
    public class ChatRoomMessageController : BaseController<ChatRoomMessage, ChatRoomMessageDto>
    {
        protected readonly IChatRoomMessageService _chatRoomMessageService;
        private readonly IMessageSession  _messageSession;
        readonly IHubContext<ChatRoomHub> _hubContext;
        
        public ChatRoomMessageController(IChatRoomMessageService chatRoomMessageService, IValidatorFactory validationFactory, IMessageSession messageSession, IHubContext<ChatRoomHub> hubContext) : base(chatRoomMessageService, validationFactory)
        {
            _chatRoomMessageService = chatRoomMessageService;
            _messageSession = messageSession;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Get Messages by ChatRoomId.
        /// </summary>
        /// <returns>A list of records.</returns>
        [HttpGet("ChatRoom/{chatRoomId}")]
        public virtual IActionResult GetByChatRoomId([FromRoute] int chatRoomId)
        {
            var list = _chatRoomMessageService.GetByChatRoomId(chatRoomId);
            return Ok(list);
        }

        /// <summary>
        /// Get stock info.
        /// </summary>
        [HttpPost("StockInfo")]
        public virtual async Task<IActionResult> StockInfo([FromBody] ChatRoomMessageDto entityDto)
        {
            await _messageSession.Send(new RequestStockInfo() { Code = entityDto.Message, ChatRoomId = entityDto.ChatRoomId.ToString() });
            return Ok();
        }

        /// <summary>
        /// Send stock info.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("SendStockInfo")]
        public virtual async Task<IActionResult> SendStockInfo([FromBody] ResponseStockInfo responseStockInfo)
        {
            await _hubContext.Clients.Group(responseStockInfo.ChatRoomId).SendAsync("StockInfo", responseStockInfo);
            return Ok();
        }
    }
}

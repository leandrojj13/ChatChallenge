using ChatChallenge.Bl.Dto.Chat;
using ChatChallenge.Model.Entities.Chat;
using ChatChallenge.Services.Services.Chat;
using ChatChallenge.Services.Services.Stock;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatChallenge.Controllers
{
    [Route("api/[controller]")]
    public class ChatRoomMessageController : BaseController<ChatRoomMessage, ChatRoomMessageDto>
    {
        protected readonly IChatRoomMessageService _chatRoomMessageService;
        private readonly IStookService _stookService;

        public ChatRoomMessageController(IChatRoomMessageService chatRoomMessageService, IValidatorFactory validationFactory, IStookService stookService) : base(chatRoomMessageService, validationFactory)
        {
            _chatRoomMessageService = chatRoomMessageService;
            _stookService = stookService;
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
            var response = await _stookService.GetStockInfoByCodeAsync(entityDto.Message);
            return Ok(response);
        }
    }
}

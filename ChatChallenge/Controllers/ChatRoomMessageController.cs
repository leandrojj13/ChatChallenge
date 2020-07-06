using ChatChallenge.Bl.Dto.Chat;
using ChatChallenge.Model.Entities.Chat;
using ChatChallenge.Services.Services.Chat;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ChatChallenge.Controllers
{
    [Route("api/[controller]")]
    public class ChatRoomMessageController : BaseController<ChatRoomMessage, ChatRoomMessageDto>
    {
        protected readonly IChatRoomMessageService _chatRoomMessageService;
        public ChatRoomMessageController(IChatRoomMessageService chatRoomMessageService, IValidatorFactory validationFactory) : base(chatRoomMessageService, validationFactory)
        {
            _chatRoomMessageService = chatRoomMessageService;
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
    }
}

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
        public ChatRoomMessageController(IChatRoomMessageService chatRoomMessageService, IValidatorFactory validationFactory) : base(chatRoomMessageService, validationFactory)
        {
        }
    }
}

using ChatChallenge.Bl.Dto.Chat;
using ChatChallenge.Model.Entities.Chat;
using ChatChallenge.Services.Services.Chat;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatChallenge.Controllers
{
    [Route("api/[controller]")]
    public class ChatRoomController : BaseController<ChatRoom, ChatRoomDto>
    {
        public ChatRoomController(IChatRoomService chatRoomService) : base(chatRoomService)
        {
        }
    }
}

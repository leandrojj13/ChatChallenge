using ChatChallenge.Core.BaseModelDto.BaseEntityDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Bl.Dto.Chat
{
    public class ChatRoomDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}

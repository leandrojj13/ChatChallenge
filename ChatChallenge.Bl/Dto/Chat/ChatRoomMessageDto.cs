﻿using ChatChallenge.Core.BaseModelDto.BaseEntityDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Bl.Dto.Chat
{
    public class ChatRoomMessageDto : BaseEntityDto
    {
        public int ChatRoomId { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string Message { get; set; }
    }
}

using ChatChallenge.Core.BaseModel.BaseEntity;
using ChatChallenge.Model.Entities.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Model.Entities.Chat
{
    public class ChatRoomMessage : BaseEntity
    {
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }

        public virtual ChatRoom ChatRoom { get; set; }
        public virtual User User { get; set; }
    }
}

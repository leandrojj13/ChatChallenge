using ChatChallenge.Core.BaseModel.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Model.Entities.Chat
{
    public class ChatRoom : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}

using System.Collections.Generic;
using ChatChallenge.Model.Entities.Chat;
using Microsoft.AspNetCore.Identity;

namespace ChatChallenge.Model.Entities.Security
{
    public class User : IdentityUser
    {
        public User()
        {
            ChatRoomMessages = new HashSet<ChatRoomMessage>();
        }

        public string FullName { get; set; }

        public virtual ICollection<ChatRoomMessage> ChatRoomMessages { get; set; }
    }
}

using AutoMapper;
using ChatChallenge.Bl.Dto.Chat;
using ChatChallenge.Bl.Dto.Security;
using ChatChallenge.Bl.Extensions;
using ChatChallenge.Model.Entities.Chat;
using ChatChallenge.Model.Entities.Security;

namespace ChatChallenge.Bl.Mappings
{
    public class ChatChallengeProfile : Profile
    {
        public ChatChallengeProfile()
        {
            CreateMap<User, UserDto>();

            this._CreateMap_WithConventions_FromAssemblies<ChatRoom, ChatRoomDto>();
        }
    }
}

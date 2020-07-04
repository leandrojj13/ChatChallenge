using AutoMapper;
using ChatChallenge.Bl.Dto.Security;
using ChatChallenge.Bl.Extensions;
using ChatChallenge.Model.Entities.Security;

namespace ChatChallenge.Bl.Mappings
{
    public class ChatChallengeProfile : Profile
    {
        public ChatChallengeProfile()
        {
            this._CreateMap_WithConventions_FromAssemblies<User, UserDto>();
        }
    }
}

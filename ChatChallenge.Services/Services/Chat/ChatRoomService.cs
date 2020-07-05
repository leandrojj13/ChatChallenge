using AutoMapper;
using ChatChallenge.Bl.Dto.Chat;
using ChatChallenge.Model.Contexts.ChatChallenge;
using ChatChallenge.Model.Entities.Chat;
using ChatChallenge.Model.UnitOfWorks;
using ChatChallenge.Services.Generic;

namespace ChatChallenge.Services.Services.Chat
{
    public interface IChatRoomService : IEntityCRUDService<ChatRoom, ChatRoomDto>
    {
    }

    public class ChatRoomService : EntityCRUDService<ChatRoom, ChatRoomDto>, IChatRoomService
    {
        public ChatRoomService(IMapper mapper, IUnitOfWork<IChatChallengeDbContext> uow) : base(mapper, uow)
        {

        }
    }
}

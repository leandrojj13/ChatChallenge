using AutoMapper;
using ChatChallenge.Bl.Dto.Chat;
using ChatChallenge.Model.Contexts.ChatChallenge;
using ChatChallenge.Model.Entities.Chat;
using ChatChallenge.Model.UnitOfWorks;
using ChatChallenge.Services.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Services.Services.Chat
{

    public interface IChatRoomMessageService : IEntityCRUDService<ChatRoomMessage, ChatRoomMessageDto>
    {
    }

    public class ChatRoomMessageService : EntityCRUDService<ChatRoomMessage, ChatRoomMessageDto>, IChatRoomMessageService
    {
        public ChatRoomMessageService(IMapper mapper, IUnitOfWork<IChatChallengeDbContext> uow) : base(mapper, uow)
        {

        }
    }
}

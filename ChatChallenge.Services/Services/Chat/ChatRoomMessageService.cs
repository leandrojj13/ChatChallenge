using AutoMapper;
using ChatChallenge.Bl.Dto.Chat;
using ChatChallenge.Model.Contexts.ChatChallenge;
using ChatChallenge.Model.Entities.Chat;
using ChatChallenge.Model.UnitOfWorks;
using ChatChallenge.Services.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatChallenge.Services.Services.Chat
{

    public interface IChatRoomMessageService : IEntityCRUDService<ChatRoomMessage, ChatRoomMessageDto>
    {
        List<ChatRoomMessageDto> GetByChatRoomId(int chatRoomId);
    }

    public class ChatRoomMessageService : EntityCRUDService<ChatRoomMessage, ChatRoomMessageDto>, IChatRoomMessageService
    {
        public ChatRoomMessageService(IMapper mapper, IUnitOfWork<IChatChallengeDbContext> uow) : base(mapper, uow)
        {

        }
        public  List<ChatRoomMessageDto> GetByChatRoomId(int chatRoomId)
        {
            var list = _repository.GetAll(x=> x.ChatRoomId == chatRoomId, x=> x.User).OrderByDescending(x=> x.CreatedDate).Take(50).OrderBy(x=> x.CreatedDate);
            var listDto = _mapper.Map<List<ChatRoomMessageDto>>(list);
            return listDto;
        }
    }
}

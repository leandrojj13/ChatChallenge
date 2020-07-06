using System.Threading.Tasks;
using ChatChallenge.Messages.Events;
using Microsoft.AspNetCore.SignalR;

namespace ChatChallenge.Hubs
{
    public class ChatRoomHub : Hub
    {
        public async Task SendMessage(string chatRoomId)
        {
            await Clients.Group(chatRoomId).SendAsync("MessageRecived", chatRoomId);
        }

        public async Task JoinUser(string chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString());
            await Clients.Group(chatRoomId).SendAsync("UserJoined", $"The connection id: {Context.ConnectionId} is connected with ChatRoom id: {chatRoomId}.");
        }
    }
}

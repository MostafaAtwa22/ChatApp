using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
        public async Task SendMessage(string user, string message, string chatId)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, chatId);
        }

        public async Task JoinChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        public async Task LeaveChat(string chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        }

        public async Task SendMessageToGroup(string user, string message, string chatId)
        {
            await Clients.Group(chatId).SendAsync("ReceiveMessage", user, message, chatId);
        }
    }
} 
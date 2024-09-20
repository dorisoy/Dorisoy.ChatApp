using Dorisoy.ChatServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace Dorisoy.ChatServer.Hubs
{
    public class ChatHub : Hub
    {        
        public void SendMessage(string connectionId, 
            string userId, 
            string name, 
            string photo, 
            string message)
        {
            string unniqueId = Guid.NewGuid().ToString();

            Clients.Client(connectionId).SendAsync("ReceiveMessage", Context.ConnectionId, userId, name, photo, message, unniqueId, false);

            Clients.Caller.SendAsync("ReceiveMessage", connectionId, "", "", "", message, unniqueId, true);
        }

        public Task Typing(string connectionId, string name)
        {
            return Clients.Client(connectionId).SendAsync("TypingMessage", connectionId, name);
        }

        public void Disconnect()
        {
            Context.Abort();
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var chatUsername = Context.GetHttpContext().Request.Query["chatUsername"];
            if (!String.IsNullOrEmpty(chatUsername))
            {
                // Remove the user if already exists in the list
                HubHelper.ChatUserList.RemoveAll(x => x.ChatUsername == chatUsername);

                // Add the user
                HubHelper.ChatUserList.Add(new ChatUser { ConnectionId = Context.ConnectionId, ChatUsername = chatUsername, ActionTime = DateTime.Now });

                await Clients.All.SendAsync("UserConnected", Context.ConnectionId, HubHelper.ChatUserList);
            }            
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var connectionId = Context.ConnectionId;

            var item = HubHelper.ChatUserList.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                HubHelper.ChatUserList.Remove(item);                                
                await Clients.All.SendAsync("UserDisconnected", connectionId, HubHelper.ChatUserList);
            }

            await base.OnDisconnectedAsync(ex);
        }

        public Task GetConnectedUsers()
        {
            return Clients.Caller.SendAsync("ChatUserList", HubHelper.ChatUserList);
        }
    }
}

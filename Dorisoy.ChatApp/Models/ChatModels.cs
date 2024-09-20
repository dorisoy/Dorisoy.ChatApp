using System;

namespace Dorisoy.ChatApp.Models
{
    public class ChatMessage
    {
        public string ConnectionId { get; set; }
        public string Message { get; set; }
        public string MyPhoto { get; set; }
        public string PairPhoto { get; set; }
        public bool IsOwnMessage { get; set; }
        public bool IsSystemMessage { get; set; }
        public string ActionTime { get; set; }

    }

    public class ChatUser
    {
        public string ConnectionId { get; set; }
        public string ChatUsername { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string PhotoPath { get; set; }
        public bool IsOnline { get; set; }
        public DateTime ActionTime { get; set; }
    }
}

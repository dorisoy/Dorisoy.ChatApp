using Dorisoy.ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dorisoy.ChatServer.Hubs
{
    public static class HubHelper
    {
        private static List<ChatUser> _chatUserList = new List<ChatUser>();
        public static List<ChatUser> ChatUserList
        {
            get
            {
                return _chatUserList;
            }
            set
            {
                _chatUserList = value;
            }
        }
    }
}

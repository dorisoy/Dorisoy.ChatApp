using Dorisoy.ChatApp.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Dorisoy.ChatApp.ViewModel;

public partial class ChatListViewModel : BaseViewModel
{
    HubConnection hubConnection;
    public ObservableCollection<ChatUser> ChatUserList { get; } = new();

    public ChatListViewModel()
    {
        // Hook up hubConnection
        string chatUserName = Preferences.Get("ChatUserName", "");
        hubConnection = ChatHelper.GetInstanse(chatUserName);
        hubConnection.On<List<ChatUser>>("ChatUserList", (chatUserList) =>
        {
            RenderList(chatUserName, chatUserList);
        });

        // Load list data
        LoadData();
    }


    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    string title;

    [RelayCommand]
    private void Login()
    {
        AppShell.Current.GoToAsync("//LoginPage");
    }

    [RelayCommand]
    private void Select(ChatUser chatUser)
    {
        AppShell.Current.GoToAsync("//ChatMessagePage?",
                new Dictionary<string, object>
                {
                    ["userId"] = chatUser.Username,
                    ["connectionId"] = chatUser.ConnectionId,
                    ["name"] = chatUser.Name,
                    ["photo"] = chatUser.PhotoPath
                }
            );
    }

    async private void LoadData()
    {
        await hubConnection.InvokeAsync("GetConnectedUsers");
    }

    private void RenderList(string chatUserName, List<ChatUser> chatUserList)
    {
        ObservableCollection<ChatUser> formatedList = new ObservableCollection<ChatUser>();
        foreach (ChatUser item in chatUserList)
        {
            if (item.ChatUsername != chatUserName) // Exclude myself in chat user list
            {
                formatedList.Add(GetFormatedChatUser(item.ConnectionId, item.ChatUsername));
            }
        }

        foreach (var item in formatedList)
        {
            ChatUserList.Add(item);
        }

        this.Title = "Chat Members (" + this.ChatUserList.Count.ToString() + ")";
    }

    private ChatUser GetFormatedChatUser(string connectionId, string chatUsername)
    {
        ChatUser formatedChatUser = new ChatUser();

        string username = String.Empty;
        string name = String.Empty;
        string location = String.Empty;
        string photo = String.Empty;

        int count = 1;
        string[] datas = chatUsername.Split('_');
        foreach (string data in datas)
        {
            switch (count)
            {
                case 1:
                    username = data;
                    break;
                case 2:
                    name = data;
                    break;
                case 3:
                    photo = data;
                    break;
                case 4:
                    location = data;
                    break;
                default:
                    break;
            }

            count++;
        } // End foreach

        formatedChatUser.ConnectionId = connectionId;
        formatedChatUser.ChatUsername = username;
        formatedChatUser.Name = name;
        formatedChatUser.Location = location;
        formatedChatUser.PhotoPath = photo;
        formatedChatUser.IsOnline = true;

        return formatedChatUser;
    }

    //public void LoadSampleData()
    //{
    //    ChatUserList.Add(new ChatUser { Name = "Danieal Gonzalez", Location = "California, United States", IsOnline = true, PhotoPath = "m2.png" });
    //    ChatUserList.Add(new ChatUser { Name = "Meaghan Sarah", Location = "Sacramento, California", IsOnline = true, PhotoPath = "f1.png" });                
    //    ChatUserList.Add(new ChatUser { Name = "Jasper Margaret", Location = "Copenhagen, Denmark", IsOnline = true, PhotoPath = "m1.png" });
    //    ChatUserList.Add(new ChatUser { Name = "William Russel", Location = "Houston, Texas", IsOnline = true, PhotoPath = "m3.png" });
    //    ChatUserList.Add(new ChatUser { Name = "Amelia Sophia", Location = "Northern Ireland", IsOnline = true, PhotoPath = "f2.png" });
    //    ChatUserList.Add(new ChatUser { Name = "Janiphar Neels", Location = "Wales, United Kingdom", IsOnline = false, PhotoPath = "f4.png" });
    //    ChatUserList.Add(new ChatUser { Name = "Miraj Sheikh", Location = "Delli, India", IsOnline = false, PhotoPath = "m4.png" });
    //    ChatUserList.Add(new ChatUser { Name = "Emma kidman", Location = "London, United Kingdom", IsOnline = false, PhotoPath = "f3.png" });
    //    this.Title = "Chat Members (" + this.ChatUserList.Count.ToString() + ")";
    //}
}


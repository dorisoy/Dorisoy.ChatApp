using Dorisoy.ChatApp.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Dorisoy.ChatApp.ViewModel;

public partial class ChatMessageViewModel : BaseViewModel, IQueryAttributable
{
    HubConnection hubConnection;

    private CancellationTokenSource tokenSource;

    public ObservableCollection<ChatMessage> ChatMessageList { get; set; } = new();

    [ObservableProperty]
    string sendingMessage;

    [ObservableProperty]
    string typingText;

    [ObservableProperty]
    bool showTyping;

    [ObservableProperty]
    string pairPhoto;

    [ObservableProperty]
    string pairName;

    public string MyUserId { get; set; }
    public string MyName { get; set; }
    public string MyPhoto { get; set; }
    public string PairConnectionId { get; set; }
    public string PairUserId { get; set; }

    public ChatMessageViewModel()
    {
        ChatMessageList = new ObservableCollection<ChatMessage>();

        string chatUserName = Preferences.Get("ChatUserName", "");
        this.MyUserId = Utils.GetUserId(chatUserName);
        this.MyName = Utils.GetName(chatUserName);
        this.MyPhoto = Preferences.Get("ProfilePhoto", "");

        hubConnection = ChatHelper.GetInstanse(chatUserName);
        hubConnection.Closed += async (error) =>
        {
            SendLocalMessage("---Connection closed---");
            ChatHelper.IsConnected = false;
            await Task.Delay(2000);
            await ChatHelper.Connect(chatUserName);
        };

        hubConnection.On<string, ObservableCollection<ChatUser>>("UserDisconnected", (connectionId, chatUserList) =>
        {
            SendLocalMessage("---User disconnected---");
        });

        hubConnection.On<string, string>("TypingMessage", (connectionId, name) =>
        {
            TaskCancel();
            this.TypingText = name + " is typing...";
            this.ShowTyping = true;
            TaskDelay();
        });

        hubConnection.On<string, string, string, string, string, string, bool>("ReceiveMessage", (pairConnectionId, pairUserId, pairName, pairPhoto, message, uniqueId, isMe) =>
        {
            if (!isMe)
            {
                this.PairConnectionId = pairConnectionId;
                this.PairUserId = pairUserId;
                this.PairName = pairName;
                this.PairPhoto = pairPhoto;
            }

            ChatMessageList.Add(new ChatMessage() { Message = message, IsOwnMessage = isMe, MyPhoto = this.MyPhoto, PairPhoto = this.PairPhoto, IsSystemMessage = false, ActionTime = DateTime.Now.ToString("hh:mm tt") });

        });

    }

    public void TaskCancel()
    {
        tokenSource?.Cancel();
    }

    public async void TaskDelay()
    {
        tokenSource = new CancellationTokenSource();
        try
        {
            await Task.Delay(3000, tokenSource.Token);
            this.ShowTyping = false;
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            if (tokenSource != null)
            {
                tokenSource.Dispose();
                tokenSource = null;
            }
        }
    }

    [RelayCommand]
    private void GoToChatList()
    {
        AppShell.Current.GoToAsync("//ChatListPage");
    }

    [RelayCommand]
    async private void SendMessage()
    {
        try
        {
            await hubConnection.InvokeAsync("SendMessage", this.PairConnectionId, this.MyUserId, this.MyName, this.MyPhoto, this.SendingMessage);
            this.SendingMessage = "";
        }
        catch (Exception ex)
        {
            SendLocalMessage($"Send failed: {ex.Message}");
        }
    }

    async Task Typing()
    {
        await hubConnection.InvokeAsync("Typing", this.PairConnectionId, this.MyName);
    }

    private void SendLocalMessage(string message)
    {
        //ChatMessageList.Add(new ChatMessage
        //{
        //    Message = message,
        //    IsOwnMessage = false,
        //    IsSystemMessage = true
        //});
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("connectionId"))
        {
            this.PairConnectionId = query["connectionId"] as string;
            this.PairUserId = query["userId"] as string;
            this.PairName = query["name"] as string;
            this.PairPhoto = query["photo"] as string;
        }
    }
}
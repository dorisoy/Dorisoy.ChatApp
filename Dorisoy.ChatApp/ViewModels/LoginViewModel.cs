
using Dorisoy.ChatApp.Helpers;

namespace Dorisoy.ChatApp.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    public LoginViewModel()
    {
        user = new User()
        {
            Username = "Dorisoy",
            Password = "Dorisoy"
        };
    }

    [ObservableProperty]
    User user;

    [RelayCommand]
    private async void Login()
    {
        var username = Preferences.Get("Username", "Dorisoy");
        var password = Preferences.Get("Password", "Dorisoy");
        var name = Preferences.Get("Name", "Dorisoy");
        var location = Preferences.Get("Location", "China");
        string imageName = Preferences.Get("ProfilePhoto", "");

        if (User.Username != username || User.Password != password)
        {
            await Shell.Current.DisplayAlert("Alert!", "Invalid username or password!", "OK");
        }
        else
        {
            // Connect to chat hub
            string chatUsername = User.Username + "_" + name + "_" + imageName + "_" + location;
            await ChatHelper.Connect(chatUsername);

            // Save to local storage
            Preferences.Set("ChatUserName", chatUsername);

            // Navigate to chat user list page
            await AppShell.Current.GoToAsync("//ChatListPage");
        }
    }

    [RelayCommand]
    private void Register()
    {
        AppShell.Current.GoToAsync("//RegisterPage");
    }
}


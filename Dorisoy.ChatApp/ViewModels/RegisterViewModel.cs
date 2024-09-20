
namespace Dorisoy.ChatApp.ViewModel;

public partial class RegisterViewModel : BaseViewModel
{
    public RegisterViewModel()
    {
        user = new User();
    }

    [ObservableProperty]
    User user;

    [RelayCommand]
    private void Login()
    {
        AppShell.Current.GoToAsync("//LoginPage");
    }

    [RelayCommand]
    private void Signup()
    {
        if (String.IsNullOrEmpty(User.Name))
        {
            Shell.Current.DisplayAlert("Alert!", "Please enter name!", "OK");
        }

        Preferences.Set("Name", User.Name);
        Preferences.Set("Location", User.Address);
        Preferences.Set("Username", User.Username);
        Preferences.Set("Password", User.Password);
        Preferences.Set("ProfilePhoto", "m1.png"); // Replace with your photo

        AppShell.Current.GoToAsync("//LoginPage");
    }
}


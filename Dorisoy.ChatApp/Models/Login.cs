namespace Dorisoy.ChatApp.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class LoginReturn
    {
        public string returnUrl { get; set; }
        public string userId { get; set; }
        public string chatUserName { get; set; }
        public bool isSuccess { get; set; }
    }
}

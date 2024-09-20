namespace Dorisoy.ChatApp;

public partial class ChatMessagePage : ContentPage
{
	public ChatMessagePage(ChatMessageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
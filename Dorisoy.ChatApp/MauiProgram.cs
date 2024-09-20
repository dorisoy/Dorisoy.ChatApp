using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Dorisoy.ChatApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>()
    .UseMauiCommunityToolkit()
    .ConfigureFonts(fonts =>
    {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
    });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoginPage>();

        builder.Services.AddSingleton<RegisterViewModel>();
        builder.Services.AddSingleton<RegisterPage>();

        builder.Services.AddSingleton<ChatListViewModel>();
        builder.Services.AddSingleton<ChatListPage>();

        builder.Services.AddSingleton<ChatMessageViewModel>();
        builder.Services.AddSingleton<ChatMessagePage>();

        return builder.Build();
    }
}

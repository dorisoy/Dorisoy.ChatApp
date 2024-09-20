using Dorisoy.ChatApp.Controls;

#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace Dorisoy.ChatApp;

public partial class App : Application
{
        public App()
        {
                InitializeComponent();

                Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", static (h, v) =>
                {
#if ANDROID
            h.PlatformView.BackgroundTintList = 
                Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent); 
#endif
                });


#if WINDOWS
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderlineWindows", (h, v) =>
        {
            h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness()
            {
                Bottom = 0,
                Top = 0,
                Left = 0,
                Right = 0,
            };
        });
#endif

                Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderLessEntry), (handler, value) =>
                {

#if __ANDROID__
				handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif __IOS__
                        handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                        handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
                });


                MainPage = new AppShell();


        }
}

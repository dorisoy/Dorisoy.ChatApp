using System;

namespace Dorisoy.ChatApp.Converters
{
    public class OnlineStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "true":
                    return Color.FromArgb("5cb26e");
                case "false":
                    return Color.FromArgb("808080");
            }

            return Color.FromArgb("808080");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

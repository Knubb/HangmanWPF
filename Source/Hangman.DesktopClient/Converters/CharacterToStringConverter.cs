using System;
using System.Globalization;
using System.Windows.Data;

namespace Hangman.DesktopClient.Converters
{
    // Looks weird to be, what's the use of it?
    public class CharacterToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

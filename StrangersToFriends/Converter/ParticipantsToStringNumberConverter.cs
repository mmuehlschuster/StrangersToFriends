using Xamarin.Forms;

using System;
using System.Globalization;

namespace StrangersToFriends.Converter
{
    public class ParticipantsToStringNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value + " participants";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

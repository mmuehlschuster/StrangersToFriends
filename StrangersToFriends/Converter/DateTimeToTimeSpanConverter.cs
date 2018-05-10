using Xamarin.Forms;

using System;
using System.Globalization;

namespace StrangersToFriends.Converter
{
    public class DateTimeToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            TimeSpan timeSpan = dateTime - dateTime.Date;
            return timeSpan;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using Xamarin.Forms;

using System;
using System.Globalization;

namespace StrangersToFriends.Converter
{
    public class DateTimeToStringConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
			string format =  parameter as string;

            DateTime dateTime = (DateTime)value;

            return dateTime.ToString(format);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

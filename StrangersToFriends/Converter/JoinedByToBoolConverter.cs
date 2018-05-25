using System;
using System.Collections.ObjectModel;
using System.Globalization;
using StrangersToFriends.Helper;
using Xamarin.Forms;

namespace StrangersToFriends.Converter
{
	public class JoinedByToBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var joined = value as ObservableCollection<string>;

			if(joined.Contains(LoginManager.Auth.User.LocalId))
			{
				return true;
			}

			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

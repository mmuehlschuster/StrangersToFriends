using System;
using StrangersToFriends.Enums;
using Xamarin.Forms;

namespace StrangersToFriends.Infastructure.Services
{
    public interface INavigationService
    {
        void GoBack();
        void NavigateTo(AppPages pageKey);
        void NavigateTo(AppPages pageKey, object parameter);
		void Configure(AppPages mainPage, Type type);
		void Initialize(NavigationPage firstPage);
	}
}

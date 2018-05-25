using System;
using System.Threading.Tasks;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using StrangersToFriends.Helper;
using StrangersToFriends.Constants;
using StrangersToFriends.Infastructure.Services;


namespace StrangersToFriends.ViewModel
{
	public class MoreViewModel : ViewModelBase
    {
		private readonly INavigationService _navigationService;
		public ICommand LogoutCommand { get; private set; }

		public MoreViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;

			LogoutCommand = new RelayCommand(Logout);
        }

		public void Logout() 
		{
			_navigationService.GoBack();
		}
    }
}

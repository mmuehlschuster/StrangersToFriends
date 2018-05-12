using Firebase.Auth;

using StrangersToFriends.Infastructure.Services;

using GalaSoft.MvvmLight;


namespace StrangersToFriends.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

		public FirebaseAuthLink auth { get; set; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
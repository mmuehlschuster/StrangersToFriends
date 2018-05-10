using Xamarin.Forms;

using System.Windows.Input;
using System.Collections.ObjectModel;

using StrangersToFriends.Enums;
using StrangersToFriends.Model;
using StrangersToFriends.Infastructure.Services;

using GalaSoft.MvvmLight;

namespace StrangersToFriends.ViewModel
{
    public class MyActivitiesViewModel
    {
        private readonly INavigationService _navigationService;
        public ICommand NavigateCommand { get; private set; }

        public ObservableCollection<Activity> Activities { get; set; }

        public MyActivitiesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateCommand = new Command(() => Navigate());

            Activities = new ObservableCollection<Activity>();
        }

        private void Navigate()
        {
            _navigationService.NavigateTo(AppPages.AddActivityPage);
        }
    }
}

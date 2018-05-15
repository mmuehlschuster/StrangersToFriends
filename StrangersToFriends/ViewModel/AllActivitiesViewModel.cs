using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Firebase.Database;

using GalaSoft.MvvmLight;

using StrangersToFriends.Model;
using StrangersToFriends.Enums;
using StrangersToFriends.Helper;
using StrangersToFriends.Infastructure.Services;
using StrangersToFriends.Constants;

namespace StrangersToFriends.ViewModel
{
	public class AllActivitiesViewModel : ViewModelBase
	{
        private readonly INavigationService _navigationService;

		public ObservableCollection<Activity> AllActivities { get; set; }

		public Activity SelectedActivity
        {
            get => null;
            set => _navigationService.NavigateTo(AppPages.DetailsPage, value);
        }

        private FirebaseClient _firebase;

		public AllActivitiesViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;

			AllActivities = new ObservableCollection<Activity>();

			_firebase = new FirebaseClient(Constant.FirebaseAppUri, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(LoginManager.Auth.FirebaseToken)
            });

            loadDataFromDatabase();
        }

		private async void loadDataFromDatabase()
        {
			var activities = await _firebase.Child("activities").OnceAsync<Activity>();

            AllActivities.Clear();
            foreach (var activity in activities)
            {
				Activity ac = activity.Object;
                ac.ID = activity.Key;
				AllActivities.Add(ac);
            }
        }
    }
}

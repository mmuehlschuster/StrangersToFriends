using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using StrangersToFriends.Enums;
using StrangersToFriends.Model;
using StrangersToFriends.Helper;
using StrangersToFriends.Constants;
using StrangersToFriends.Infastructure.Services;

using Firebase.Database;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace StrangersToFriends.ViewModel
{
	public class MyActivitiesViewModel : ViewModelBase
	{
		private readonly INavigationService _navigationService;
		public ICommand AddCommand { get; private set; }

		public ObservableCollection<Activity> Activities { get; set; }

		public Activity SelectedActivity 
		{
			get => null;
			set => _navigationService.NavigateTo(AppPages.DetailsPage, value);
		}

		private FirebaseClient _firebase;

		public MyActivitiesViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
            
			AddCommand = new RelayCommand(AddActivity);

			Activities = new ObservableCollection<Activity>();
            
			_firebase = new FirebaseClient(Constant.FirebaseAppUri, new FirebaseOptions
			{
				AuthTokenAsyncFactory = () => Task.FromResult(LoginManager.Auth.FirebaseToken)
			});

			loadDataFromDatabase();
		}

		private void AddActivity()
		{
			_navigationService.NavigateTo(AppPages.AddActivityPage);
		}

		private async void loadDataFromDatabase()
		{
			var activities = await _firebase.Child("activities").OnceAsync<Activity>();

			Activities.Clear();
			foreach (var activity in activities)
			{
				if (activity.Object.CreatedBy.Equals(LoginManager.Auth.User.LocalId)) 
				{
					Activity ac = activity.Object;
					ac.ID = activity.Key;
					Activities.Add(ac);
				}
			}
		}
	}
}

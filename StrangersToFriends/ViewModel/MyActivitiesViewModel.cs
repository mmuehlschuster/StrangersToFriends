using System;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using StrangersToFriends.Enums;
using StrangersToFriends.Model;
using StrangersToFriends.Helper;
using StrangersToFriends.Constants;
using StrangersToFriends.Infastructure.Services;

using Firebase.Database;
using Firebase.Database.Query;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace StrangersToFriends.ViewModel
{
	public class MyActivitiesViewModel : ViewModelBase
	{
		private readonly INavigationService _navigationService;
		public ICommand AddCommand { get; private set; }
		public ICommand DeleteCommand { get; private set; }

		public ObservableCollection<Activity> Activities { get; set; }

		public Activity SelectedActivity 
		{
			get => null;
			set
			{
				if (value != null)
				{
					_navigationService.NavigateTo(AppPages.DetailsPage, value);
					RaisePropertyChanged();
				}
			}
		}

		private ContentManager _contentManager;

		public MyActivitiesViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
            
			AddCommand = new RelayCommand(AddActivity);
			DeleteCommand = new RelayCommand<Activity>(DeleteActivity);
            
			Activities = new ObservableCollection<Activity>();

			_contentManager = new ContentManager();
		}

		private void AddActivity()
		{
			_navigationService.NavigateTo(AppPages.AddActivityPage);
		}

		private void RemoveActivitiyFromList(ObservableCollection<Activity> activities, Activity activity)
		{
			foreach (Activity ac in activities.ToList())
			{
				if (ac.ID.Equals(activity.ID)) 
				{
					activities.Remove(ac);
				}
			}
		}

		private async void DeleteActivity(Activity activity) 
		{
			var all = App.Locator.AllActivitiesViewModel.Activities;
			var filtered = App.Locator.AllActivitiesViewModel.FilteredActivities;
            var created = App.Locator.MyActivitiesViewModel.Activities;
            var joined = App.Locator.JoinedActivitiesViewModel.Activities;

			RemoveActivitiyFromList(all, activity);
			RemoveActivitiyFromList(filtered, activity);
			RemoveActivitiyFromList(created, activity);
			RemoveActivitiyFromList(joined, activity);

			await _contentManager.DeleteActivity(activity);
		}

		public async void loadDataFromDatabase()
		{
			var activities = await _contentManager.GetActivities();

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

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using StrangersToFriends.Model;
using StrangersToFriends.Enums;
using StrangersToFriends.Helper;
using StrangersToFriends.Constants;
using StrangersToFriends.Infastructure.Services;

using Firebase.Database;

namespace StrangersToFriends.ViewModel
{
	public class JoinedActivitiesViewModel : ViewModelBase
    {
		private readonly INavigationService _navigationService;

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

		public JoinedActivitiesViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;

			Activities = new ObservableCollection<Activity>();

			_contentManager = new ContentManager();
        }

		public async void loadDataFromDatabase()
        {
			var activities = await _contentManager.GetActivities();

			Activities.Clear();
            foreach (var activity in activities)
            {
				if (activity.Object.JoinedBy.Contains(LoginManager.Auth.User.LocalId)) 
				{
					Activity ac = activity.Object;
                    ac.ID = activity.Key;
					Activities.Add(ac);
				}
            }
        }
    }
}

using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

using StrangersToFriends.Model;
using StrangersToFriends.Enums;
using StrangersToFriends.Helper;
using StrangersToFriends.Infastructure.Services;

namespace StrangersToFriends.ViewModel
{
	public class AllActivitiesViewModel : ViewModelBase
	{
        private readonly INavigationService _navigationService;
        
		public ObservableCollection<Activity> Activities { get; set; }
		public ObservableCollection<Activity> FilteredActivities { get; set; }

		public Activity SelectedActivity
        {
			get => null;
            set
			{
				if (value != null) {
					_navigationService.NavigateTo(AppPages.DetailsPage, value);
                    RaisePropertyChanged();
				}
			} 
        }

		private ContentManager _contentManager;

		public AllActivitiesViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;
                   
			Activities = new ObservableCollection<Activity>();
			FilteredActivities = new ObservableCollection<Activity>();

			_contentManager = new ContentManager();
        }

		public async void loadDataFromDatabase()
        {
			var activities = await _contentManager.GetActivities();

			Activities.Clear();
			FilteredActivities.Clear();
            foreach (var activity in activities)
            {
				Activity ac = activity.Object;
                ac.ID = activity.Key;
				Activities.Add(ac);
				FilteredActivities.Add(ac);
            }
        }
    }
}

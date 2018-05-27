using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Plugin.Geolocator;

using StrangersToFriends.Model;
using StrangersToFriends.Constants;
using StrangersToFriends.Infastructure.Services;
using StrangersToFriends.Infrastructure.Services;

namespace StrangersToFriends.ViewModel
{
	public class SearchViewModel : ViewModelBase
    {
		private readonly INavigationService _navigationService;
		private readonly IDialogService _dialogService;

		public ICommand SearchCommand { get; private set; }
		public ICommand ResetCommand { get; private set; }

		private DateTime _fromDate = DateTime.Now;
        public DateTime FromDate 
		{
			get => _fromDate;
			set 
			{ 
				_fromDate = value;
				RaisePropertyChanged();
			} 
		}

		private DateTime _toDate = DateTime.Now.AddDays(3);
        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                _toDate = value;
                RaisePropertyChanged();
            }
        }

		public List<string> Categories { get; set; }

		private string _selectedCategory;
        public string SelectedCategory 
		{
			get => _selectedCategory;
			set
			{
				_selectedCategory = value;
				RaisePropertyChanged();
			}
		}

		private double _distance;
		public double Distance 
		{
			get => _distance; 
			set
			{
				_distance = value;
				RaisePropertyChanged();
			}
		}

		public SearchViewModel(INavigationService navigationService, IDialogService dialogService)
        {
			_navigationService = navigationService;
			_dialogService = dialogService;

			SearchCommand = new RelayCommand(Search);
			ResetCommand = new RelayCommand(Reset);

			Categories = Constant.Categories;
        }

		private async void Search()
		{
			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 5;

			try
			{
				var pos = await locator.GetPositionAsync(TimeSpan.FromSeconds(5));
				Position position = new Position(pos.Latitude, pos.Longitude);

				var activities =
					(from activity in App.Locator.AllActivitiesViewModel.Activities
					 where (activity.Date.Date >= FromDate.Date && activity.Date.Date <= ToDate.Date) && (activity.Location.DistanceTo(position).Kilometers <= Distance) && (activity.Category == SelectedCategory)
					 select activity).ToList();

				App.Locator.AllActivitiesViewModel.FilteredActivities.Clear();
				foreach (Activity activity in activities)
				{
					App.Locator.AllActivitiesViewModel.FilteredActivities.Add(activity);
				}            
			}
			catch (Exception ex) 
			{
				await _dialogService.ShowMessage(ex.Message, "Error");
			}
		}

		private void Reset()
		{
			FromDate = DateTime.Now;
			ToDate = DateTime.Now.AddDays(3);
            SelectedCategory = string.Empty;
            Distance = 0;

			App.Locator.AllActivitiesViewModel.FilteredActivities.Clear();
			foreach (Activity activity in App.Locator.AllActivitiesViewModel.Activities)
			{
				App.Locator.AllActivitiesViewModel.FilteredActivities.Add(activity);
			}
		}
    }
}

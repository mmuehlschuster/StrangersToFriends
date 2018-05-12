using Firebase.Auth;
using Firebase.Database;

using System;
using System.Collections.Generic;
using System.Windows.Input;

using StrangersToFriends.Infastructure.Services;
using StrangersToFriends.Model;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace StrangersToFriends.ViewModel
{
	public class AddViewModel : ViewModelBase
    {
		public const string FirebaseAppUri = "https://strangerstofriends-50df0.firebaseio.com";

        private readonly INavigationService _navigationService;
        public ICommand CreateCommand { get; private set; }

        public string Title { get; set; }

        private DateTime date = DateTime.Now;
        public DateTime Date { get { return date; } set { date = value; } }
        public TimeSpan Time { get; set; }

		public List<string> Categories { get; set; }
        public string SelectedCategory { get; set; }

		public string Location { get; set; }

        public string Description { get; set; }
		public string NumberOfParticipants { get; set; }

        
        public AddViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

			CreateCommand = new RelayCommand(CreateActivity);

            Categories = new List<string> { "Sport", "Gastronomy", "Culture", "Games", "Tutoring", "Events", "Trips" };
        }

        private void CreateActivity() 
        {
			FirebaseAuth auth = App.Locator.MainViewModel.auth;



            Location location = new Location(Location, 1, 1);

            Activity activity = new Activity();
            activity.Title = Title;
            activity.Date = Date.Date + Time;
            activity.Category = SelectedCategory;
            activity.Location = location;
            activity.Description = Description;
            activity.Participants = 1;
            activity.NumberOfParticipants = activity.Participants + "/" + NumberOfParticipants;
            activity.CreatedBy = auth.User.LocalId;
			activity.JoinedBy.Add(auth.User.LocalId);

            App.Locator.MyActivitiesViewModel.Activities.Insert(0, activity);

            clearFields();

            _navigationService.GoBack();
        }

        private void clearFields() 
        {
			Title = string.Empty;
            date = DateTime.Now;
			SelectedCategory = string.Empty;
			Location = string.Empty;
			Description = string.Empty;
			NumberOfParticipants = string.Empty;
        }
    }
}

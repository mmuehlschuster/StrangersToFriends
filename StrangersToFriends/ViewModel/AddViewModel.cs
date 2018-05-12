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

        public string Description { get; set; }
        public string Participants { get; set; }

        public List<string> Categories { get; set; }
        public string SelectedCategory { get; set; }

        public AddViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

			CreateCommand = new RelayCommand(CreateActivity);

            Categories = new List<string> { "Sport" };
        }

        private void CreateActivity() 
        {
            Date = Date.Date + Time;
            App.Locator.MyActivitiesViewModel.Activities.Insert(0, new Activity(Title, Date, Description, Participants));

            clearFields();

            _navigationService.GoBack();
        }

        private void clearFields() 
        {
			Title = string.Empty;
            date = DateTime.Now;
			Description = string.Empty;
			Participants = string.Empty;
			SelectedCategory = string.Empty;
        }
    }
}

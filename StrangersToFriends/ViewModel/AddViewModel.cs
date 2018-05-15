using Xamarin.Forms.Maps;

using Newtonsoft.Json;

using Firebase.Database;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

using StrangersToFriends.Model;
using StrangersToFriends.Helper;
using StrangersToFriends.Constants;
using StrangersToFriends.Infastructure.Services;
using StrangersToFriends.Infrastructure.Services;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace StrangersToFriends.ViewModel
{
	public class AddViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
		private readonly IDialogService _dialogService;

        public ICommand CreateCommand { get; private set; }

        public string Title { get; set; }

        private DateTime _date = DateTime.Now;
        public DateTime Date { get { return _date; } set { _date = value; } }
        public TimeSpan Time { get; set; }

		public List<string> Categories { get; set; }
        public string SelectedCategory { get; set; }

		public string Location { get; set; }

        public string Description { get; set; }
		public string NumberOfParticipants { get; set; }

		private FirebaseClient _firebase;

		public AddViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
			_dialogService = dialogService;

			CreateCommand = new RelayCommand(CreateActivity);
            
            Categories = new List<string> { "Sport", "Gastronomy", "Culture", "Game", "Tutoring", "Event", "Trip" };
            
			_firebase = new FirebaseClient(Constant.FirebaseAppUri, new FirebaseOptions
			{
				AuthTokenAsyncFactory = () => Task.FromResult(LoginManager.Auth.FirebaseToken)
			});
        }

		private async void CreateActivity() 
        {
			if (!string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(SelectedCategory) || !string.IsNullOrWhiteSpace(Location) || !string.IsNullOrWhiteSpace(NumberOfParticipants)) 
			{
				List<Position> positionList = new List<Position>();
                try
                {
                    Geocoder geocoder = new Geocoder();
                    IEnumerable<Position> positions = await geocoder.GetPositionsForAddressAsync(Location);
                    foreach (Position position in positions)
                    {
                        positionList.Add(position);
                    }
                }
                catch (Exception ex) 
				{
					await _dialogService.ShowMessage(ex.Message, "Error");
				}

                Location location = new Location(Location, positionList[0].Latitude, positionList[0].Longitude);

                Activity activity = new Activity();
                activity.Title = Title;
                activity.Date = Date.Date + Time;
                activity.Category = SelectedCategory;
                activity.Location = location;
                activity.Description = Description;
                activity.Participants = 1;
                activity.NumberOfParticipants = activity.Participants + "/" + NumberOfParticipants;
                activity.CreatedBy = LoginManager.Auth.User.LocalId;
                activity.JoinedBy.Add(LoginManager.Auth.User.LocalId);

                string json = JsonConvert.SerializeObject(activity);

                try
                {
					var response = await _firebase.Child("activities").PostAsync(json);
					var ac = JsonConvert.DeserializeObject<Activity>(response.Object);
					ac.ID = response.Key;

					App.Locator.MyActivitiesViewModel.Activities.Add(ac);
					App.Locator.AllActivitiesViewModel.AllActivities.Add(ac);

					clearFields();
                    _navigationService.GoBack();
                }
                catch (Exception ex) 
				{
					await _dialogService.ShowMessage(ex.Message, "Error");
				}
			} 
			else 
			{
				await _dialogService.ShowMessage("Fill out all Fields, except Description!", "Error");
			}
        }

        private void clearFields() 
        {
			Title = string.Empty;
            _date = DateTime.Now;
			SelectedCategory = string.Empty;
			Location = string.Empty;
			Description = string.Empty;
			NumberOfParticipants = string.Empty;
        }
    }
}

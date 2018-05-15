using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using StrangersToFriends.Model;
using StrangersToFriends.Helper;
using StrangersToFriends.Constants;
using StrangersToFriends.Infastructure.Services;
using StrangersToFriends.Infrastructure.Services;

using Firebase.Database;
using Firebase.Database.Query;

using Newtonsoft.Json;

namespace StrangersToFriends.ViewModel
{
	public class DetailsViewModel : ViewModelBase
    {
		private readonly INavigationService _navigationService;
		private readonly IDialogService _dialogService;
		public ICommand JoinOrLeaveCommand { get; private set; }
        
		private bool _hasBeenJoined;

		private Activity _activity;
		public Activity Activity 
		{
			get => _activity;
			set
			{
				_activity = value;
				Title = _activity.Title;
                Date = _activity.Date;
                Participants = _activity.NumberOfParticipants;
                Location = _activity.Location.Name;
                Description = _activity.Description;

				foreach (string id in _activity.JoinedBy)
				{
					if (id.Equals(LoginManager.Auth.User.LocalId)) {
						_hasBeenJoined = true;
						ButtonText = "Leave";
					} else {
						_hasBeenJoined = false;
                        ButtonText = "Join";
					}
				}

				if (_activity.JoinedBy.Count == 0) 
				{
					_hasBeenJoined = false;
                    ButtonText = "Join";
				}
			}
		}

		public string Title { get; set; }
		public DateTime Date { get; set; }
		public string Participants { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }

		public string ButtonText { get; set; }

		private FirebaseClient _firebase;

		public DetailsViewModel(INavigationService navigationService, IDialogService dialogService) 
		{
			_navigationService = navigationService;
			_dialogService = dialogService;
            
			JoinOrLeaveCommand = new RelayCommand(JoinOrLeaveActivity);

			_firebase = new FirebaseClient(Constant.FirebaseAppUri, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(LoginManager.Auth.FirebaseToken)
            });
		}

		private void JoinOrLeaveActivity()
		{
			if (_hasBeenJoined)
			{
				// leave
				_activity.Participants -= 1;
				_activity.NumberOfParticipants = _activity.Participants + "/" + _activity.NumberOfParticipants.Split('/')[1];

				for (int i = 0; i < _activity.JoinedBy.Count; i++)
				{
					if (_activity.JoinedBy[i].Equals(LoginManager.Auth.User.LocalId))
                    {
						_activity.JoinedBy.Remove(_activity.JoinedBy[i]);
                    }
				}
               
				updateDatabaseActivity(_activity);
			}
			else 
			{
				// join
				_activity.Participants += 1;
                _activity.NumberOfParticipants = _activity.Participants + "/" + _activity.NumberOfParticipants.Split('/')[1];
				_activity.JoinedBy.Add(LoginManager.Auth.User.LocalId);

				updateDatabaseActivity(_activity);
			}
		}

		private async void updateDatabaseActivity(Activity activity) {
			string json = JsonConvert.SerializeObject(activity);
            try
            {
				await _firebase.Child("activities").Child(_activity.ID).PutAsync(json);
                _navigationService.GoBack();
            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
		}
    }
}

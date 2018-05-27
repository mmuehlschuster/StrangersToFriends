using System;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using StrangersToFriends.Model;
using StrangersToFriends.Helper;
using StrangersToFriends.Infastructure.Services;
using StrangersToFriends.Infrastructure.Services;

namespace StrangersToFriends.ViewModel
{
	public class DetailsViewModel : ViewModelBase
	{
		private readonly INavigationService _navigationService;
		private readonly IDialogService _dialogService;
		public ICommand JoinOrLeaveCommand { get; private set; }

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

				if (_activity.JoinedBy.Contains(LoginManager.Auth.User.LocalId))
				{
					ButtonText = "Leave";
				}
				else
				{
					ButtonText = "Join";
				}
			}
		}

		private string _title;
		public string Title
		{
			get => _title;
			set
			{
				_title = value;
				RaisePropertyChanged();
			}
		}

		private DateTime _date;
		public DateTime Date
		{
			get => _date;
			set
			{
				_date = value;
				RaisePropertyChanged();
			}
		}

		private string _participants;
		public string Participants
		{
			get => _participants;
			set
			{
				_participants = value;
				RaisePropertyChanged();
			}
		}

		private string _location;
		public string Location
		{
			get => _location;
			set
			{
				_location = value;
				RaisePropertyChanged();
			}
		}

		private string _description;
		public string Description
		{
			get => _description;
			set
			{
				_description = value;
				RaisePropertyChanged();
			}
		}

		private string _buttonText;
		public string ButtonText
		{
			get => _buttonText;
			set
			{
				_buttonText = value;
				RaisePropertyChanged();
			}
		}

		private ContentManager _contentManager;

		public DetailsViewModel(INavigationService navigationService, IDialogService dialogService)
		{
			_navigationService = navigationService;
			_dialogService = dialogService;

			JoinOrLeaveCommand = new RelayCommand(JoinOrLeaveActivity);

			_contentManager = new ContentManager();
		}

		private void JoinOrLeaveActivity()
		{
			updateActivity(_activity);
			updateDatabaseActivity(_activity);
		}

		private void updateActivity(Activity activity)
		{
			if (activity.JoinedBy.Contains(LoginManager.Auth.User.LocalId))
            {
                // leave
                activity.Participants -= 1;
                activity.NumberOfParticipants = activity.Participants + "/" + activity.NumberOfParticipants.Split('/')[1];
                activity.JoinedBy.Remove(LoginManager.Auth.User.LocalId);
            }
            else
            {
                // join
                activity.Participants += 1;
                activity.NumberOfParticipants = activity.Participants + "/" + activity.NumberOfParticipants.Split('/')[1];
                activity.JoinedBy.Add(LoginManager.Auth.User.LocalId);
            }
		}

		private async void updateDatabaseActivity(Activity activity)
		{
			try
			{
				await _contentManager.UpdateActivity(activity);

				var activities = await _contentManager.GetActivities();

                App.Locator.AllActivitiesViewModel.Activities.Clear();
				App.Locator.AllActivitiesViewModel.FilteredActivities.Clear();
                App.Locator.MyActivitiesViewModel.Activities.Clear();
                App.Locator.JoinedActivitiesViewModel.Activities.Clear();

                foreach (var ac in activities)
                {
                    App.Locator.AllActivitiesViewModel.Activities.Add(ac.Object);
					App.Locator.AllActivitiesViewModel.FilteredActivities.Add(ac.Object);
                    
					if (ac.Object.CreatedBy.Equals(LoginManager.Auth.User.LocalId))
                    {
						App.Locator.MyActivitiesViewModel.Activities.Add(ac.Object);
                    }

					if (ac.Object.JoinedBy.Contains(LoginManager.Auth.User.LocalId))
                    {
						App.Locator.JoinedActivitiesViewModel.Activities.Add(ac.Object);
                    }
                }

				_navigationService.GoBack();
			}
			catch (Exception ex)
			{
				await _dialogService.ShowMessage(ex.Message, "Error");
			}
		}
	}
}

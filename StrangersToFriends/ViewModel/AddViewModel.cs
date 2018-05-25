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

		private ContentManager _contentManager;

		public AddViewModel(INavigationService navigationService, IDialogService dialogService)
		{
			_navigationService = navigationService;
			_dialogService = dialogService;

			CreateCommand = new RelayCommand(CreateActivity);

			Categories = Constant.Categories;

			_contentManager = new ContentManager();
		}

		private async void CreateActivity()
		{
			if (!string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(SelectedCategory) || !string.IsNullOrWhiteSpace(Location) || !string.IsNullOrWhiteSpace(NumberOfParticipants))
			{
				List<Xamarin.Forms.Maps.Position> positionList = new List<Xamarin.Forms.Maps.Position>();
				try
				{
					Geocoder geocoder = new Geocoder();
					IEnumerable<Xamarin.Forms.Maps.Position> positions = await geocoder.GetPositionsForAddressAsync(Location);
					foreach (Xamarin.Forms.Maps.Position position in positions)
					{
						positionList.Add(position);
					}

					if (positionList.Count != 0)
					{
						Location location = new Location(Location, new Model.Position(positionList[0].Latitude, positionList[0].Longitude));

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

						try
						{
							var response = await _contentManager.AddActivity(activity);
							var ac = JsonConvert.DeserializeObject<Activity>(response.Object);
							ac.ID = response.Key;
                            
							App.Locator.AllActivitiesViewModel.Activities.Add(ac);
							App.Locator.AllActivitiesViewModel.FilteredActivities.Add(ac);
							App.Locator.MyActivitiesViewModel.Activities.Add(ac);
							App.Locator.JoinedActivitiesViewModel.Activities.Add(ac);

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
						await _dialogService.ShowMessage("Location could not be found!", "Error");
					}
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

using System;
using System.Windows.Input;

using StrangersToFriends.Enums;
using StrangersToFriends.Helper;

using Firebase.Auth;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StrangersToFriends.Infrastructure.Services;
using StrangersToFriends.Infastructure.Services;

namespace StrangersToFriends.ViewModel
{
	public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
		private readonly IDialogService _dialogService;

		private bool _isSignup = false;

		public ICommand SignUpOrLoginCommand { get; private set; }
		public ICommand SignUpCommand { get; private set; }

		private string _title;
		public String Title 
		{
			get => _title; 
			set
			{
				_title = value;
				RaisePropertyChanged();
			}
		}

		public String Email { get; set; }
		public String Password { get; set; }

		private bool _isPasswordResetVisable;
		public bool IsPasswordResetVisable 
		{
			get => _isPasswordResetVisable; 
			set
			{
				_isPasswordResetVisable = value;
				RaisePropertyChanged();
			}
		}

		private string _signupButtonText;
		public String SignupButtonText 
		{
			get => _signupButtonText; 
			set
			{
				_signupButtonText = value;
				RaisePropertyChanged();
			}
		}

		public LoginViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
			_dialogService = dialogService;

			SignUpOrLoginCommand = new RelayCommand(SingnupOrLoginAsync);
			SignUpCommand = new RelayCommand(Register);

			clearFields();
        }
        
		private async void SingnupOrLoginAsync()  {
            
			if (!string.IsNullOrWhiteSpace(Email) || !string.IsNullOrWhiteSpace(Password))
			{
				FirebaseAuthLink auth;
                if (_isSignup)
                {
					try 
					{
						auth = await LoginManager.Instance.signupWithEmailAndPassword(Email, Password);
						LoginManager.Auth = auth;
						App.Locator.AllActivitiesViewModel.loadDataFromDatabase();
						App.Locator.MyActivitiesViewModel.loadDataFromDatabase();
						App.Locator.JoinedActivitiesViewModel.loadDataFromDatabase();
                        _navigationService.NavigateTo(AppPages.MainPage);
					}
					catch (FirebaseAuthException ex)
					{
						await _dialogService.ShowMessage(ex.Message, "Error");
					}
                }
                else
                {
                    try
                    {
						auth = await LoginManager.Instance.loginWithEmailAndPassword(Email, Password);
						LoginManager.Auth = auth;
						App.Locator.AllActivitiesViewModel.loadDataFromDatabase();
                        App.Locator.MyActivitiesViewModel.loadDataFromDatabase();
                        App.Locator.JoinedActivitiesViewModel.loadDataFromDatabase();
						_navigationService.NavigateTo(AppPages.MainPage);
                    }
                    catch (FirebaseAuthException ex)
                    {
						await _dialogService.ShowMessage("You have entered an invalid email or password!", "Login");
                    }
                }
			}
		}

		private void Register() {
			if (!_isSignup)
			{
				Title = "Sign up";
				SignupButtonText = "Login";
				IsPasswordResetVisable = false;
				_isSignup = true;
			} else {
				Title = "Login";
				SignupButtonText = "Sign up";
                _isSignup = false;
				IsPasswordResetVisable = true;
			}
		}

		private void clearFields() {
			Title = "Login";
			SignupButtonText = "Sign up";
			Email = string.Empty;
			Password = string.Empty;
			IsPasswordResetVisable = true;
		}
    }
}

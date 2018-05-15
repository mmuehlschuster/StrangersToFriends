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

		public String Title { get; set; }
		public String Email { get; set; }
		public String Password { get; set; }
       
		public String Signup { get; set; }
		public bool IsPasswordResetVisable { get; set; }

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
				Signup = "Login";
				IsPasswordResetVisable = false;
				_isSignup = true;
			} else {
				Title = "Login";
				Signup = "Sign up";
                _isSignup = false;
				IsPasswordResetVisable = true;
			}
		}

		private void clearFields() {
			Title = "Login";
			Signup = "Sign up";
			Email = string.Empty;
			Password = string.Empty;
			IsPasswordResetVisable = true;
		}
    }
}

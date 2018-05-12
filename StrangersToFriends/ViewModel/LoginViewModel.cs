using System;
using System.Windows.Input;

using StrangersToFriends.Enums;
using StrangersToFriends.Infastructure.Services;

using Firebase.Auth;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace StrangersToFriends.ViewModel
{
	public class LoginViewModel : ViewModelBase
    {
		public const string FirebaseAppKey = "AIzaSyCnvfHpScTlaTYR1IMlyeN4o_aQ_PVjzEM";

        private readonly INavigationService navigationService;

		private bool isSignup = false;
        
        private FirebaseAuthProvider authProvider = null;

		public ICommand SignUpOrLoginCommand { get; private set; }
		public ICommand SignUpCommand { get; private set; }

		public String Title { get; set; }
		public String Email { get; set; }
		public String Password { get; set; }
       
		public String Signup { get; set; }
		public bool IsPasswordResetVisable { get; set; }

        public LoginViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
			SignUpOrLoginCommand = new RelayCommand(SingnupOrLoginAsync);
			SignUpCommand = new RelayCommand(Register);

			clearFields();

			authProvider = new FirebaseAuthProvider(new FirebaseConfig(FirebaseAppKey));
        }
        
		private async void SingnupOrLoginAsync()  {
            
			if (!string.IsNullOrWhiteSpace(Email) || !string.IsNullOrWhiteSpace(Password))
			{
				FirebaseAuth auth = null;
                if (isSignup)
                {
					try 
					{
						auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);
                        navigationService.NavigateTo(AppPages.MainPage, auth);
					}
					catch (FirebaseAuthException ex)
					{
						// TODO: Dialog
					}
                }
                else
                {
                    try
                    {
						auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Password);
						navigationService.NavigateTo(AppPages.MainPage, auth);
                    }
                    catch (FirebaseAuthException ex)
                    {
						// TODO: Dialog 
                    }
                }
			}
		}

		private void Register() {
			if (!isSignup)
			{
				Title = "Sign up";
				Signup = "Login";
				IsPasswordResetVisable = false;
				isSignup = true;
			} else {
				Title = "Login";
				Signup = "Sign up";
                isSignup = false;
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

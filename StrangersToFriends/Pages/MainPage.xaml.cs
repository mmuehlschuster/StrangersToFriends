using System;
using System.Collections.Generic;

using Firebase.Auth;

using Xamarin.Forms;

namespace StrangersToFriends.Pages
{
    public partial class MainPage : TabbedPage
    {
		public MainPage(FirebaseAuthLink auth)
        {
            InitializeComponent();
            BindingContext = App.Locator.MainViewModel;
			App.Locator.MainViewModel.auth = auth;
			NavigationPage.SetHasBackButton(this, false);
        }
    }
}

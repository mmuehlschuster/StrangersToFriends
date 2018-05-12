using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StrangersToFriends.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.LoginViewModel;
			NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}

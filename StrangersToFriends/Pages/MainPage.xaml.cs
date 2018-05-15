using System;
using System.Collections.Generic;

using Firebase.Auth;

using Xamarin.Forms;

namespace StrangersToFriends.Pages
{
    public partial class MainPage : TabbedPage
    {
		public MainPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.MainViewModel;
			NavigationPage.SetHasBackButton(this, false);
        }
    }
}

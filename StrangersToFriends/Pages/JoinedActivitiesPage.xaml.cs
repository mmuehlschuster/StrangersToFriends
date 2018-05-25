using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StrangersToFriends.Pages
{
    public partial class JoinedActivitiesPage : ContentPage
    {
        public JoinedActivitiesPage()
        {
            InitializeComponent();
			BindingContext = App.Locator.JoinedActivitiesViewModel;
        }
    }
}

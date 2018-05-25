using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StrangersToFriends.Pages
{
    public partial class AllActivitiesPage : ContentPage
    {
        public AllActivitiesPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.AllActivitiesViewModel;
        }
	}
}

using System;
using System.Collections.Generic;

using StrangersToFriends.Model;

using Xamarin.Forms;

namespace StrangersToFriends.Pages
{
	public partial class DetailsPage : ContentPage
    {
		public DetailsPage(Activity activity)
        {
            InitializeComponent();
			BindingContext = App.Locator.DetailsViewModel; 
			App.Locator.DetailsViewModel.Activity = activity;
        }
    }
}

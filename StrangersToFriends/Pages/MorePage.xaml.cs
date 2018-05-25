using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StrangersToFriends.Pages
{
    public partial class MorePage : ContentPage
    {
        public MorePage()
        {
            InitializeComponent();
			BindingContext = App.Locator.MoreViewModel;
        }
    }
}

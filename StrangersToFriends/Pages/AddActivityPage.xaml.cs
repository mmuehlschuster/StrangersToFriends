using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StrangersToFriends.Pages
{
    public partial class AddActivityPage : ContentPage
    {
        public AddActivityPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.AddViewModel;
        }
    }
}

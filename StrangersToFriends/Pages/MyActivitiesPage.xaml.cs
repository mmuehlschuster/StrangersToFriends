using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StrangersToFriends.Pages
{
    public partial class MyActivitiesPage : ContentPage
    {
        public MyActivitiesPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.MyActivitiesViewModel;
        }
    }
}

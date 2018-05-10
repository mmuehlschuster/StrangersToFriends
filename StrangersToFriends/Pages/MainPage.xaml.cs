using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StrangersToFriends.Pages
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.MainViewModel;
        }
    }
}

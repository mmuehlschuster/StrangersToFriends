using Xamarin.Forms;
using StrangersToFriends.Pages;
using StrangersToFriends.ViewModel;
using StrangersToFriends.Enums;
using StrangersToFriends.Infastructure.Services;

using GalaSoft.MvvmLight.Ioc;

namespace StrangersToFriends
{
    public partial class App : Application
    {
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator 
        {
            get
            {
                return _locator ?? (_locator = new ViewModelLocator());
            }
        }

        public App()
        {
            InitializeComponent();

            INavigationService navigationService;

            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                // Setup navigation service:
                navigationService = new NavigationService();

                // Configure pages:
                navigationService.Configure(AppPages.MainPage, typeof(MainPage));
                navigationService.Configure(AppPages.AddActivityPage, typeof(AddActivityPage));
                navigationService.Configure(AppPages.AllActivitiesPage, typeof(AllActivitiesPage));
                navigationService.Configure(AppPages.MyActivitiesPage, typeof(MyActivitiesPage));
                navigationService.Configure(AppPages.SearchPage, typeof(SearchPage));
				navigationService.Configure(AppPages.LoginPage, typeof(LoginPage));

                // Register NavigationService in IoC container:
                SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            }

            else
                navigationService = SimpleIoc.Default.GetInstance<INavigationService>();

			// Create new Navigation Page and set MainPage as its default page:
			var firstPage = new NavigationPage(new LoginPage());
            // Set Navigation page as default page for Navigation Service:
            navigationService.Initialize(firstPage);
            // You have to also set MainPage property for the app:
            MainPage = firstPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

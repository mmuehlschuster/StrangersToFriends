using Xamarin.Forms;

using StrangersToFriends.Pages;
using StrangersToFriends.ViewModel;
using StrangersToFriends.Enums;
using StrangersToFriends.Infastructure.Services;
using StrangersToFriends.Infrastructure.Services;

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
			IDialogService dialogService;

			if (!SimpleIoc.Default.IsRegistered<Infastructure.Services.INavigationService>())
            {
                // Setup navigation service:
                navigationService = new NavigationService();
				dialogService = new DialogService();

                // Configure pages:
                navigationService.Configure(AppPages.MainPage, typeof(MainPage));
                navigationService.Configure(AppPages.AddActivityPage, typeof(AddActivityPage));
                navigationService.Configure(AppPages.AllActivitiesPage, typeof(AllActivitiesPage));
                navigationService.Configure(AppPages.MyActivitiesPage, typeof(MyActivitiesPage));
                navigationService.Configure(AppPages.SearchPage, typeof(SearchPage));
				navigationService.Configure(AppPages.LoginPage, typeof(LoginPage));
				navigationService.Configure(AppPages.DetailsPage, typeof(DetailsPage));

                // Register NavigationService in IoC container:
				SimpleIoc.Default.Register<INavigationService>(() => navigationService);
				SimpleIoc.Default.Register<IDialogService>(() => dialogService);
            }

            else
			{
				navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
				dialogService = SimpleIoc.Default.GetInstance<IDialogService>();
			}
				

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

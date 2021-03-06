/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:StrangersToFriends"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using StrangersToFriends.Infrastructure.Services;

namespace StrangersToFriends.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddViewModel>();
            SimpleIoc.Default.Register<AllActivitiesViewModel>();
            SimpleIoc.Default.Register<MyActivitiesViewModel>();
			SimpleIoc.Default.Register<JoinedActivitiesViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
			SimpleIoc.Default.Register<DetailsViewModel>();
			SimpleIoc.Default.Register<MoreViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public AddViewModel AddViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddViewModel>();
            }
        }

        public AllActivitiesViewModel AllActivitiesViewModel 
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AllActivitiesViewModel>();
            }
        }

        public MyActivitiesViewModel MyActivitiesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MyActivitiesViewModel>();
            }
        }

		public JoinedActivitiesViewModel JoinedActivitiesViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<JoinedActivitiesViewModel>();
			}
		}

        public SearchViewModel SearchViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchViewModel>();
            }
        }

        public LoginViewModel LoginViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

		public DetailsViewModel DetailsViewModel 
		{
			get
			{
				return ServiceLocator.Current.GetInstance<DetailsViewModel>();	
			}
		}

		public MoreViewModel MoreViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MoreViewModel>();
			}
		}
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MusicOre"
                           x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, DevicePath=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MusicOre.ViewModel
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
			SimpleIoc.Default.Register<DirectoryListingViewModel>();
			SimpleIoc.Default.Register<ManageLibraryViewModel>();
			SimpleIoc.Default.Register<PlayerViewModel>();
		}

		public MainViewModel Main
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MainViewModel>();
			}
		}

		public ManageLibraryViewModel ManageLibrary
		{
			get
			{
				return ServiceLocator.Current.GetInstance<ManageLibraryViewModel>();
			}
		}

		public DirectoryListingViewModel DirectoryListing
		{
			get
			{
				return ServiceLocator.Current.GetInstance<DirectoryListingViewModel>();
			}
		}

		public PlayerViewModel Player
		{
			get
			{
				return ServiceLocator.Current.GetInstance<PlayerViewModel>();
			}
		}

		public static void Cleanup()
		{
			// TODO Clear the ViewModels
		}
	}
}
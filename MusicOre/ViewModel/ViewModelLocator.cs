/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MusicOre.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  
  OR (WPF only):
  
  xmlns:vm="clr-namespace:MusicOre.ViewModel"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/

namespace MusicOre.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MusicOre.ViewModel"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// You can also use Blend to do all this with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// <para>
    /// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
    /// the Main property and bind to the ViewModelNameStatic property instead:
    /// </para>
    /// <code>
    /// xmlns:vm="clr-namespace:MusicOre.ViewModel"
    /// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
    /// </code>
    /// </summary>
    public class ViewModelLocator
    {

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view models
            ////}
            ////else
            ////{
            ////    // Create run time view models
            ////}

            CreateMain();
            CreateFullPlayer();
            CreateLibrary();
            CreateMiniPlayer();
        }

        #region MiniPlayer
        private static MiniPlayerViewModel _miniPlayer;

        /// <summary>
        /// Gets the MiniPlayer property.
        /// </summary>
        public static MiniPlayerViewModel MiniPlayerStatic
        {
            get
            {
                if (_miniPlayer == null)
                {
                    CreateMiniPlayer();
                }

                return _miniPlayer;
            }
        }

        /// <summary>
        /// Gets the MiniPlayer property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MiniPlayerViewModel MiniPlayer
        {
            get
            {
                return MiniPlayerStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the MiniPlayer property.
        /// </summary>
        public static void ClearMiniPlayer()
        {
            _main.Cleanup();
            _main = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the MiniPlayer property.
        /// </summary>
        public static void CreateMiniPlayer()
        {
            if (_miniPlayer == null)
            {
                _miniPlayer = new MiniPlayerViewModel();
            }
        }


        #endregion

        #region Main
        private static MainViewModel _main;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static MainViewModel MainStatic
        {
            get
            {
                if (_main == null)
                {
                    CreateMain();
                }

                return _main;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return MainStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearMain()
        {
            _main.Cleanup();
            _main = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateMain()
        {
            if (_main == null)
            {
                _main = new MainViewModel();
            }
        }

        #endregion

        #region Library

        private static LibraryViewModel _library;

        /// <summary>
        /// Gets the Library property.
        /// </summary>
        public static LibraryViewModel LibraryStatic
        {
            get
            {
                if (_library == null)
                {
                    CreateLibrary();
                }

                return _library;
            }
        }

        /// <summary>
        /// Gets the Library property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public LibraryViewModel Library
        {
            get
            {
                return LibraryStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Library property.
        /// </summary>
        public static void ClearLibrary()
        {
            _library.Cleanup();
            _library = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Library property.
        /// </summary>
        public static void CreateLibrary()
        {
            if (_library == null)
            {
                _library = new LibraryViewModel();
            }
        }

        #endregion

        #region FullPlayer

        private static FullPlayerViewModel _fullPlayerViewModel;

        /// <summary>
        /// Gets the FullPlayer property.
        /// </summary>
        public static FullPlayerViewModel FullPlayerStatic
        {
            get
            {
                if (_fullPlayerViewModel == null)
                {
                    CreateFullPlayer();
                }

                return _fullPlayerViewModel;
            }
        }

        /// <summary>
        /// Gets the FullPlayer property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FullPlayerViewModel FullPlayer
        {
            get
            {
                return FullPlayerStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the FullPlayer property.
        /// </summary>
        public static void ClearFullPlayer()
        {
            _fullPlayerViewModel.Cleanup();
            _fullPlayerViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the FullPlayer property.
        /// </summary>
        public static void CreateFullPlayer()
        {
            if (_fullPlayerViewModel == null)
            {
                _fullPlayerViewModel = new FullPlayerViewModel();
            }
        }


        #endregion

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            ClearMain();
            ClearLibrary();
            ClearMiniPlayer();
            ClearFullPlayer();
        }
    }
}
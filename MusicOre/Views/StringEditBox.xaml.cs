using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using HW.WpfControls.DAL;
using MahApps.Metro;

namespace HW.WpfControls.Dialogs
{
	/// <summary>
	/// Interaction logic for StringEditBox.xaml
	/// </summary>
	public partial class StringEditBox
	{
		public StringEditBox()
		{
			InitializeComponent();
			this.Loaded += StringEditBox_Loaded;
		}

		void StringEditBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if (SimpleIoc.Default.GetInstance<ISettingsProvider>().ColorTheme == ColorTheme.Dark)
			{
				ThemeManager.ChangeTheme(Window.GetWindow(this), ThemeManager.DefaultAccents.First(a => a.Name == "Blue"), Theme.Dark);
			}
		}
	}
}

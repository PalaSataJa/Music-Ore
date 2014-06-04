using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using MusicOre.Model;
using MusicOre.ViewModel;

namespace MusicOre.Views
{
	/// <summary>
	/// Interaction logic for RatingSelector.xaml
	/// </summary>
	public partial class RatingSelector : UserControl
	{
		public RatingSelector()
		{
			InitializeComponent();
			this.Loaded += RatingSelector_Loaded;
		}

		void RatingSelector_Loaded(object sender, RoutedEventArgs e)
		{
			ComboBox.ItemsSource = Enum.GetValues(typeof(Rating)).Cast<Rating>();
		}
		private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.RemovedItems.Count == 0)
			{
				//first load - todo resolve
				return;
			}
			var rating = (Rating)e.AddedItems[0];

			Messenger.Default.Send(new RatingSelectedMessage(rating), PlayerViewModel.Token);
		}
	}
}

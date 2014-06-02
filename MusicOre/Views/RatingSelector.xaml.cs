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
using MusicOre.Model;

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
			//(this.Content as FrameworkElement).DataContext = this; 
			this.Loaded += RatingSelector_Loaded;
		}

		void RatingSelector_Loaded(object sender, RoutedEventArgs e)
		{
			ComboBox.ItemsSource = Enum.GetValues(typeof(Rating)).Cast<Rating>();
		}

		public static readonly DependencyProperty SelectedRatingProperty = DependencyProperty.Register(
			"SelectedRating", typeof (Rating), typeof (RatingSelector), new PropertyMetadata(Rating.Dunno));
		
		public Rating SelectedRating
		{
			get { return (Rating) GetValue(SelectedRatingProperty); }
			set { SetValue(SelectedRatingProperty, value); }
		}

		private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var rating = (Rating)e.AddedItems[0];
			SelectedRating = rating;
		}
	}
}

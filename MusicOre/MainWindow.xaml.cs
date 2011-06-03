using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Win32;
using MusicOre.Model;
using MusicOre.Utility;
using MusicOre.ViewModel;

namespace MusicOre
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Types.ViewType viewType = Types.ViewType.MiniPlayer;
            using (LibraryEntities libraryEntities = new LibraryEntities())
            {
                var e = libraryEntities.StartupParams.Where(p => p.ParamName == "DefaultView").Select(p => p.ParamValue).FirstOrDefault();
                if (!string.IsNullOrEmpty(e))
                {
                    Enum.TryParse(e, out viewType);
                }
            }
            Window view;
            switch (viewType)
            {
                case Types.ViewType.MiniPlayer:
                    view = new MiniPlayer();
                    break;
                case Types.ViewType.FullPlayer:
                    view = new FullPlayer();
                    break;
                default:
                    view = new MiniPlayer();
                    break;
            }

            Application.Current.MainWindow = view;
            this.Close();
            view.Show();

        }


    }
}

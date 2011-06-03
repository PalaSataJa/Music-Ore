using System;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using MusicOre.ViewModel;

namespace MusicOre
{
    /// <summary>
    /// Description for MiniPlayer.
    /// </summary>
    public partial class MiniPlayer : Window
    {
        /// <summary>
        /// Initializes a new instance of the MiniPlayer class.
        /// </summary>
        public MiniPlayer()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();
            var s = of.FileName;
            media.LoadedBehavior = MediaState.Manual;
            media.Source = new Uri(s);

            //((MiniPlayerViewModel)DataContext).PlayCommand.Execute(media);
        }


    }
}
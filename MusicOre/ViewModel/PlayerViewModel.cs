using System;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MusicOre
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class PlayerViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the PlayerViewModel class.
        /// </summary>
        public PlayerViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                PlayCommand = new RelayCommand<MediaElement>(
                    Play,
                    (media) => true);
                StopCommand = new RelayCommand<MediaElement>(
                    Stop,
                    (media) => true);
                PauseCommand = new RelayCommand<MediaElement>(
                    Pause,
                    (media) => true);
            }
        }


        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}

        #region Implementation

        private void Play(MediaElement media)
        {
            MessageBox.Show("Play");
        }


        private void Stop(MediaElement media)
        {
            MessageBox.Show("Stop");
        }


        private void Pause(MediaElement media)
        {
            MessageBox.Show("Pause");
        }

        #endregion

        #region Commands

        public RelayCommand<MediaElement> PlayCommand
        {
            get;
            private set;
        }

        public RelayCommand<MediaElement> StopCommand
        {
            get;
            private set;
        }

        public RelayCommand<MediaElement> PauseCommand
        {
            get;
            private set;
        }

        #endregion
    }
}
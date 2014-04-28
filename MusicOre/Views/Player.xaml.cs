using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using MusicOre.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MusicOre.Views
{
	/// <summary>
	/// Interaction logic for Player.xaml
	/// </summary>
	public partial class Player : UserControl
	{
		private DispatcherTimer progressTimer;

		public Player()
		{
			InitializeComponent();
			Messenger.Default.Register<PropertyChangedMessage<FileEntry>>(this, UriChanged);
			MediaElement.MediaEnded += MediaElement_MediaEnded;
			MediaElement.MediaOpened += MediaElement_MediaOpened;
		}

		private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
		{
			Messenger.Default.Send(new FileEndedMessage(), PlayerViewModel.Token);
		}

		private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
		{
			this.progressTimer = new DispatcherTimer();
			this.progressTimer.Interval = TimeSpan.FromSeconds(1);
			this.progressTimer.Tick += progressTimer_Tick;
			this.progressTimer.Start();
			ProgressBar.Maximum = MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
		}

		private void OpenDialog(DialogMessage obj)
		{
			var openFileDialog = new OpenFileDialog();
			openFileDialog.DefaultExt = "mp3";
			openFileDialog.Title = obj.Content;
			bool? showDialog = openFileDialog.ShowDialog();
			if (showDialog.HasValue && showDialog.Value)
			{
				Messenger.Default.Send(new GenericMessage<string>(openFileDialog.FileName), PlayerViewModel.Token);
			}
		}

		private void PlayPauseClick(object sender, RoutedEventArgs e)
		{
			if ((string)(sender as Button).Content == "Play")
			{
				MediaElement.Pause();
				(sender as Button).Content = "Pause";
			}
			else
			{
				MediaElement.Play();
				(sender as Button).Content = "Play";
			}
		}

		private void ProgressBar_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			double position = e.GetPosition(ProgressBar).X;
			double percent = position / ProgressBar.ActualWidth;
			TimeSpan duration = MediaElement.NaturalDuration.TimeSpan;
			int newPosition = (int)(duration.TotalSeconds * percent);
			MediaElement.Position = new TimeSpan(0, 0, newPosition);
			UpdateProgress();
		}

		private void progressTimer_Tick(object sender, EventArgs e)
		{
			UpdateProgress();
		}

		private void UpdateProgress()
		{
			ProgressBar.Value = MediaElement.Position.TotalSeconds;
			ProgressBar.ToolTip = string.Format("{0}:{1}", MediaElement.Position.Minutes.ToString("00"),
					MediaElement.Position.Seconds.ToString("00"));
		}
		private void UriChanged(PropertyChangedMessage<FileEntry> message)
		{
			MediaElement.Stop();
			MediaElement.Source = new Uri(message.NewValue.Uri, UriKind.RelativeOrAbsolute);
			MediaElement.Play();
		}
	}
}
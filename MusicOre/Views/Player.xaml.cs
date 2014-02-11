﻿using System;
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
using Microsoft.Win32;
using MusicOre.ViewModel;

namespace MusicOre.Views
{
	/// <summary>
	/// Interaction logic for Player.xaml
	/// </summary>
	public partial class Player : UserControl
	{
		public Player()
		{
			InitializeComponent();
			Messenger.Default.Register<PropertyChangedMessage<string>>(this, UriChanged);
			Messenger.Default.Register<MediaMessage>(this, PlayPause);
		}

		private bool state = false;

		private void PlayPause(MediaMessage obj)
		{
			if (state)
				MediaElement.Pause();
			else
				MediaElement.Play();
		}
		
		private void UriChanged(PropertyChangedMessage<string> message)
		{
			MediaElement.Stop();
			MediaElement.Source = new Uri(message.NewValue, UriKind.RelativeOrAbsolute);
			MediaElement.Play();
		}
	}
}

using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MusicOre.Model;

namespace MusicOre.ViewModel
{
	public class MediaMessage: MessageBase
	{
		
	}
	public class PlayerViewModel : ViewModelBase
	{
		private Playlist playlist;
		public static object Token;

		public PlayerViewModel()
		{
			MessengerInstance.Register<GenericMessage<string>>(this, PlayerViewModel.Token, FilesSelected);
		}

		private void FilesSelected(GenericMessage<string> message)
		{
			PlayUri = message.Content;
		}

		#region UpNext

		private ObservableCollection<FileEntry> _upNext = new ObservableCollection<FileEntry>();

		/// <summary>
		/// Sets and gets the UpNext property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public ObservableCollection<FileEntry> UpNext
		{
			get
			{
				return _upNext;
			}
			set
			{
				Set("UpNext", ref _upNext, value);
			}
		}

		#endregion UpNext

		#region PlayList

		private ObservableCollection<FileEntry> _playlistEntries = new ObservableCollection<FileEntry>();

		/// <summary>
		/// Sets and gets the PlayList property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public ObservableCollection<FileEntry> PlayList
		{
			get
			{
				return _playlistEntries;
			}
			set
			{
				Set("PlayList", ref _playlistEntries, value);
			}
		}

		#endregion PlayList

		#region PlayUri

		/// <summary>
		/// The <see cref="PlayUri" /> property's name.
		/// </summary>
		public const string PlayUriPropertyName = "PlayUri";

		private string _playUri = null;

		/// <summary>
		/// Sets and gets the PlayUri property.
		/// Changes to that property's value raise the PropertyChanged event.
		/// This property's value is broadcasted by the MessengerInstance when it changes.
		/// </summary>
		public string PlayUri
		{
			get
			{
				return _playUri;
			}
			set
			{
				Set(() => PlayUri, ref _playUri, value, true);
			}
		}

		#endregion

		#region Previous
		private RelayCommand previous;

		/// <summary>
		/// Gets the Next.
		/// </summary>
		public RelayCommand Previous
		{
			get
			{
				return previous
						?? (previous = new RelayCommand(
																	() =>
																	{
																		playlist.Previous();
																		PlayUri = playlist.Current.Uri;
																		UpNext = new ObservableCollection<FileEntry>(playlist.UpNext.Take(5).ToList());
																	}));
			}
		}
		#endregion Previous

		#region Next
		private RelayCommand nextCommand;

		/// <summary>
		/// Gets the Next.
		/// </summary>
		public RelayCommand Next
		{
			get
			{
				return nextCommand
						?? (nextCommand = new RelayCommand(
																	() =>
																	{
																		playlist.Next();
																		PlayUri = playlist.Current.Uri;
																		UpNext = new ObservableCollection<FileEntry>(playlist.UpNext.Take(5).ToList());
																	}));
			}
		}
		#endregion Next

		#region Play
		private RelayCommand playCommand;

		/// <summary>
		/// Gets the Next.
		/// </summary>
		public RelayCommand Play
		{
			get
			{
				return playCommand
						?? (playCommand = new RelayCommand(
																	() => MessengerInstance.Send(new MediaMessage())));
			}
		}
		#endregion Next

		#region Select
		private RelayCommand _selectCommand;

		/// <summary>
		/// Gets the Select.
		/// </summary>
		public RelayCommand Select
		{
			get
			{
				return _selectCommand
							 ?? (_selectCommand = new RelayCommand(
								 () => { }));
			}
		}
		#endregion Select
	}
}
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using MusicOre.Model;
using Ploeh.AutoFixture;

namespace MusicOre.ViewModel
{
	public class FileEndedMessage : MessageBase { }

	public class RatingSelectedMessage : MessageBase
	{
		public RatingSelectedMessage( Rating newRating)
		{
			this.NewRating = newRating;
		}

		public readonly Rating NewRating;
	}

	public class PlayerViewModel : ViewModelBase
	{
		public static object Token;
		private Playlist playlist;
		public PlayerViewModel()
		{
			MessengerInstance.Register<GenericMessage<string>>(this, PlayerViewModel.Token, FilesSelected);
			MessengerInstance.Register<FileEndedMessage>(this, PlayerViewModel.Token, message => GoNext());
			MessengerInstance.Register<RatingSelectedMessage>(this, PlayerViewModel.Token, UpdateRating);
			if (IsInDesignMode)
			{
				CurrentMedia = new Fixture().Create<MediaEntry>();
			}
		}

		private void UpdateRating(RatingSelectedMessage message)
		{
			if (message.NewRating != CurrentMedia.Rating)
			{
				CurrentMedia.UpdateRating(message.NewRating);
			}
		}

		private void FilesSelected(GenericMessage<string> message)
		{
			//CurrentMedia = message.Content;
		}

		#region UpNext

		private ObservableCollection<MediaEntry> _upNext = new ObservableCollection<MediaEntry>();

		/// <summary>
		/// Sets and gets the UpNext property.
		/// Changes to that property's value raise the PropertyChanged event.
		/// </summary>
		public ObservableCollection<MediaEntry> UpNext
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

		private ObservableCollection<MediaEntry> _playlistEntries = new ObservableCollection<MediaEntry>();

		/// <summary>
		/// Sets and gets the PlayList property.
		/// Changes to that property's value raise the PropertyChanged event.
		/// </summary>
		public ObservableCollection<MediaEntry> PlayList
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

		#region CurrentMedia

		/// <summary>
		/// The <see cref="CurrentMedia" /> property's name.
		/// </summary>
		public const string CurrentMediaPropertyName = "CurrentMedia";

		private MediaEntry _currentMedia = null;

		/// <summary>
		/// Sets and gets the PlayUri property.
		/// Changes to that property's value raise the PropertyChanged event.
		/// This property's value is broadcasted by the MessengerInstance when it changes.
		/// </summary>
		public MediaEntry CurrentMedia
		{
			get
			{
				return _currentMedia;
			}
			set
			{
				Set(() => CurrentMedia, ref _currentMedia, value, true);
			}
		}

		#endregion CurrentMedia

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
																															CurrentMedia = playlist.Current;
																															UpNext = new ObservableCollection<MediaEntry>(playlist.UpNext.Take(5).ToList());
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
																														GoNext));
			}
		}

		private void GoNext()
		{
			playlist.Next();
			CurrentMedia = playlist.Current;
			UpNext = new ObservableCollection<MediaEntry>(playlist.UpNext.Take(5).ToList());
		}

		#endregion Next

		#region PlayAll

		private RelayCommand _playAllCommand;

		/// <summary>
		/// Gets the Select.
		/// </summary>
		public RelayCommand PlayAll
		{
			get
			{
				return _playAllCommand
										 ?? (_playAllCommand = new RelayCommand(
														 () =>
														 {
															 playlist = new Playlist();
															 playlist.AllMusic();
															 CurrentMedia = playlist.Current;
														 }));
			}
		}

		#endregion PlayAll
	}
}
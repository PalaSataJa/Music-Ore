using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace MusicOre.ViewModel
{
	public class PlayerViewModel : ViewModelBase
	{
		public static object Token;

		public PlayerViewModel()
		{
			MessengerInstance.Register<GenericMessage<string>>(this, PlayerViewModel.Token, FilesSelected);
		}

		private void FilesSelected(GenericMessage<string> message)
		{
			PlayUri = message.Content;
		}

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
																	() => MessengerInstance.Send<DialogMessage>(new DialogMessage(this, "Select File", result => { }))));
			}
		}
		#endregion Select
	}
}
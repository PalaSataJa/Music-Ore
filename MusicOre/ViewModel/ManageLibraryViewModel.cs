using System;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MusicOre.Model;

namespace MusicOre.ViewModel
{
	public class ManageLibraryViewModel : ViewModelBase
	{
		public static object Token = new object();

		public ManageLibraryViewModel()
		{
			Messenger.Default.Register<GenericMessage<string>>(this, Token, FolderSelected);
		}

		private void FolderSelected(GenericMessage<string> message)
		{
			LibraryOperations.ScanDirectory(message.Content);
		}


		#region AddFolder	Command

		private RelayCommand _addFolderCommand;

		/// <summary>
		/// Gets the MyCommand.
		/// </summary>
		public RelayCommand AddFolder
		{
			get
			{
				return _addFolderCommand
						?? (_addFolderCommand = new RelayCommand(
																	() => MessengerInstance.Send<DialogMessage>(new DialogMessage(this, "Select File", result => { }))));
			}
		}

		#endregion AddFolder	Command
	}
}
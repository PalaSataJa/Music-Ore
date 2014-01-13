using GalaSoft.MvvmLight.Messaging;
using Microsoft.WindowsAPICodePack.Dialogs;
using MusicOre.ViewModel;
using System.Windows.Controls;

namespace MusicOre.Views
{
	/// <summary>
	/// Interaction logic for ManageLibrary.xaml
	/// </summary>
	public partial class ManageLibrary : UserControl
	{
		public ManageLibrary()
		{
			InitializeComponent();
			Messenger.Default.Register<DialogMessage>(this, OpenDialog);

		}

		private void OpenDialog(DialogMessage obj)
		{
			var dlg = new CommonOpenFileDialog();
			dlg.Title = "Select Folder to add to Library";
			dlg.IsFolderPicker = true;

			dlg.AddToMostRecentlyUsedList = false;
			dlg.AllowNonFileSystemItems = false;
			dlg.EnsureFileExists = true;
			dlg.EnsurePathExists = true;
			dlg.EnsureReadOnly = false;
			dlg.EnsureValidNames = true;
			dlg.Multiselect = false;
			dlg.ShowPlacesList = true;

			if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
			{
				var folder = dlg.FileName;
				Messenger.Default.Send(new GenericMessage<string>(folder), ManageLibraryViewModel.Token);
			}
		}
	}
}
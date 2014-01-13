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
			var directoryInfo = new DirectoryInfo(message.Content);
			//todo async
			using (var context = new LibraryContext())
			{
				var device = context.Devices.FirstOrDefault(d => d.Name.Equals(Environment.MachineName));
				var rootFolder = context.RootFolders.FirstOrDefault(f => f.Name.Equals(directoryInfo.Name));
				if (rootFolder == null)
				{
					rootFolder = new RootFolder { Name = directoryInfo.Name };
				}
				if (device == null)
				{
					device = FileSystemOperations.AddCurrentDevice(context);
				}
				var rootFolderPath = new RootFolderPath
				{
					Device = device,
					RootFolder = rootFolder,
					Path = directoryInfo.FullName
				};
				context.RootFolderPaths.Add(rootFolderPath);


				var FileEntries =
					Directory.GetFiles(message.Content,"*", SearchOption.AllDirectories)
						.Select(filename => new FileInfo(filename))
						.Select(
							fileInfo =>
								new MediaEntry
								{
									Filename = fileInfo.Name.Replace(fileInfo.Extension,""),
									Extension = fileInfo.Extension,
									RootFolder = rootFolder,
									RelativeFolderPath = fileInfo.DirectoryName.Replace(directoryInfo.FullName, "")
								});
				context.MediaEntries.AddRange(FileEntries);

				context.SaveChanges();
				//todo add id3 task
			}
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
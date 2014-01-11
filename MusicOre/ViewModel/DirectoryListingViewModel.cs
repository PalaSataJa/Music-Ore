using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight;

namespace MusicOre.ViewModel
{
	public class FileEntry
	{
		public string FileName { get; set; }
		public string Uri { get; set; }
		public string Artist { get; set; }
		public string Title { get; set; }
		public string Album { get; set; }
		public DateTime Duration { get; set; }

		public static FileEntry Get(string parentPath, string filename)
		{
			//todo: get id3 info

			return new FileEntry { FileName = filename, Uri = parentPath + @"\" + filename };
		}
	}

	public class DirectoryEntry
	{
		public string Name { get; set; }
		public bool IsEmpty { get; set; }

		public static DirectoryEntry Get(string parentPath, string directoryName)
		{
			return new DirectoryEntry
			{
				Name = directoryName,
				IsEmpty = Directory.GetFileSystemEntries(parentPath + @"\" + directoryName).Any()
			};
		}
	}

	public class DirectoryListingViewModel : ViewModelBase
	{
		#region FileEntries
		/// <summary>
		/// The <see cref="FileEntries" /> property's name.
		/// </summary>
		public const string FileEntriesPropertyName = "FileEntries";

		private ObservableCollection<FileEntry> _fileEntries = new ObservableCollection<FileEntry>();

		/// <summary>
		/// Sets and gets the FileEntries property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public ObservableCollection<FileEntry> FileEntries
		{
			get
			{
				return _fileEntries;
			}
			set
			{
				Set(FileEntriesPropertyName, ref _fileEntries, value);
			}
		}
		#endregion

		#region DirectoryEntries
		/// <summary>
		/// The <see cref="DirectoryEntries" /> property's name.
		/// </summary>
		public const string DirectoryEntriesPropertyName = "DirectoryEntries";

		private ObservableCollection<DirectoryEntry> _directoryEntries = new ObservableCollection<DirectoryEntry>();

		/// <summary>
		/// Sets and gets the DirectoryEntries property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public ObservableCollection<DirectoryEntry> DirectoryEntries
		{
			get
			{
				return _directoryEntries;
			}
			set
			{
				Set(DirectoryEntriesPropertyName, ref _directoryEntries, value);
			}
		}
		#endregion DirectoryEntries

		public void ListEntries(string parentDirectory)
		{
			FileEntries = new ObservableCollection<FileEntry>(
					Directory.GetFiles(parentDirectory).Select(filename => FileEntry.Get(parentDirectory, filename)));
			DirectoryEntries = new ObservableCollection<DirectoryEntry>(
					Directory.GetDirectories(parentDirectory).Select(directoryName => DirectoryEntry.Get(parentDirectory, directoryName)));
		}

	}
}

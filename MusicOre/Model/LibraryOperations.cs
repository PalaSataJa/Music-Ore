using MusicOre.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicOre.Model
{
	public static class LibraryOperations
	{
		public static List<string> ValidExtensions = new List<string> { ".mp3", ".wav", ".ogg", ".m4a" };

		private static List<FileEntry> _currentEntries;

		public static List<FileEntry> CurrentDeviceMediaEntries
		{
			get
			{
				return _currentEntries ?? (_currentEntries = GetAllMedia());
			}
		}

		public static string GetPathOnCurrentDevice(this MediaEntry media)
		{
			return media.RootFolder.PathsOnDevices.First(d => d.Device.Name == Environment.MachineName).Path + @"\" +
						 media.RelativeFolderPath + @"\" +
						 media.Filename + media.Extension;
		}

		public static Device InitCurrentDevice()
		{
			using (var context = new LibraryContext())
			{
				return InitCurrentDevice(context);
			}
		}

		public static void ScanDirectory(string dirName)
		{
			var directoryInfo = new DirectoryInfo(dirName);
			//todo async
			using (var context = new LibraryContext())
			{
				var device = InitCurrentDevice(context);
				RootFolder rootFolder;
				if (context.RootFolders.FirstOrDefault(f => f.Name.Equals(directoryInfo.Name)) != null)
					rootFolder = context.RootFolders.FirstOrDefault(f => f.Name.Equals(directoryInfo.Name));
				else rootFolder = new RootFolder { Name = directoryInfo.Name, MediaEntries = new List<MediaEntry>() };

				var rootFolderPath = new RootFolderPath
				{
					Device = device,
					RootFolder = rootFolder,
					Path = directoryInfo.FullName
				};
				RootFolderPath firstOrDefault = context.RootFolderPaths.Any() ? context.RootFolderPaths.FirstOrDefault(rf => rf.Device.Name == Environment.MachineName && rf.RootFolder.Name == directoryInfo.Name) : null;
				if (firstOrDefault != null)
				{
					firstOrDefault.Path = directoryInfo.FullName;
				}
				else
				{
					context.RootFolderPaths.Add(rootFolderPath);
				}

				var FileEntries =
						Directory.GetFiles(dirName, "*", SearchOption.AllDirectories)
								.Select(filename => new FileInfo(filename))
								.Where(fileInfo => ValidExtensions.Contains(fileInfo.Extension))
								.Select(
										fileInfo =>
												new MediaEntry
												{
													Filename =
															!string.IsNullOrEmpty(fileInfo.Extension) ? fileInfo.Name.Replace(fileInfo.Extension, "") : fileInfo.Name,
													Extension = fileInfo.Extension,
													RootFolder = rootFolder,
													RelativeFolderPath = fileInfo.DirectoryName.Replace(directoryInfo.FullName, "")
												});
				foreach (var mediaEntry in FileEntries)
				{
					var existing = rootFolder.MediaEntries.FirstOrDefault(e => e.Equals(mediaEntry));
					if (existing != null)
					{
						existing.Update(mediaEntry);
					}
					else
					{
						rootFolder.MediaEntries.Add(mediaEntry);
					}
				}

				context.SaveChanges();
				//todo add id3 task
			}
		}

		private static Device AddCurrentDevice(LibraryContext context)
		{
			Device entity = new Device { Name = System.Environment.MachineName };

			context.Devices.Add(entity);
			context.SaveChanges();
			return entity;
		}

		private static List<FileEntry> GetAllMedia()
		{
			using (var context = new LibraryContext())
			{
				InitCurrentDevice(context);

				var dev = context.Devices.First(d => d.Name == Environment.MachineName);
				var rf = dev.RootFolderPaths.Select(f => f.RootFolder).ToList();
				return rf.SelectMany(f => f.MediaEntries)
				.ToList().Select(FileEntry.Get).ToList();
			}
		}

		private static Device InitCurrentDevice(LibraryContext context)
		{
			var device = context.Devices.FirstOrDefault(d => d.Name.Equals(Environment.MachineName));
			if (device == null)
			{
				device = LibraryOperations.AddCurrentDevice(context);
			}
			return device;
		}
	}
}
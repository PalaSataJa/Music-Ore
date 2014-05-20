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
			return media.Root.DevicePaths.First(d => d.Device.Name == Environment.MachineName).Path + @"\" +
						 media.RelativePath + @"\" +
						 media.Filename + media.Extension;
		}

		public static Device InitCurrentDevice()
		{
			using (var context = new LibraryContext())
			{
				return InitCurrentDevice(context);
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
				var rf = dev.DevicePaths.Select(f => f.Root).ToList();
				return rf.SelectMany(f => f.MediaEntries)
				.ToList().Select(FileEntry.Get).ToList();
			}
		}

		private static Device InitCurrentDevice(LibraryContext context)
		{
			return context.Devices.FirstOrDefault(d => d.Name.Equals(Environment.MachineName)) ?? AddCurrentDevice(context);	
		}

		internal static void ScanDirectory(string directoryPath, string rootName)
		{
			var directoryInfo = new DirectoryInfo(directoryPath);
			var updateQueue = new Dictionary<string, MediaEntry>();
			using (var context = new LibraryContext())
			{
				var device = InitCurrentDevice(context);

				var root = context.Roots.FirstOrDefault(r => r.Name == rootName) ?? new Root();

				DevicePath path;
				//todo assuming now that no duplicated roots will be added

				if (root.DevicePaths.FirstOrDefault(p => p.Device == device) != null)
					path = root.DevicePaths.FirstOrDefault(p => p.Device == device);
				else
				{
					path = new DevicePath { Device = device };
					root.DevicePaths.Add(path);
				}

				//todo for now fileEntry is considered to be added to one root only
				var fileEntries =
					Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories)
						.Select(filename => new FileInfo(filename))
						.Where(fileInfo => ValidExtensions.Contains(fileInfo.Extension))
						.Select(
							fileInfo =>
								new
								{
									MediaEntry =
										new MediaEntry
										{
											Filename =
												!string.IsNullOrEmpty(fileInfo.Extension) ? fileInfo.Name.Replace(fileInfo.Extension, "") : fileInfo.Name,
											Extension = fileInfo.Extension,
											Root = root,
											RelativePath = fileInfo.DirectoryName.Replace(directoryInfo.FullName, "")
										},
									FilePath = fileInfo.FullName
								});

				foreach (var fileEntry in fileEntries)
				{
					var existing = root.MediaEntries.FirstOrDefault(me => me.Equals(fileEntry.MediaEntry));
					fileEntry.MediaEntry.ComputeMd5(fileEntry.FilePath);
					if (existing == null)
					{
						root.MediaEntries.Add(fileEntry.MediaEntry);
						updateQueue.Add(fileEntry.FilePath, fileEntry.MediaEntry);
					}
					else
					{
						if (existing.Md5 != fileEntry.MediaEntry.Md5)
						{
							updateQueue.Add(fileEntry.FilePath, existing);
						}
					}
				}

				context.SaveChanges();
			}


			updateQueue.ForceId3Update();
		}
	}
}
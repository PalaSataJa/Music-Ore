using System.Collections.ObjectModel;
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
			var updateQueue = new List<MediaEntry>();
			using (var context = new LibraryContext())
			{
				var device = InitCurrentDevice(context);
				context.Devices.Add(device);

				Root root = context.Roots.FirstOrDefault(r => r.Name == rootName);
				if (root != null)
				{
					DevicePath path = root.DevicePaths.FirstOrDefault(p => p.Device == device);
					if (path != null)
					{
						if (!path.Path.Equals(directoryPath, StringComparison.InvariantCultureIgnoreCase))
						{
							throw new ArgumentException("Path conflicts with existing");
						}
					}
					else
					{
						root.DevicePaths.Add(new DevicePath { Path = directoryPath, Device = device});
					}
				}
				else
				{
					root = new Root { Name = rootName,MediaEntries = new Collection<MediaEntry>(), DevicePaths = new Collection<DevicePath> { new DevicePath { Path = directoryPath, Device = device} } };
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
											RelativePath = fileInfo.DirectoryName.Replace(directoryInfo.FullName, ""),
											FullPath = fileInfo.FullName,
											LastUpdateDate = fileInfo.LastWriteTimeUtc > fileInfo.CreationTimeUtc ? fileInfo.LastWriteTimeUtc : fileInfo.CreationTimeUtc
										},
									FilePath = fileInfo.FullName
								});

				foreach (var fileEntry in fileEntries)
				{
					var existing = root.MediaEntries.FirstOrDefault(me => me.Equals(fileEntry.MediaEntry));
					if (existing == null)
					{
						root.MediaEntries.Add(fileEntry.MediaEntry);
						updateQueue.Add(fileEntry.MediaEntry);
					}
					else
					{
						if (existing.LastUpdateDate != fileEntry.MediaEntry.LastUpdateDate)
						{
							updateQueue.Add(existing);
						}
					}
				}

				context.SaveChanges();
			}


			//updateQueue.ForceId3Update();
		}
	}
}
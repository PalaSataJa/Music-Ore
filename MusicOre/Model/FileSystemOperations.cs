using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOre.Model
{
	public class FileSystemOperations
	{
		public static Device AddCurrentDevice()
		{
			using (var context = new LibraryContext())
			{
				return AddCurrentDevice(context);
			}
		}

		public static Device AddCurrentDevice(LibraryContext context)
		{
			var machineName = System.Environment.MachineName;
			Device entity = context.Devices.FirstOrDefault(device => device.Name == machineName);
			if (entity == null)
			{
				entity = new Device { Name = machineName };
				context.Devices.Add(entity);
				context.SaveChanges();
			}
			return entity;
		}

		public static void UpdateLibrary()
		{
			using (var context = new LibraryContext())
			{
				var device = context.Devices.FirstOrDefault(d => d.Name.Equals(Environment.MachineName));
				if (device != null)
				{
					var folders = device.RootFolderPaths;
					foreach (var path in folders)
					{
						ListFolder(path.Path);
					}
				}
			}
		}


		public static void ListFolder(string path)
		{
			var directoryInfo = new DirectoryInfo(path);
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
					Directory.GetFiles(path, "*", SearchOption.AllDirectories)
						.Select(filename => new FileInfo(filename))
						.Select(
							fileInfo =>
								new MediaEntry
								{
									Filename = fileInfo.Name.Replace(fileInfo.Extension, ""),
									Extension = fileInfo.Extension,
									RootFolder = rootFolder,
									RelativeFolderPath = fileInfo.DirectoryName.Replace(directoryInfo.FullName, "")
								});

				var duplicates = FileEntries.Where(newEntry => context.MediaEntries.Any(entry => entry.Equals(newEntry)));
				//todo duplicate handling ?

				context.MediaEntries.AddRange(FileEntries.Except(duplicates));

				context.SaveChanges();
				//todo add id3 task
			}
		}
	}
}

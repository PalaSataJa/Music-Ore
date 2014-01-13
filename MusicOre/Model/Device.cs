using System.Collections.Generic;

namespace MusicOre.Model
{
	public class Device
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual List<RootFolderPath> RootFolderPaths { get; set; }
	}

	public class MediaEntry
	{
		public string Extension { get; set; }

		public string Filename { get; set; }

		public int Id { get; set; }

		public string RelativeFolderPath { get; set; }

		public RootFolder RootFolder { get; set; }
	}

	public class RootFolder
	{
		public int Id { get; set; }

		public bool IsCloud { get; set; }

		public string Name { get; set; }

		public virtual List<RootFolderPath> PathsOnDevices { get; set; }

		public virtual List<MediaEntry> MediaEntries { get; set; }
	}

	public class RootFolderPath
	{
		public Device Device { get; set; }

		public int Id { get; set; }

		public string Path { get; set; }

		public RootFolder RootFolder { get; set; }
	}
}
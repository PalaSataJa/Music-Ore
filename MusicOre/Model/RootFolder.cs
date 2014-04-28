using System.Collections.Generic;

namespace MusicOre.Model
{
	public class RootFolder
	{
		public int Id { get; set; }

		public bool IsCloud { get; set; }

		public string Name { get; set; }

		public virtual List<RootFolderPath> PathsOnDevices { get; set; }

		public virtual List<MediaEntry> MediaEntries { get; set; }

	}
}
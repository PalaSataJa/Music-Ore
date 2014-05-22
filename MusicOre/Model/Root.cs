using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MusicOre.Model
{
	public class Root
	{
		public int Id { get; set; }

		public bool IsCloud { get; set; }

		public string Name { get; set; }

		public virtual ICollection<DevicePath> DevicePaths { get; set; }

		public virtual ICollection<MediaEntry> MediaEntries { get; set; }

	}
}
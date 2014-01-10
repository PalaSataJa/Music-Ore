using System.Collections.Generic;
using System.Data.Entity;
namespace MusicOre.Model
{
	public class Device
	{
		public int DeviceId { get; set; }

		public string Name { get; set; }

		public virtual List<RootFolder> RootFolders { get; set; }
	}

	public class RootFolder
	{
		public int RootFolderId { get; set; }

		public virtual RootFolder Devices { get; set; }

		public bool IsCloud { get; set; }

		public string Name { get; set; }

		public string Path { get; set; }
	}
}
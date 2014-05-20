using System;
using System.Collections.Generic;

namespace MusicOre.Model
{
	public class Device
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual List<DevicePath> DevicePaths { get; set; }
	}
}
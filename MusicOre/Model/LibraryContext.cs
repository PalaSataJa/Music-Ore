using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOre.Model
{
	public class LibraryContext : DbContext
	{
		public DbSet<Device> Devices { get; set; }
		public DbSet<MediaEntry> MediaEntries { get; set; }
		public DbSet<Root> Roots { get; set; }
		public DbSet<DevicePath> DevicePaths { get; set; }

		public static void CleanAllData()
		{
			using (var context = new LibraryContext())
			{
				context.MediaEntries.RemoveRange(context.MediaEntries);
				context.Roots.RemoveRange(context.Roots);
				context.DevicePaths.RemoveRange(context.DevicePaths);
				context.Devices.RemoveRange(context.Devices);
				context.SaveChanges();
			}
		}

	}
}

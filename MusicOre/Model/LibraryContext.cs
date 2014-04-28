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
		public DbSet<RootFolder> RootFolders { get; set; }
		public DbSet<RootFolderPath> RootFolderPaths { get; set; }

	}
}

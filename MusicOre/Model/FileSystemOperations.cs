using System;
using System.Collections.Generic;
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
			Device entity = new Device { Name = System.Environment.MachineName };
			context.Devices.Add(entity);
			context.SaveChanges();
			return entity;
		}
	}
}

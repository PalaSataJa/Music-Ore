using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicOre.Model;
using NUnit.Framework;

namespace MusicOre.Tests
{
	[TestFixture]
	class DLLTests
	{
		[Test]
		public void TestLayeredInset()
		{
			var currentDevice = LibraryOperations.InitCurrentDevice();
			var root = new Root{ Name = "bla"};
			var path = new DevicePath{ Path = "path"};

		}
	}
}

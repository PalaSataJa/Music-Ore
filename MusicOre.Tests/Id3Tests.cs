using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicOre.Tests.Properties;
using NUnit.Framework;
using TagLib;

namespace MusicOre.Tests
{
	[TestFixture]
    public class Id3Tests
    {

		[Test]
		public void TestExtract()
		{
			var fileName =
				@"..\..\Samples\01. 30 Seconds To Mars - Birth.mp3";
			TagLib.File tagFile = TagLib.File.Create(fileName);
			Assert.NotNull(tagFile);
			Assert.AreEqual(tagFile.Tag.Title,"Birth");
		}
    }
}

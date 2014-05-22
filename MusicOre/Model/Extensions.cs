using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MusicOre.Model
{
	public static class Extensions
	{
		public static MD5 HashGen = MD5.Create();

		public static void ComputeMd5(this MediaEntry mediaEntry, string filePath)
		{
			byte[] data = HashGen.ComputeHash(File.ReadAllBytes(filePath));

			var sBuilder = new StringBuilder();

			foreach (byte t in data)
			{
				sBuilder.Append(t.ToString("x2"));
			}

			mediaEntry.Md5 = sBuilder.ToString();
		}

		public static void ForceId3Update(this List<MediaEntry> queue)
		{
			//todo async
			using (var context = new LibraryContext())
			{
				foreach (var entry in queue)
				{

					var tag = TagLib.File.Create(entry.FullPath).Tag;
					entry.Artist = tag.FirstPerformer;
					entry.Album = tag.Album;
					entry.Title = tag.Title;
					entry.Genre = tag.FirstGenre;
					entry.BeatsPerMinute = tag.BeatsPerMinute.ToString();

				}
				context.SaveChanges();
			}

		}
	}
}
using Id3;
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

		public static void ForceId3Update(this Dictionary<string, MediaEntry> queue)
		{
			//todo async
			using (var context = new LibraryContext())
			{
				foreach (var entry in queue)
				{
					using (var mp3 = new Mp3File(entry.Key))
					{
						Id3Tag tag = mp3.GetTag(Id3TagFamily.FileStartTag);
						entry.Value.Artist = tag.Artists.Value;
						entry.Value.Album = tag.Album.Value;
						entry.Value.Title = tag.Title.Value;
						entry.Value.Genre = tag.Genre.Value;
						entry.Value.BeatsPerMinute = tag.BeatsPerMinute.Value;
					}
				}
				context.SaveChanges();
			}

		}
	}
}
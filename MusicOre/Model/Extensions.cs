using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MusicOre.Model
{
	public static class Extensions
	{
		public static MD5 HashGen = MD5.Create();

		public static bool CloseEnough(this DateTime thisTime, DateTime thatTime)
		{
			if (Math.Abs((thatTime - thisTime).TotalSeconds) < 1)
			{
				return true;
			}
			return false;
		}

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

					var tag = TagLib.File.Create(entry.FullPath);
					entry.Artist = tag.Tag.FirstPerformer;
					entry.Album = tag.Tag.Album;
					entry.Title = tag.Tag.Title;
					entry.Genre = tag.Tag.FirstGenre;
					entry.BeatsPerMinute = tag.Properties.AudioBitrate.ToString();
					entry.Duration = tag.Properties.Duration;

					context.MediaEntries.Attach(entry);
					context.Entry(entry).State = EntityState.Modified;
				}
				context.SaveChanges();
			}

		}
	}
}
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicOre.Model
{
	public class MediaEntry
	{
		public string Album { get; set; }

		public string Artist { get; set; }

		public string BeatsPerMinute { get; set; }

		public string Extension { get; set; }

		public string Filename { get; set; }

		public string Genre { get; set; }

		public int Id { get; set; }

		public DateTime LastUpdateDate { get; set; }

		[Index]
		public string Md5 { get; set; }

		public string RelativePath { get; set; }

		[NotMapped]
		public string FullPath { get; set; }

		public Root Root { get; set; }

		public int RootFolderId { get; set; }

		public string Title { get; set; }

		public bool Equals(MediaEntry other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Extension, other.Extension) && string.Equals(Filename, other.Filename) && string.Equals(RelativePath, other.RelativePath) && Equals(Root, other.Root);
		}

		public bool IsDuplicate(MediaEntry other)
		{
			if (other != null && other.Md5 == this.Md5)
				return true;
			return false;
		}

		public bool PossibleMovedEntry(MediaEntry obj)
		{
			var me = obj as MediaEntry;
			return this.Filename.Equals(me.Filename) && this.Extension.Equals(me.Extension) &&
						 this.RelativePath.Equals(me.RelativePath) && !this.Root.Equals(me.Root);
		}

		public void Update()
		{
			throw new NotImplementedException();
		}
	}
}
namespace MusicOre.Model
{
	public class MediaEntry
	{
		public bool Equals(MediaEntry other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Extension, other.Extension) && string.Equals(Filename, other.Filename) && string.Equals(RelativeFolderPath, other.RelativeFolderPath) && Equals(RootFolder, other.RootFolder);
		}
        
		public string Extension { get; set; }

		public string Filename { get; set; }

		public int Id { get; set; }

		public string RelativeFolderPath { get; set; }

		public RootFolder RootFolder { get; set; }
        
		public bool PossibleMovedEntry(MediaEntry obj)
		{
			var me = obj as MediaEntry;
			return this.Filename.Equals(me.Filename) && this.Extension.Equals(me.Extension) &&
			       this.RelativeFolderPath.Equals(me.RelativeFolderPath) && !this.RootFolder.Equals(me.RootFolder);
		}

		public void Update(MediaEntry newer)
		{
			//todo update tag info
		}
	}
}
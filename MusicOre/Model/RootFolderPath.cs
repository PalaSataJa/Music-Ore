namespace MusicOre.Model
{
	public class RootFolderPath
	{
		public Device Device { get; set; }

		public int Id { get; set; }

		public string Path { get; set; }

		public virtual RootFolder RootFolder { get; set; }
	}
}
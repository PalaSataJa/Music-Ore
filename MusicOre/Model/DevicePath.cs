namespace MusicOre.Model
{
	public class DevicePath
	{
		public virtual Device Device { get; set; }
		public int DeviceId { get; set; }

		public int Id { get; set; }

		public string Path { get; set; }

		public int RootId { get; set; }
		public virtual Root Root { get; set; }
	}
}
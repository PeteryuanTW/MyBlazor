namespace MyBlazor.Client.DataClass
{
	public class WoInfo
	{
		public string wo;
		public string machine;
		public int index;
		public StartTimeAndEndTime t;

		public WoInfo(string wo, string machine, int index, DateTime start, DateTime end)
		{
			this.wo = wo;
			this.machine = machine;
			this.index = index;
			t = new StartTimeAndEndTime(start, end);
		}
	}
}

namespace MyBlazor.Client.DataClass
{
	public class WoInfo
	{
		public string wo;
		public string machine;
		public StartTimeAndEndTime t;

		public WoInfo(string wo, string machine, int start, int end)
		{
			this.wo = wo;
			this.machine = machine;
			t = new StartTimeAndEndTime(start, end);
		}
	}
}

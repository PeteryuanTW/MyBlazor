namespace MyBlazor.Shared.DataClass
{
	public class WoInfo
	{
		public string wo;
		public string machine;
		public int index;
		public int generateType;
		public StartTimeAndEndTime t;

		public WoInfo(string wo, string machine, int index, int generateType, DateTime start, DateTime end)
		{
			this.wo = wo;
			this.machine = machine;
			this.index = index;
			this.generateType = generateType;
			t = new StartTimeAndEndTime(start, end);
		}
	}
}

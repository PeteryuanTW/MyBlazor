namespace MyBlazor.Shared.DataClass
{
	public class StartTimeAndEndTime
	{
		public DateTime startTime;
		public DateTime endTime;

		public StartTimeAndEndTime(DateTime start, DateTime end)
		{
			startTime = start;
			endTime = end;
		}
	}
}

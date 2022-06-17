namespace MyBlazor.Shared.DataClass
{
    public class WOAndTimes
    {
        public string wo;
        public int woStatus;
        public StartTimeAndEndTime startAndEndTime;

        public WOAndTimes(string wo,int woStatus, StartTimeAndEndTime startAndEndTime)
        {
            this.wo = wo;
            this.woStatus = woStatus;
            this.startAndEndTime = startAndEndTime;
        }
    }
}

namespace MyBlazor.Client.DataClass
{
    public class WOAndTimes
    {
        public string wo;
        public StartTimeAndEndTime startAndEndTime;

        public WOAndTimes(string wo, StartTimeAndEndTime startAndEndTime)
        {
            this.wo = wo;
            this.startAndEndTime = startAndEndTime;
        }
    }
}

namespace MyBlazor.Shared.DataClass
{
    public class WOJobInMachine
    {
        public string wo;
        public int woStatus;
        public StartTimeAndEndTime startAndEndTime;

        public WOJobInMachine(string wo,int woStatus, StartTimeAndEndTime startAndEndTime)
        {
            this.wo = wo;
            this.woStatus = woStatus;
            this.startAndEndTime = startAndEndTime;
        }
    }
}

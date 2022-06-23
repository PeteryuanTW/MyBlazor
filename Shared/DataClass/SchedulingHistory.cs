namespace MyBlazor.Shared.DataClass
{
    public class SchedulingHistory
    {
        public DateTime dataTimeID;
        public List<WoInfo> woInfos;
        public TimeSpan dueTime;

        public SchedulingHistory(DateTime dataTimeID, List<WoInfo> woInfos, TimeSpan dueTime)
        {
            this.dataTimeID = dataTimeID;
            this.woInfos = woInfos;
            this.dueTime = dueTime;
        }
        public SchedulingHistory()
        {}
    }
}

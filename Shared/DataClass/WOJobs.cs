namespace MyBlazor.Shared.DataClass
{
    public class WOJobs
    {
        public string wo;
        public List<TimeSpan> processCost;
        public DateTime dueTime;
        public WOJobs(string wo, List<TimeSpan> processCost, DateTime dueTime)
        {
            this.wo = wo;
            this.processCost = processCost;
            this.dueTime = dueTime;
        }

        public int GetJobsCount()
        {
            return processCost.Count;
        }
    }
}

namespace MyBlazor.Shared.DataClass
{
    public class SchedulingHistory
    {
        public DateTime dataTimeID;
        public List<List<Job>> jobs;
        public TimeSpan dueTime;
        public int jobsCount = 0;

        public SchedulingHistory(DateTime dataTimeID, List<List<Job>> jobs, TimeSpan dueTime)
        {
            this.dataTimeID = dataTimeID;
            this.jobs = jobs;
            this.dueTime = dueTime;
            SetJobsCount();
        }
        private void SetJobsCount()
        {
            foreach (List<Job> jobsList in jobs)
            {
                foreach (Job job in jobsList)
                {
                    if (job != null)
                    {
                        jobsCount++;
                    }
                }
            }
        }
    }
}

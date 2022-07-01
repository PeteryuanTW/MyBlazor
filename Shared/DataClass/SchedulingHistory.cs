﻿namespace MyBlazor.Shared.DataClass
{
    public class SchedulingHistory
    {
        public DateTime dataTimeID;
        public List<List<Job>> jobs;
        public TimeSpan dueTime;
        public Dictionary<(string, string), DateTime> machineNextAvailable;
        public Dictionary<string, DateTime> woNextAvailable;
        public int jobsCount = 0;

        public SchedulingHistory(DateTime dataTimeID, List<List<Job>> jobs, TimeSpan dueTime, Dictionary<(string, string), DateTime> machineNextAvailable, Dictionary<string, DateTime> woNextAvailable)
        {
            this.dataTimeID = dataTimeID;
            this.jobs = jobs;
            this.dueTime = dueTime;
            this.machineNextAvailable = machineNextAvailable;
            this.woNextAvailable = woNextAvailable;
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

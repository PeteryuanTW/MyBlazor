namespace MyBlazor.Shared.DataClass
{
    public class MachineWOJobs
    {
        public string machineName;
        public string index;
        public int machineStatus;
        public List<WOJobInMachine> woJobsInMachine = new List<WOJobInMachine>();

        public MachineWOJobs(string machineName, string index, int machineStatus, List<WOJobInMachine> woJobsInMachine)
        {
            this.machineName = machineName;
            this.index = index;
            this.machineStatus = machineStatus;
            this.woJobsInMachine = woJobsInMachine;
        }

        public void Sort()
        {
            woJobsInMachine = woJobsInMachine.OrderBy(x => x.startAndEndTime.startTime).ToList();
        }
    }
}

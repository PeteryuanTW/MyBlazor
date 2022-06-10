namespace MyBlazor.Client.DataClass
{
    public class MachineWOInfos
    {
        public string machineName;
        public int index;
        public int machineStatus;
        public List<WOAndTimes> woAndTimes = new List<WOAndTimes>();

        public MachineWOInfos(string machineName, int index, int machineStatus, List<WOAndTimes> woAndTimes)
        {
            this.machineName = machineName;
            this.index = index;
            this.machineStatus = machineStatus;
            this.woAndTimes = woAndTimes;
        }

        public void Sort()
        {
            woAndTimes = woAndTimes.OrderBy(x => x.startAndEndTime.startTime).ToList();
        }
    }
}

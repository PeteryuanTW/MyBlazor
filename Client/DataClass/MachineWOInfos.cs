namespace MyBlazor.Client.DataClass
{
    public class MachineWOInfos
    {
        public string machineName;
        public int machineStatus;
        public List<WOAndTimes> woAndTimes = new List<WOAndTimes>();

        public MachineWOInfos(string machineName, int machineStatus, List<WOAndTimes> woAndTimes)
        {
            this.machineName = machineName;
            this.machineStatus = machineStatus;
            this.woAndTimes = woAndTimes;
        }
    }
}

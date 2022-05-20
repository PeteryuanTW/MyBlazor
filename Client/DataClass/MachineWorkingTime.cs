namespace MyBlazor.Client.DataClass
{
    public class MachineWorkingTime
    {
        public string x { set; get; }
        public long[] y { set; get; }

        public MachineWorkingTime(string machineName, long[] timeInterval)
        {
            this.x = machineName;
            this.y = timeInterval;
        }
    }
}

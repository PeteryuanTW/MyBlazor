namespace MyBlazor.Client.DataClass
{
    public class WoUnit
    {
        public String name { set; get; }
        public MachineWorkingTime[] data { set; get; }

        public WoUnit(String woName, MachineWorkingTime[] machineAndTimeIntervals)
        {
            this.name = woName;
            this.data = machineAndTimeIntervals;
        }
    }
}

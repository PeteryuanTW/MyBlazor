namespace MyBlazor.Shared.DataClass
{
    public class MachineData
    {
        public string machineName;
        public int index;
        public int machineStatus;

        public MachineData(string machineName, int index, int machineStatus)
        {
            this.machineName = machineName;
            this.index = index;
            this.machineStatus = machineStatus;
        }
    }
}

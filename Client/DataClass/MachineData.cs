namespace MyBlazor.Client.DataClass
{
    public class MachineData
    {
        public string machineName;
        public int machineStatus;

        public MachineData(string machineName, int machineStatus)
        {
            this.machineName = machineName;
            this.machineStatus = machineStatus;
        }
    }
}

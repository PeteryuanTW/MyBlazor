namespace MyBlazor.Shared.DataClass
{
    public class MachineData
    {
        public string machineName;
        public string index;
        public int machineStatus;
        public DateTime nextAvailableTime;

        public MachineData(string machineName, string index, int machineStatus)
        {
            this.machineName = machineName;
            this.index = index;
            this.machineStatus = machineStatus;
            nextAvailableTime = DateTime.Now;
        }

        public void SetAvaiable(DateTime dt)
        {
            if (dt > nextAvailableTime)
            {
                nextAvailableTime = dt;
            }
        }
    }
}

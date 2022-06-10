namespace MyBlazor.Client.DataClass
{
    public class WORequirements
    {
        public string wo;
        public List<TimeSpan> processCost;
        public DateTime dueTime;
        public WORequirements(string wo, List<TimeSpan> processCost, DateTime dueTime)
        {
            this.wo = wo;
            this.processCost = processCost;
            this.dueTime = dueTime;
        }
    }
}

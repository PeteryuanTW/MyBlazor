namespace MyBlazor.Shared.DataClass
{
    public class Job
    {
        public string machine;
        public string index;
        public string wo;
        public int generateType;
        public DateTime start;
        public TimeSpan duration;
        public DateTime end;

        public Job(string wo, int generateType, TimeSpan duration)
        {
            this.wo = wo;
            this.generateType = generateType;
            this.duration = duration;
            this.machine = "";
            this.index = "";
            start = DateTime.MinValue;
            end = DateTime.MinValue;
        }

        public void SetStartTime(DateTime start)
        {
            this.start = start;
            end = start + duration;
        }
    }
}

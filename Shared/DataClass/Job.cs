using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazor.Shared.DataClass
{
    public class Job
    {
        public string machine;
        public string index;
        public string wo;
        public DateTime start;
        public TimeSpan duration;
        public DateTime end;

        public Job(string wo, TimeSpan duration)
        {
            this.wo = wo;
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

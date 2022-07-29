using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazor.Shared.DataClass
{
    public class StationInfo
    {
        string factory;
        string line;
        string process;
        string stationType;
        string stationName;
        public List<string> stations = new();
        public StationInfo(string factory, string line, string process, string stationType, string stationName)
        {
            this.factory = factory;
            this.line = line;
            this.process = process;
            this.stationType = stationType;
            this.stationName = stationName;
            ResetList();
        }
        private void ResetList()
        {
            stations = new();
            stations.Add(factory);
            stations.Add(line);
            stations.Add(process);
            stations.Add(stationType);
            stations.Add(stationName);
        }
    }
}

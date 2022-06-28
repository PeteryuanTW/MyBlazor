using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazor.Shared.DataClass
{
    public class MachineTypeList
    {
        public string machineType;
        public List<string> machineIndexList;

        public MachineTypeList(string machineType)
        {
            this.machineType = machineType;
            machineIndexList = new();
        }

    }
}

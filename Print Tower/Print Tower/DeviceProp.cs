using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Print_Tower
{
    public class DeviceProp
    {
        public string PropName { get; private set; }
        public string PropOID { get; private set; }
        public string PropValue { get; set; }

        public DeviceProp(string propName, string propOID)
        {
            this.PropName = propName;
            this.PropOID = propOID;
            this.PropValue = "No information";
        }
    }
}

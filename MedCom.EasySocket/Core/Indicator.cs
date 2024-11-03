using MedCom.EasySocket.HL7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.Core
{
    public  class Indicator
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Unit { get; set; }

        public Hl7ValueType ValueType { get; set; }

        public bool IsQC { get; set; }
    }
}

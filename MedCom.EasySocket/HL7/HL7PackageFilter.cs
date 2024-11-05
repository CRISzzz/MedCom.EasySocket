using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedCom.EasySocket.SocketCom;

namespace MedCom.EasySocket.HL7
{
    public class HL7PackageFilter : PkgFilter
    {

        private const string HL7StartFilter = "0x0B";

        private const string HL7EndFilter = "0x1C 0x0D";

        public HL7PackageFilter() : base(HL7StartFilter, HL7EndFilter)
        {

        }

    }
}

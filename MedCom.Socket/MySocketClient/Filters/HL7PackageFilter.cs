using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.MySocketClient.Filters
{
    public class HL7PackageFilter : PkgFilter
    {

        private const string HL7StartFilter = "0x0B";

        private const string HL7EndFilter = "0x1C 0x0D";

        public HL7PackageFilter():base(HL7StartFilter, HL7EndFilter)
        {
            
        }

        public override string ExtractPayload(string message)
        {
            try
            {
                message = message.Remove(message.Length - HL7EndFilter.Split(' ').Length, HL7EndFilter.Split(' ').Length);
                message = message.Remove(0, HL7StartFilter.Split(' ').Length);
                return message;
            }
            catch
            {
                return null;
            }
        }
    }
}

using MedCom.EasySocket.Core;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Base.Util;
using NHapi.Model.V23.Message;
using NHapi.Model.V23.Segment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.HL7.HandlersV23
{
    public class ACK_R01_HL7Handler<T> : IMsgAnswer<PatientReport>
    {
        public Result<PatientReport> Parse(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("Invalid HL7 message");
            PipeParser parser = new PipeParser();
            IMessage hl7Message = parser.Parse(message);
            Terser terser = new Terser(hl7Message);
            string revStatus = terser.Get("/.MSA-1");
            var result = new Result<PatientReport>
            {
                Value = new PatientReport
                {
                    SampleNo = terser.Get("/.MSA-3")
                },
            };
            if (!string.IsNullOrEmpty(revStatus) && revStatus == "AA")
                result.Success = true;
            else result.Success = false;
            return result;
        }
    }
}

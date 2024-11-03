using MedCom.EasySocket.Core;
using MedCom.EasySocket.HL7.HandlersV23;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.HL7
{
    public class Protohl7 : IProtohl7
    {
        public void FetchPatientData()
        {

        }

        public bool SendReport(Func<PatientReport> report)
        {
            PatientReport patientReport = report();
            MsgSender sender = new ORU_R01_HL7PkgHandler();
            string msg = sender.CreateMessage(patientReport);

            //todo
            return true;
        }

        public bool SendReportQc(Func<PatientReport> report)
        {
            return SendReport(report);
        }

        public bool UpdateReport(Func<PatientReport> report)
        {
            throw new NotImplementedException();
        }
    }
}

using MedCom.EasySocket.Core;
using MedCom.EasySocket.HL7.HandlersV23;
using MedCom.EasySocket.SocketCom;
using MedCom.EasySocket.SocketCom.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.HL7
{
    public class Protohl7 : IProtohl7
    {
        private ISocketClient _socketClient;

        public Protohl7(ISocketClient client)
        {
            _socketClient = client;
            _socketClient.Start();
        }

        public void FetchPatientData()
        {

        }

        public bool SendReport(Func<PatientReport> report)
        {
            PatientReport patientReport = report();
            IMsgSender pkgSender = new ORU_R01_HL7PkgHandler();

            string msg = pkgSender.CreateMessage(patientReport);
            _socketClient.Send(msg);

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

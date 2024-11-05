using MedCom.EasySocket.Core;
using MedCom.EasySocket.HL7.HandlersV23;
using MedCom.EasySocket.SocketCom;
using MedCom.EasySocket.SocketCom.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.HL7
{
    public class Protohl7 : IProtohl7
    {
        private ISocketClient _socketClient;
        private IObservableQueue<Package> _queue;

        public Protohl7(ISocketClient client, IObservableQueue<Package> queue)
        {
            _socketClient = client;
            _queue = queue;
            _socketClient.Start();
        }

        public void FetchPatientData()
        {

        }

        public bool SendReport(Func<PatientReport> getReport)
        {
            PatientReport report = getReport();
            IMsgSender messageSender = new ORU_R01_HL7PkgHandler();
            string hl7Message = messageSender.CreateMessage(report);

            _socketClient.Send(hl7Message);

            Package responsePackage;
            if (!_queue.TryDequeue(out responsePackage) || responsePackage == null)
            {
                return false;
            }

            var ackHandler = new ACK_R01_HL7Handler();
            var parsedResponse = ackHandler.Parse(responsePackage.payloadString);

            return parsedResponse.Success;
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

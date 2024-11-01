using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.Socket.Core
{
    public interface IProtocal
    {
        void FetchPatientData();

        void SendReport();

        void SendReportQc();

        void UpdateReport();
    }
}

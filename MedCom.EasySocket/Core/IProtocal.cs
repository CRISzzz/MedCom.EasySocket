using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.Core
{
    public interface IProtocal
    {
        void FetchPatientData();

        bool SendReport(Func<PatientReport> report);

        bool SendReportQc(Func<PatientReport> report);

        bool UpdateReport(Func<PatientReport> report);
    }
}

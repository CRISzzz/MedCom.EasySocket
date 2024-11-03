using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.Core
{
    public abstract class MsgSender
    {
        public abstract string CreateMessage(PatientReport report);
    }
}

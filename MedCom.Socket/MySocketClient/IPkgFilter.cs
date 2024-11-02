using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.MySocketClient
{
    public interface IPkgFilter
    {
        string ExtractPayload(string message);
    }
}

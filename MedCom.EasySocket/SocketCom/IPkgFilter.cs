using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.SocketCom
{
    public interface IPkgFilter
    {

        void ResolveBytes(byte[] bytes);
    }
}

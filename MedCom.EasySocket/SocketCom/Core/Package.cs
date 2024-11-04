using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.SocketCom.Core
{
    public class Package
    {

        public byte Fore { get; set; }

        public byte[] Payload { get; set; }
        public byte Tail { get; set; }
    }
}

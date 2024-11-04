using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.SocketCom.Core
{
    public class ForeBuffer
    {
        public bool IsConcluedTail { get; set; }

        public byte[] Bytes { get; set; }
    }
}

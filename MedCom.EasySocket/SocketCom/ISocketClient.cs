using MedCom.EasySocket.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.SocketCom
{
    public interface ISocketClient
    {
        void Start();
        Task<bool> Send(string message);
        void Close();

    }
}

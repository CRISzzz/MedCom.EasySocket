using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.SocketClient
{
    public interface ISocketClient
    {
        Task Connect();
        Task<bool> Send(string message);
        Task ReceiveMessage();
        void Close();

    }
}

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
        //event Func<string, Result<T>>? OnRecv;
        Task Connect();
        Task<bool> Send(string message);
        Task ReceiveMessage();
        void Close();

    }
}

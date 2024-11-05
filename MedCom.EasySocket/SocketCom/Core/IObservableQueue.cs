using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.SocketCom.Core
{
    public interface IObservableQueue<T>
    {
        event EventHandler<T> ItemEnqueued;

        void Enqueue(T item);

        bool TryDequeue(out T item);

        int Count { get; }
    }
}

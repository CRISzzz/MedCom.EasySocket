using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.SocketCom.Core
{
    public class ObservableQueue<T>:IObservableQueue<T>
    {
        private ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();

        public event EventHandler<T> ItemEnqueued;

        public void Enqueue(T item)
        {
            _queue.Enqueue(item);
            OnItemEnqueued(item); 
        }

        protected virtual void OnItemEnqueued(T item)
        {
            ItemEnqueued?.Invoke(this, item);  
        }

        public bool TryDequeue(out T result)
        {
            return _queue.TryDequeue(out result);
        }

        public int Count => _queue.Count;
    }
}

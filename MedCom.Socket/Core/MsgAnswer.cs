using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.Core
{
    public abstract class MsgAnswer<T> where T : class
    {
        public abstract Result<T> Parse(string message);
    }
}

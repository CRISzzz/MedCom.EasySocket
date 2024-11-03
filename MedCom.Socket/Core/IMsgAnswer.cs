using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.Core
{
    public interface IMsgAnswer<T> where T : class
    {
        Result<T> Parse(string message);
    }
}

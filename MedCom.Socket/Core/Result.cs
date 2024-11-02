using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.Core
{
    public class Result<T>
    {
        public T Value { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; } 
    }
}

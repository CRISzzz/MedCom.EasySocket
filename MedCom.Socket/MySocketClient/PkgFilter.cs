using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.MySocketClient
{
    public abstract class PkgFilter : IPkgFilter
    {
        private byte[] byteStart;
        private byte[] byteEnd;

        public PkgFilter(string start, string end)
        {
            byteStart = GetBytesFromString(start);
            byteEnd = GetBytesFromString(end);
        }
        public abstract string ExtractPayload(string message);

        public  byte[] GetBytesFromString(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return new byte[0];
            string[] stringSpilt = hex.Split(' ');
            byte[] byteArray = new byte[stringSpilt.Length];
            for (int i = 0; i < stringSpilt.Length; i++)
            {
                var num = Convert.ToInt32(stringSpilt[i], 16);
                byteArray[i] = (byte)num;
            }
            return byteArray;
        }
    }
}

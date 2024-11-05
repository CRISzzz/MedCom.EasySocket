using MedCom.EasySocket.SocketCom.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.SocketCom
{
    public abstract class PkgFilter : IPkgFilter
    {
        private byte[] byteStart;
        private byte[] byteEnd;

        public ConcurrentQueue<Package> packagesMQ = new ConcurrentQueue<Package>();
        private byte[] lastBackBuffer;

        public PkgFilter(string start, string end)
        {
            byteStart = GetBytesFromString(start);
            byteEnd = GetBytesFromString(end);
        }
        public abstract string ExtractPayload(string message);

        public byte[] GetBytesFromString(string hex)
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

        public void ResolveBytes(byte[] bytes)
        {
            var buff = new BufferStructure(bytes, byteStart, byteEnd);

            if (lastBackBuffer?.Any() == true)
            {
                packagesMQ.Enqueue(new Package
                {
                    Payload = lastBackBuffer.Concat(buff.ForeBuffer.Bytes).ToArray()
                });
            }

            if (buff.Packages?.Any() == true)
            {
                foreach (var pkg in buff.Packages)
                {
                    packagesMQ.Enqueue(pkg);
                }
            }

            if (buff.BackBuffer.Bytes?.Any() == true)
            {
                lastBackBuffer = new byte[buff.BackBuffer.Bytes.Length];
                buff.BackBuffer.Bytes.CopyTo(lastBackBuffer, 0);
            }
        }
    }
}

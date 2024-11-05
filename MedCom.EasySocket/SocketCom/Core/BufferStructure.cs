using MedCom.EasySocket.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.SocketCom.Core
{
    public class BufferStructure
    {
        private byte[] _bytes;
        private byte[] _head;
        private byte[] _tail;

        public BufferStructure(byte[] bytes, byte[] head, byte[] tail)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes), "Byte array cannot be null.");
            _head = head;
            _tail = tail;
            _bytes = bytes;
        }

        private Dictionary<int, ByteType> _headTailCache;

        private Dictionary<int, ByteType> headTailDic
        {
            get
            {
                if (_headTailCache == null)
                {
                    _headTailCache = SubArrayMatcher.FindSubArrays(_bytes, _head, _tail);
                }
                return _headTailCache;
            }
        }

        public ForeBuffer ForeBuffer
        {
            get
            {
                var firstTailPair = headTailDic.FirstOrDefault();

                if (!EqualityComparer<KeyValuePair<int, ByteType>>.Default.Equals(firstTailPair, default))
                {
                    byte[] foreBytes = new byte[firstTailPair.Key + 1];
                    Array.Copy(_bytes, foreBytes, firstTailPair.Key + 1);
                    return new ForeBuffer
                    {
                        Bytes = foreBytes
                    };
                }

                return new ForeBuffer
                {
                    Bytes = Array.Empty<byte>()
                };
            }
        }

        public List<Package> Packages
        {
            get
            {
                var packages = new List<Package>();

                if (headTailDic.Count == 0)
                {
                    return packages;
                }

                var temphtDic = new Dictionary<int, ByteType>(headTailDic);

                if (temphtDic.First().Value == ByteType.EndPst)
                {
                    temphtDic.Remove(temphtDic.First().Key);
                }
                if (temphtDic.Last().Value == ByteType.StartPst)
                {
                    temphtDic.Remove(temphtDic.Last().Key);
                }

                if (temphtDic.Count <= 1)
                {
                    return packages;
                }

                var pkgIdxLst = new List<(int, int)>();
                var kvLst = temphtDic.ToList();

                for (int i = 0; i < kvLst.Count - 1; i++)
                {
                    if (kvLst[i].Value == ByteType.StartPst && kvLst[i + 1].Value == ByteType.EndPst)
                    {
                        pkgIdxLst.Add((kvLst[i].Key, kvLst[i + 1].Key));
                    }
                }

                foreach (var pkg in pkgIdxLst)
                {
                    byte[] payload = new byte[pkg.Item2 - pkg.Item1];
                    Array.Copy(_bytes, pkg.Item1, payload, 0, payload.Length);
                    packages.Add(new Package
                    {
                        Fore = _head,
                        Payload = payload,
                        Tail = _tail,
                    });
                }

                return packages;

            }
        }

        public BackBuffer BackBuffer
        {
            get
            {
                var lastHeadPair = headTailDic.LastOrDefault(x => x.Value == ByteType.StartPst);

                if (!EqualityComparer<KeyValuePair<int, ByteType>>.Default.Equals(lastHeadPair, default))
                {
                    byte[] foreBytes = new byte[_bytes.Length - lastHeadPair.Key];
                    Array.Copy(_bytes, lastHeadPair.Key, foreBytes, 0, foreBytes.Length);

                    return new BackBuffer
                    {
                        Bytes = foreBytes
                    };
                }

                return new BackBuffer
                {
                    Bytes = Array.Empty<byte>()
                };
            }
        }
    }
}

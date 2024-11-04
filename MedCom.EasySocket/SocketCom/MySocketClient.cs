using MedCom.EasySocket.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.SocketCom
{
    public class MySocketClient : ISocketClient
    {

        private readonly IPAddress _ipAddr;
        private readonly int _port;
        private Socket? _mySocket;
        private IPkgFilter _filter;
        private CancellationTokenSource cts;

        public static ConcurrentQueue<byte[]> _messageQueue = new ConcurrentQueue<byte[]>();

        public MySocketClient(IPAddress ipAddr, int port, IPkgFilter filter)
        {
            _ipAddr = ipAddr;
            _port = port;
            _filter = filter;
            cts = new CancellationTokenSource();
        }

        public void Start()
        {
            Task.Run(() => OpenAndKeepConnected());
            Task.Run(() => ReceiveMessage());
        }

        #region basic socket method
        public void Init()
        {
            _mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _mySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
        }

        public async Task Connect()
        {
            try
            {
                await _mySocket.ConnectAsync(_ipAddr, _port);
            }
            catch (Exception)
            {

                _mySocket = null;
            }

        }

        public async Task<bool> Send(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                int sendBytes = await _mySocket.SendAsync(data);
                if (sendBytes > 0)
                    return await Task.FromResult(true);
                else return false;
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }

        public void Close()
        {
            cts.Cancel();
            if (_mySocket != null)
            {
                _mySocket.Close();
                _mySocket = null;
            }
        }

        public bool IsConnected()
        {
            try
            {
                return !(_mySocket.Poll(1, SelectMode.SelectRead) && _mySocket.Available == 0);
            }
            catch (Exception)
            {

                return false;
            }

        }
        #endregion


        #region async Task
        public async Task ReceiveMessage()
        {
            try
            {
                var buffer = new byte[1024];
                while (!cts.IsCancellationRequested)
                {
                    if (_mySocket.Connected)
                    {
                        int receivedBytes = await _mySocket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                        if (receivedBytes > 0)
                        {
                            byte[] receivedData = new byte[receivedBytes];
                            Array.Copy(buffer, receivedData, receivedBytes);
                            _messageQueue.Enqueue(receivedData);
                        }
                    }
                    //string message = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                }
                //string payload = _filter.ExtractPayload(message);
                //OnRecv?.Invoke(payload);
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Socket error while receiving: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while receiving: {ex.Message}");
            }
        }

        public async Task OpenAndKeepConnected()
        {
            while (!cts.IsCancellationRequested)
            {
                if (_mySocket == null)
                {
                    Init();
                    await Connect();
                }

                if (!IsConnected())
                    await Connect();

                await Task.Delay(1000);
            }
        }
        #endregion
    }
}

﻿using MedCom.EasySocket.SocketClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.MySocketClient
{
    public class MySocketClient<TPackageInfo> : ISocketClient, IDisposable
    {

        private readonly IPAddress _ipAddr;
        private readonly int _port;
        private Socket _mySocket;
        public MySocketClient(IPAddress ipAddr, int port)
        {
            _ipAddr = ipAddr;
            _port = port;
            _mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public async Task Connect()
        {
            if (_mySocket != null)
                _mySocket.Close();

            await _mySocket.ConnectAsync(_ipAddr, _port);
            Console.WriteLine($"MedCom connected the remote server{_ipAddr.ToString()}:{_port}");
        }


        public async Task<bool> Send(string message)
        {
            try
            {
                if (_mySocket == null || !_mySocket.Connected)
                    await Connect();

                byte[] data = Encoding.UTF8.GetBytes(message);
                int sendBytes = await _mySocket.SendAsync(data);

                Console.WriteLine($"Sent: {message}");
                if (sendBytes > 0)
                    return await Task.FromResult(true);
                else return false;

            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Socket error while connecting: {ex.Message}");
                return await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while connecting: {ex.Message}");
                return await Task.FromResult(false);
            }

        }

        public async Task ReceiveMessage()
        {
            await ReceiveMessage(1024);
        }


        private async Task ReceiveMessage(int bufferSize = 1024)
        {
            try
            {
                var buffer = new byte[bufferSize];

                int receivedBytes = await _mySocket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

                string message = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

                IPkgFilter filter = null;
                Console.WriteLine($"Received: {message}");
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

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            if (_mySocket != null)
                _mySocket.Close();
        }
    }
}

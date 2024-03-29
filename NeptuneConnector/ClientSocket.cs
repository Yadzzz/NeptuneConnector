﻿using NeptuneConnector.Communication;
using NeptuneConnector.Communication.Incoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneConnector
{
    public class ClientSocket
    {
        private Socket _socket { get; set; }
        private byte[] _data { get; set; }
        public bool Authenticated { get; set; }

        private CommunicationManager _communicationManager { get; set; }

        public ClientSocket(string ip, int port)
        {
            this._communicationManager = new CommunicationManager();
            this._data = new byte[1024];
            this.Connect(ip, port);
            this.Recieve();
        }

        private void Connect(string ip, int port)
        {
            try
            {
                this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this._socket.Connect(ip, port);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        private void Recieve()
        {
            try
            {
                this._socket.BeginReceive(this._data, 0, this._data.Length, SocketFlags.None, this.Recieved, this._socket);
            }
            catch (Exception e)
            {
                this.Disconnect();
                throw;
            }
        }

        private void Recieved(IAsyncResult result)
        {
            try
            {
                int bytesRecieved = this._socket.EndReceive(result);

                if (bytesRecieved > 0)
                {
                    byte[] newBytes = new byte[bytesRecieved];
                    Array.Copy(this._data, newBytes, bytesRecieved);
                    int header = BitConverter.ToInt32(newBytes, 0);
                    ClientPacket clientPacket = new ClientPacket(newBytes, header);

                    Console.WriteLine("Packet ID [" + header + "] Recieved!");

                    if (!this.Authenticated && header != IncomingPacketHeaders.AuthenticationComplete && header != IncomingPacketHeaders.AuthenticationDenied)
                    {
                        throw (new AuthenticationException("Authenticaion Required"));
                    }
                    else
                    {
                        if (this._communicationManager.GetPacket(header, out IPacket packet))
                        {
                            packet.ExecutePacket(this, clientPacket);
                            Console.WriteLine("Packet ID [" + header + "] Executed!");
                        }
                        else
                        {
                            Console.WriteLine("Packet ID [" + header + "] Not Found!");
                        }
                    }

                    this.Recieve();
                }
            }
            catch (Exception e)
            {
                this.Disconnect();
                throw;
            }
        }

        public void Send(byte[] data)
        {
            try
            {
                this._socket.Send(data, 0, data.Length, SocketFlags.None);
            }
            catch (Exception e)
            {
                this.Disconnect();
                throw;
            }
        }

        public bool IsSocketConnected
        {
            get
            {
                return this._socket != null && this._socket.Connected;
            }
        }

    public void Disconnect()
    {
        try
        {
            this._socket.Shutdown(SocketShutdown.Both);
            this._socket.Disconnect(false);
            this._socket.Dispose();
        }
        catch
        {

        }
    }
}
}

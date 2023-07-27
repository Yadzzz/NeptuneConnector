using NeptuneConnector.Communication.Outgoing.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneConnector
{
    public class Connector : IDisposable
    {
        private string _ip;
        private int _port;
        private bool _autoConnect;
        private bool _keepConnectionAlive;
        private string _accountSid;
        private string _authToken;
        private string _organizationSid;
        private string _organizationAuthToken;
        private string _applicationIdentifier;

        private ClientSocket _clientSocket { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip">Server IP Adress</param>
        /// <param name="port">Server Port</param>
        /// <param name="autoConnect">Auto Connect to Server</param>
        /// <param name="keepConnectionAlive">Keeps Server Connection Alive</param>
        /// <param name="accountSid">Account SID</param>
        /// <param name="authToken">Account Auth Token</param>
        /// <param name="organizationSid">Organization SID</param>
        /// <param name="organizationAuthToken">Organization Auth Token</param>
        /// <param name="applicationIdentifier">Application Id</param>
        public Connector(
            ConnectorConfiguration configuration,
            string accountSid,
            string authToken,
            string organizationSid,
            string organizationAuthToken,
            string applicationIdentifier)
        {
            this._ip = configuration.Ip;
            this._port = configuration.Port;
            this._autoConnect = configuration.AutoConnect;
            this._keepConnectionAlive = configuration.KeepConnectionAlive;
            this._accountSid = accountSid;
            this._authToken = authToken;
            this._organizationSid = organizationSid;
            this._organizationAuthToken = organizationAuthToken;
            this._applicationIdentifier = applicationIdentifier;

            if (configuration.AutoConnect)
            {
                this.Connect();
            }
        }

        /// <summary>
        /// Connects to the server by given authentications
        /// </summary>
        public void Connect()
        {
            if (this._clientSocket != null)
            {
                this._clientSocket.Disconnect();
            }

            this._clientSocket = new ClientSocket(this._ip, this._port);

            AuthenticationRequestComposer authRequest = new AuthenticationRequestComposer(this._accountSid, this._authToken, this._organizationSid, this._organizationAuthToken, this._applicationIdentifier);
            this._clientSocket.Send(authRequest.Finalize());
        }

        /// <summary>
        /// Logs Data
        /// </summary>
        /// <param name="logType">Log Type</param>
        /// <param name="logText">Log Text</param>
        public void LogApplication(LogType logType, string logText)
        {
            if (this._clientSocket == null || !this._clientSocket.IsSocketConnected)
            {
                this.Connect();
            }

            if (_clientSocket == null)
            {
                Console.WriteLine("Client Socket NULL");
                return;
            }

            ApplicationLogComposer applicationLogComposer = new ApplicationLogComposer(logType.ToString(), logText, DateTime.Now.ToString());
            this._clientSocket.Send(applicationLogComposer.Finalize());

            if(!this._keepConnectionAlive)
            {
                _clientSocket.Disconnect();
            }
        }

        /// <summary>
        /// Disconnects the Client Socket
        /// </summary>
        public void Disconnect()
        {
            _clientSocket.Disconnect();
        }

        public void Dispose()
        {
            _clientSocket.Disconnect();
        }
    }
}

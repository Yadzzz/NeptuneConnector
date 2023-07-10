using NeptuneConnector.Communication.Outgoing.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneConnector
{
    public class Connector
    {
        private string _ip;
        private int _port;
        private string _accountSid;
        private string _authToken;
        private int _applicationId;

        private ClientSocket _clientSocket { get; set; }

        public Connector(string ip, int port, string accountSid, string authToken, int applicationId)
        {
            this._ip = ip;
            this._port = port;
            this._accountSid = accountSid;
            this._authToken = authToken;
            this._applicationId = applicationId;
        }

        public void Connect()
        {
            if(this._clientSocket != null)
            {
                this._clientSocket.Disconnect();
            }

            this._clientSocket = new ClientSocket(this._ip, this._port);

            AuthenticationRequestComposer authRequest = new AuthenticationRequestComposer(this._accountSid, this._authToken, this._applicationId);
            this._clientSocket.Send(authRequest.Finalize());
        }

        public void LogApplication(string logType, string logText, string logDate)
        {
            if (this._clientSocket == null)
            {
                Console.WriteLine("ClientSocket NULL");
                return;
            }

            ApplicationLogComposer applicationLogComposer = new ApplicationLogComposer(logType, logText, logDate);
            this._clientSocket.Send(applicationLogComposer.Finalize());
        }
    }
}

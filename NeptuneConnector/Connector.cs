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
        private string _organizationSid;
        private string _organizationAuthToken;
        private int _applicationId;

        private ClientSocket _clientSocket { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip">Server IP Adress</param>
        /// <param name="port">Server Port</param>
        /// <param name="accountSid">Account SID</param>
        /// <param name="authToken">Account Auth Token</param>
        /// <param name="organizationSid">Organization SID</param>
        /// <param name="organizationAuthToken">Organization Auth Token</param>
        /// <param name="applicationId">Application Id</param>
        public Connector(string ip, int port, string accountSid, string authToken, string organizationSid, string organizationAuthToken, int applicationId)
        {
            this._ip = ip;
            this._port = port;
            this._accountSid = accountSid;
            this._authToken = authToken;
            this._organizationSid = organizationSid;
            this._organizationAuthToken = organizationAuthToken;
            this._applicationId = applicationId;
        }

        /// <summary>
        /// Connects to the server by given authentications
        /// </summary>
        public void Connect()
        {
            if(this._clientSocket != null)
            {
                this._clientSocket.Disconnect();
            }

            this._clientSocket = new ClientSocket(this._ip, this._port);

            AuthenticationRequestComposer authRequest = new AuthenticationRequestComposer(this._accountSid, this._authToken, this._organizationSid, this._organizationAuthToken, this._applicationId);
            this._clientSocket.Send(authRequest.Finalize());
        }

        /// <summary>
        /// Logs Data
        /// </summary>
        /// <param name="logType">Log Type</param>
        /// <param name="logText">Log Text</param>
        public void LogApplication(LogType logType, string logText)
        {
            if (this._clientSocket == null)
            {
                Console.WriteLine("ClientSocket NULL");
                return;
            }

            ApplicationLogComposer applicationLogComposer = new ApplicationLogComposer(logType.ToString(), logText, DateTime.Now.ToString());
            this._clientSocket.Send(applicationLogComposer.Finalize());
        }
    }
}

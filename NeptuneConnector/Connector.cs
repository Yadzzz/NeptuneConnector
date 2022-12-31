using NeptuneConnector.Communication.Outgoing.Packets.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneConnector
{
    public class Connector
    {
        private ClientSocket _clientSocket { get; set; }

        public Connector(string ip, int port)
        {
            this._clientSocket= new ClientSocket(ip, port);
        }

        public void LogError(string errorText)
        {
            ApplicationLogComposer logComposer = new ApplicationLogComposer(errorText);
            this._clientSocket.Send(logComposer.Finalize());
        }
    }
}

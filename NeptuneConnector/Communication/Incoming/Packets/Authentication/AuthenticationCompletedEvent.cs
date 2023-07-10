using NeptuneConnector;
using NeptuneConnector.Communication.Outgoing.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneConnector.Communication.Incoming.Packets
{
    public class AuthenticationCompletedEvent : IPacket
    {
        public void ExecutePacket(ClientSocket clientSocket, ClientPacket clientPacket)
        {
            clientSocket.Authenticated = true;
        }
    }
}

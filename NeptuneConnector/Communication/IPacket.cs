using NeptuneConnector;
using NeptuneServer.Communication.Incoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneServer.Communication
{
    public interface IPacket
    {
        public void ExecutePacket(ClientSocket clientSocket, ClientPacket packet);
    }
}

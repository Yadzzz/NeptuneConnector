﻿using NeptuneConnector;
using NeptuneConnector.Communication.Outgoing.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneConnector.Communication.Incoming.Packets
{
    public class AuthenticationDeniedEvent : IPacket
    {
        public void ExecutePacket(ClientSocket clientSocket, ClientPacket clientPacket)
        {
            string error = clientPacket.ReadString();
            throw (new AuthenticationException("[Authentication Denied] " + error));
        }
    }
}

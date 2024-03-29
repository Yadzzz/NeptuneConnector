﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeptuneConnector.Communication.Outgoing;
using NeptuneConnector.Communication.Incoming;
using NeptuneConnector.Communication.Incoming.Packets;

namespace NeptuneConnector.Communication
{
    public class CommunicationManager
    {
        public Dictionary<int, IPacket> Packets;

        public CommunicationManager()
        {
            this.Packets = new Dictionary<int, IPacket>();
            this.LoadPackets();
        }

        public bool GetPacket(int header, out IPacket packet)
        {
            return this.Packets.TryGetValue(header, out packet);
        }

        public void LoadPackets()
        {
            this.Packets.Add(IncomingPacketHeaders.AuthenticationComplete, new AuthenticationCompletedEvent());
            this.Packets.Add(IncomingPacketHeaders.AuthenticationDenied, new AuthenticationDeniedEvent());
        }
    }
}

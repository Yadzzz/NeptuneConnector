using NeptuneServer.Communication.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneConnector.Communication.Outgoing.Packets.Application
{
    public class ApplicationLogComposer : ServerPacket
    {
        public ApplicationLogComposer(string errorText) : base(OutgoingPacketHeaders.ApplicationLogComposer)
        {
            base.WriteString(errorText);
        }
    }
}

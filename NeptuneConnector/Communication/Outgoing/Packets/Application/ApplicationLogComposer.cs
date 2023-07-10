using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneConnector.Communication.Outgoing.Packets
{
    public class ApplicationLogComposer : ServerPacket
    {
        public ApplicationLogComposer(string logType, string logText, string logDate) : base(OutgoingPacketHeaders.ApplicationLogEvent)
        {
            base.WriteString(logType);
            base.WriteString(logText);
            base.WriteString(logDate);
        }
    }
}

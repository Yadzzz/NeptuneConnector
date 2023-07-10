using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneConnector.Communication.Outgoing.Packets
{
    public class AuthenticationRequestComposer : ServerPacket
    {
        public AuthenticationRequestComposer(string accountSid, string authenticationToken, int applicationId) : base(OutgoingPacketHeaders.AuthenticationRequestComposer)
        {
            base.WriteString(accountSid);
            base.WriteString(authenticationToken);
            base.WriteInt(applicationId);
        }
    }
}

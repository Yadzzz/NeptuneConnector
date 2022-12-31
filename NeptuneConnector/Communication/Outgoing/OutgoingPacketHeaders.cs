using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneServer.Communication.Outgoing
{
    public class OutgoingPacketHeaders
    {
        //AuthenticationRequest
        public const int AuthenticationRequestComposer = 1001;

        //Application
        public const int ApplicationLogComposer = 1010;
    }
}

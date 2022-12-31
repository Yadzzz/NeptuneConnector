using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneServer.Communication.Incoming
{
    public class IncomingPacketHeaders
    {
        //Authentication
        public const int AuthenticationComplete = 10001;
        public const int AuthenticationDenied = 10002;
    }
}

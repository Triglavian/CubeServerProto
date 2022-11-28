using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeServerTest.PacketData
{
    public enum Protocol
    {
        DISCONNECTED,
        INVALID,
        REQ_STAGELIST,
        RES_STAGELIST,
        REQ_STAGE,
        RES_STAGE,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace CubeServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerBase server = new ServerBase();
            server.Run();
        }
    }
}

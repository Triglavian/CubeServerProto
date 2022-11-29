using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace CubeServerTest
{
    class ClientCommunication
    {
        TcpClient client = null;
        NStream stream;
        public ClientCommunication()
        {

        }
        public void Start(TcpClient client)
        {
            this.client = client;
            Thread commBase = new Thread(MainLogic);
            stream = new NStream(this.client.GetStream());   
            commBase.Start();
        }
        void MainLogic()
        {

        }
        private ClientCommunication(ClientCommunication obj) { }
    }
}

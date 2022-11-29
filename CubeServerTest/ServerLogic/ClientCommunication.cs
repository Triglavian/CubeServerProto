using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using CubeServerTest.State;
using CubeServerTest.PacketData;

namespace CubeServerTest
{
    class ClientCommunication
    {
        TcpClient client = null;
        NStream stream;
        MainState state;
        public ClientCommunication()
        {
            state = MainState.IDLE;
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
            while (true)
            {
                stream.Write(Protocol.WAIT);
                switch (state)
                {
                    case MainState.IDLE:

                        break;
                    case MainState.CUSTOM:

                        break;
                }
            }
        }
        void SwitchState()
        {

        }
        private ClientCommunication(ClientCommunication obj) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

using CubeServerTest.PacketData;
using CubeServerTest.ServerLogic;
using CubeServerTest.State;

namespace CubeServerTest
{
    class ClientCommunication
    {
        TcpClient client = null;
        NStream stream = null;
        MainState state;
        ClientMainData data = null;

        #region Service
        CustomStageManager customManager = null;
        #endregion

        public ClientCommunication()
        {
            state = MainState.IDLE;
        }
        ~ClientCommunication()
        {
            Console.WriteLine("IP : {0}, Port : {1} Disconnected",
                IPAddress.Parse(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString()),
                IPAddress.Parse(((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString()));
        }
        public void Start(TcpClient client)
        {
            this.client = client;
            Thread commBase = new Thread(MainLogic);
            stream = new NStream(this.client.GetStream());
            data = new ClientMainData();
            customManager = new CustomStageManager(stream);
            Console.WriteLine("IP : {0}, Port : {1} Connected",
                IPAddress.Parse(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString()),
                IPAddress.Parse(((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString()));
            commBase.Start();
        }
        void MainLogic()
        {
            bool flag = true;
            while (flag)
            {
                switch (state)
                {
                    case MainState.INVALID:
                        InvalidProtocol();
                        return;
                    case MainState.IDLE:
                        SwitchState();
                        break;
                    case MainState.CUSTOM:
                        customManager.StartManager();
                        break;
                    case MainState.EXIT:
                        flag = false;
                        break;
                }
            }
        }
        void SwitchState()
        {
            stream.Read(out data.protocol);
            switch (data.protocol)
            {
                case Protocol.ENT_CUSTOM:
                    state = MainState.CUSTOM;
                    break;
                default:
                    state = MainState.INVALID;
                    break;
            }
        }
        void InvalidProtocol()
        {
            stream.Write(Protocol.INVALID);
        }
        private ClientCommunication(ClientCommunication obj) { }
    }
}

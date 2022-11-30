using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using CubeServerTest.ServerLogic;

namespace CubeServerTest
{
    class ServerBase
    {
        TcpListener listener = null;
        TcpClient client = null;
        int port = 9400;
        public void Run()
        {
            try
            {
                if (!Init())
                {
                    Console.WriteLine("listener failed");
                    return;
                }
                while (true)
                {
                    client = listener.AcceptTcpClient();
                    ClientCommunication _client = new ClientCommunication();
                    _client.Start(client);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {

            }
        }
        bool Init()
        {
            listener = new TcpListener(IPAddress.Any, port);
            if (listener == null) return false;
            if (FileManager.GetInstance() is null) 
            {
                return false;
            }
            listener.Start();
            return true;
        }
    }
}

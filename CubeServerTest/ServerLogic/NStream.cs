﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CubeServerTest
{
    class NStream
    {
        NetworkStream stream = null;
        //StreamReader reader;
        //StreamWriter writer;
        const int bufferSize = 4096;
        byte[] buffer = new byte[bufferSize];
        public NStream(NetworkStream stream)
        {
            this.stream = stream;
            //reader = new StreamReader(this.stream, Encoding.UTF8);
            //writer = new StreamWriter(this.stream, Encoding.UTF8);
        }
        ~NStream()
        {
            //if (writer != null) writer.Close();
            //if (reader != null) reader.Close();
            if (stream != null) stream.Close();
        }
        public void Read()
        {
            //stream.Read

            Console.WriteLine(buffer);
        }
        //public int ReadProtocol()
        //{
        //    //return stream.Read(buffer, 0, sizeof(int) + sizeof(PacketData.Protocol));
        //    return 0;
        //}
        //public int Read(int size)
        //{
        //    //return stream.Read(buffer, 0, size);
        //    reader.ReadLine();
        //}
        public void Write()
        {
            string msg = "Hello World!";
            buffer = Encoding.ASCII.GetBytes(msg);
            //writer.WriteLine(buffer);
        }
    }
}
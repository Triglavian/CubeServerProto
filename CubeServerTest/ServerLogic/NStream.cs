using CubeServerTest.PacketData;
using System;
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
        byte[] readBuffer = new byte[bufferSize];
        byte[] writeBuffer = new byte[bufferSize];
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
        #region Read
        public bool Read(out Protocol protocol)
        {
            if (stream == null)
            {
                protocol = Protocol.INVALID;
                return false;
            }
            if (readBuffer == null)
            {
                readBuffer = new byte[bufferSize];
            }
            Array.Clear(readBuffer, 0, bufferSize);
            int readSize = stream.Read(readBuffer, 0, sizeof(int));
            if (readSize <= 0)
            {
                protocol = Protocol.INVALID;
                return false;
            }
            int pivot = 0;
            int packetSize = BitConverter.ToInt32(readBuffer, pivot);
            pivot += sizeof(int);
            if (readBuffer == null)
            {
                protocol = Protocol.INVALID;
                return false;
            }
            readSize = stream.Read(readBuffer, pivot, sizeof(Protocol));
            if (readSize <= 0)
            {
                protocol = Protocol.INVALID;
                return false;
            }
            protocol = (Protocol)BitConverter.ToInt32(readBuffer, pivot);
            pivot += sizeof(Protocol);
            return true;
        }
        public bool Read(out Protocol protocol, out int data)
        {
            if (stream == null)
            {
                protocol = Protocol.INVALID;
                data = 0;
                return false;
            }
            if (readBuffer == null)
            {
                readBuffer = new byte[bufferSize];
            }
            Array.Clear(readBuffer, 0, bufferSize);
            int readSize = stream.Read(readBuffer, 0, sizeof(int));
            if (readSize <= 0)
            {
                protocol = Protocol.INVALID;
                data = 0;
                return false;
            }
            int pivot = 0;
            int packetSize = BitConverter.ToInt32(readBuffer, pivot);
            pivot += sizeof(int);
            if (readBuffer == null)
            {
                protocol = Protocol.INVALID;
                data = 0;
                return false;
            }
            readSize = stream.Read(readBuffer, pivot, sizeof(Protocol));
            if (readSize <= 0)
            {
                protocol = Protocol.INVALID;
                data = 0;
                return false;
            }
            protocol = (Protocol)BitConverter.ToInt32(readBuffer, pivot);
            pivot += sizeof(Protocol);
            readSize = stream.Read(readBuffer, pivot, sizeof(int));
            if (readSize <= 0)
            {
                protocol = Protocol.INVALID;
                data = 0;
                return false;
            }
            int dataSize = BitConverter.ToInt32(readBuffer, pivot);
            pivot += sizeof(int);
            readSize = stream.Read(readBuffer, pivot, dataSize);
            if (readSize <= 0)
            {
                protocol = Protocol.INVALID;
                data = 0;
                return false;
            }
            //byte[] tempBuffer = new byte[sizeof(int)];
            //Array.Copy(readBuffer, pivot, tempBuffer, 0, dataSize);
            //data = int.Parse(tempBuffer.ToString());
            data = BitConverter.ToInt32(readBuffer, pivot);
            return true;
        }
        public bool Read(out Protocol protocol, out byte[] data)
        {
            if (stream == null)
            {
                protocol = Protocol.INVALID;
                data = null;
                return false;
            }
            if (readBuffer == null)
            {
                readBuffer = new byte[bufferSize];
            }
            Array.Clear(readBuffer, 0, bufferSize);
            int readSize = stream.Read(readBuffer, 0, sizeof(int));
            if (readSize <= 0)
            {
                protocol = Protocol.INVALID;
                data = null;
                return false;
            }
            int pivot = 0;
            int packetSize = BitConverter.ToInt32(readBuffer, pivot);
            pivot += sizeof(int);
            if (readBuffer == null)
            {
                protocol = Protocol.INVALID;
                data = null;
                return false;
            }
            readSize = stream.Read(readBuffer, pivot, sizeof(Protocol));
            if (readSize <= 0)
            {
                protocol = Protocol.INVALID;
                data = null;
                return false;
            }
            protocol = (Protocol)BitConverter.ToInt32(readBuffer, pivot);
            pivot += sizeof(Protocol);
            readSize = stream.Read(readBuffer, pivot, sizeof(int));
            if (readSize <= 0)
            {
                protocol = Protocol.INVALID;
                data = null;
                return false;
            }
            int dataSize = BitConverter.ToInt32(readBuffer, pivot);
            pivot += sizeof(int);
            readSize = stream.Read(readBuffer, pivot, dataSize);
            if (readSize <= 0)
            {
                protocol = Protocol.INVALID;
                data = null;
                return false;
            }
            data = new byte[bufferSize];
            Array.Copy(readBuffer, pivot, data, 0, dataSize);
            return true;
        }
        #endregion
        #region Write
        public bool Write(Protocol protocol)
        {
            if (stream == null)
            {
                return false;
            }
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            Array.Clear(writeBuffer, 0, bufferSize);
            int packetSize = sizeof(int);
            if (writeBuffer == null) 
            {
                writeBuffer = new byte[bufferSize];
            }
            BitConverter.GetBytes((int)protocol).CopyTo(writeBuffer, packetSize);
            packetSize += sizeof(Protocol);
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            BitConverter.GetBytes(packetSize - sizeof(int)).CopyTo(writeBuffer, 0);
            stream.Write(writeBuffer, 0, packetSize);
            return true;
        }
        public bool Write(Protocol protocol, int data)
        {
            if (stream == null)
            {
                return false;
            }
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            Array.Clear(writeBuffer, 0, bufferSize);
            int packetSize = sizeof(int);
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            BitConverter.GetBytes((int)protocol).CopyTo(writeBuffer, packetSize);
            packetSize += sizeof(Protocol);
            int dataSize = sizeof(int);
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            BitConverter.GetBytes(dataSize).CopyTo(writeBuffer, packetSize);
            packetSize += sizeof(int);
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            BitConverter.GetBytes(data).CopyTo(writeBuffer, packetSize);
            packetSize += dataSize;
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            BitConverter.GetBytes(packetSize - sizeof(int)).CopyTo(writeBuffer, 0);
            stream.Write(writeBuffer, 0, packetSize);
            return true;
        }
        public bool Write(Protocol protocol, byte[] data)
        {
            int leftSize = data.Length;
            if (stream == null)
            {
                return false;
            }
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            Array.Clear(writeBuffer, 0, bufferSize);
            int packetSize = sizeof(int);
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            BitConverter.GetBytes((int)protocol).CopyTo(writeBuffer, packetSize);
            packetSize += sizeof(Protocol);
            int dataSize = data.Length;
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            BitConverter.GetBytes(dataSize).CopyTo(writeBuffer, packetSize);
            packetSize += sizeof(int);
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            //data.CopyTo(writeBuffer, packetSize);
            Array.Copy(data, 0, writeBuffer, packetSize, data.Length);
            packetSize += data.Length;
            if (writeBuffer == null)
            {
                writeBuffer = new byte[bufferSize];
            }
            BitConverter.GetBytes(packetSize - sizeof(int)).CopyTo(writeBuffer, 0);
            stream.Write(writeBuffer, 0, packetSize);
            return true;
        }
        #endregion
        public int GetBufferSize()
        {
            return bufferSize;
        }
    }
}

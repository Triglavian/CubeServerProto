using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace CubeServerTest.ServerLogic.ClientData
{
    class CustomManagerData : ClientMainData
    {
        public byte[] buffer = null;
        public int id;
        public CustomManagerData(int size)
        {
            buffer = new byte[size];
            id = 0;
        }
    }
}

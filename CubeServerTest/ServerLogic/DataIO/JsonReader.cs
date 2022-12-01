using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CubeServerTest.PacketData;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CubeServerTest.ServerLogic.DataIO
{
    class JsonReader
    {
        public static byte[] Convert(string directory)
        {
            //StageData data = JsonConvert.DeserializeObject<StageData>(File.ReadAllText(directory), 
            //    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto});
            //dynamic obj = JObject.Parse(File.ReadAllText(directory));
            //StageData data =JsonConvert.DeserializeObject<StageData>(File.ReadAllText(directory), )
            //System.Text.Json;
            string data = File.ReadAllText(directory);
            char[] token = { '\"', ',', ':', '{', '}' };
            string[] tokens = data.Split(token);
            int pivot = 0;
            byte[] buffer = new byte[0];
            for (int index = 0; index < tokens.Length;)
            {
                switch (tokens[index])
                {
                    case "stageId":
                    case "type":
                    case "cubeIndex":
                    case "wallIndex":
                    case "color":
                        Array.Resize(ref buffer, buffer.Length + sizeof(int));
                        BitConverter.GetBytes(int.Parse(tokens[index += 2])).CopyTo(buffer, pivot);
                        pivot += sizeof(int);
                        break;
                    case "}":
                        break;
                }
                index++;
            }
            Console.WriteLine();
            return buffer;
        }   
    }
}

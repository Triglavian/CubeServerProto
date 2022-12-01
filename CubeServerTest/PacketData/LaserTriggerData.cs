using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CubeServerTest.PacketData
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class LaserTriggerData : PuzzleObjectData
    {
        LaserColor color;
        public LaserTriggerData(int cubeIndex, int wallIndex, LaserColor color)
        {
            this.type = ObjectType.LASER_TRIGGER;
            this.cubeIndex = cubeIndex;
            this.wallIndex = wallIndex;
            this.color = color;
        }
        public override void Serialize(byte[] buffer, int startPos)
        {
            int dataSize = 0;
            Buffer.BlockCopy(BitConverter.GetBytes((int)type), 0, buffer, startPos + dataSize, sizeof(ObjectType));
            dataSize += sizeof(ObjectType);
            Buffer.BlockCopy(BitConverter.GetBytes((int)cubeIndex), 0, buffer, startPos + dataSize, sizeof(int));
            dataSize += sizeof(int);
            Buffer.BlockCopy(BitConverter.GetBytes((int)wallIndex), 0, buffer, startPos + dataSize, sizeof(int));
            dataSize += sizeof(int);
            Buffer.BlockCopy(BitConverter.GetBytes((int)color), 0, buffer, startPos + dataSize, sizeof(LaserColor));
        }

        public override int Size()
        {
            return Marshal.SizeOf(this);
        }

        public override string ToJson()
        {
            return $"{{\"type\":{type},\"cubeIndex\":{cubeIndex},\"wallIndex\":{wallIndex},\"color\":{color}}}";
        }
    }
}

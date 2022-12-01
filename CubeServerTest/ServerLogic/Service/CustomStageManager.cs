using CubeServerTest.PacketData;
using CubeServerTest.ServerLogic.ClientData;
using CubeServerTest.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeServerTest.ServerLogic
{
    class CustomStageManager
    {
        NStream stream;
        CustomManagerData data;
        CustomState state;
        public CustomStageManager(NStream stream)
        {
            this.stream = stream;
            data = new CustomManagerData(stream.GetBufferSize());
            state = CustomState.IDLE;
        }
        public bool StartManager()
        {
            bool flag = true;
            while (flag)
            {
                switch (state)
                {
                    case CustomState.INVALID:
                        InvalidProtocol();
                        return false;
                    case CustomState.IDLE:
                        SwitchState();
                        break;
                    case CustomState.REQSTAGE:
                        WriteStageData();
                        break;
                    case CustomState.REQSTLIST:
                        WriteStageList();
                        break;
                    case CustomState.EXIT:
                        flag = false;
                        break;

                }
            }
            return true;
        }
        void WriteStageData()
        {
            if (stream.Read(out data.protocol, out data.id) == false) 
            {
                state = CustomState.EXIT;
                return;
            }
            if (data.protocol != Protocol.REQ_STAGE)
            {
                Console.WriteLine("InvalidProtocol, WriteStageData()");
                state = CustomState.INVALID;
                return;
            }
            data.buffer = FileManager.GetInstance().GetStageData(data.id);
            stream.Write(Protocol.RES_STAGE, data.buffer);
            state = CustomState.IDLE;
        }
        void WriteStageList()
        {

            state = CustomState.IDLE;
        }
        void SwitchState()
        {
            stream.Read(out data.protocol);
            switch (data.protocol)
            {
                case Protocol.REQ_STAGELIST:
                    state = CustomState.REQSTLIST;
                    break;
                case Protocol.REQ_STAGE:
                    state = CustomState.REQSTAGE;
                    break;
                default:
                    state = CustomState.INVALID;
                    break;
            }
        }
        void InvalidProtocol()
        {
            stream.Write(Protocol.INVALID);
        }
    }
}

using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeServerTest.PacketData
{
    public class StageDataConverter : CustomCreationConverter<StageData>
    {
        public override StageData Create(Type objectType)
        {
            return null;
        }
    }
}

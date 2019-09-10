using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracing.Trace;
using Newtonsoft.Json;

namespace Tracing.Serializer
{
    class JsonTraceSerializer : ISerializable
    {
        public string Serialize(TraceResult value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
    }
}

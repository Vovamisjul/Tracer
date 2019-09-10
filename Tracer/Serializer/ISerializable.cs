using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracing.Trace;

namespace Tracing.Serializer
{
    interface ISerializable
    {
        string Serialize(TraceResult value);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tracing.Trace;

namespace Tracing.Serializer
{
    class XmlTraceSerializer : ISerializable
    {
        public string Serialize(TraceResult value)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(value.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, value);
                return textWriter.ToString();
            }
        }
    }
}

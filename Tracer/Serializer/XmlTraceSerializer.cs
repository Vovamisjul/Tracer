using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Tracing.Trace;

namespace Tracing.Serializer
{
    public class XmlTraceSerializer : ISerializable
    {
        public string Serialize(TraceResult value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            
                var xmlserializer = new XmlSerializer(value.GetType());
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Indent = true, IndentChars = "\t" }))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
        }
    }
}

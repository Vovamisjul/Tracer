using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracing.Logging
{
    public class FileLogger : ILogger
    {

        public void Log(string value)
        {
            using (var stream = File.Create("log.log"))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine($"Trace result for : {DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")}");
                writer.WriteLine(value);
            }
        }
    }
}

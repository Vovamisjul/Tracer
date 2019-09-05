using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tracer.Tracer
{
    class Tracer : ITracer
    {
        TraceResult traceResult = new TraceResult();
        public TraceResult GetTraceResult()
        {
            return traceResult;
        }

        public void StartTrace()
        {
            var stackFrame = new StackFrame(1);
            stackFrame.GetMethod();
            if (traceResult.threads.Any(elem => elem.Id == Thread.CurrentThread.ManagedThreadId))
                traceResult.threads


        }

        public void StopTrace()
        {
            throw new NotImplementedException();
        }
    }
}

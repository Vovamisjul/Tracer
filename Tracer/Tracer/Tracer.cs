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
            var method = new StackFrame(1).GetMethod();
            if (traceResult.threads.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                traceResult.threads[Thread.CurrentThread.ManagedThreadId].methods.StartTrace(method);
            else
                traceResult.AddNewThread(Thread.CurrentThread.ManagedThreadId, method);

        }

        public void StopTrace()
        {
            throw new NotImplementedException();
        }
    }
}

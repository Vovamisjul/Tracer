﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer.Tracer;

namespace TracerImpl
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
            traceResult.TryAddNewThread(Thread.CurrentThread.ManagedThreadId, method);

        }

        public void StopTrace()
        {
            traceResult.StopTrace();
        }
    }
}

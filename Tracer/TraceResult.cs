using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer

{
    class TraceResult
    {
        public List<MeasuredThread> threads = new List<MeasuredThread>();

        public void AddNewThread(MethodBase method)
        {
            MeasuredThread thread = new MeasuredThread(method);

        }
    }

    class MeasuredMethod
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public long Time { get; set; }
        public List<MeasuredMethod> Methods { get; set; }
        private Stopwatch Watch { get; set; } = new Stopwatch();

        public MeasuredMethod(MethodBase method)
        {
            MethodName = method.Name;
            ClassName = method.DeclaringType.Name;
            Watch.Start();
        }

        public void StopMeasure()
        {
            Watch.Stop();
            Time = Watch.ElapsedMilliseconds;
        }
    }

    class MeasuredThread
    {
        public int Id { set; get; }
        public long Time { set; get; }
        public List<MeasuredMethod> methods { set; get; }

        public MeasuredThread(MethodBase method)
        {
            Id = Thread.CurrentThread.ManagedThreadId;
            methods.Add(new MeasuredMethod(method));
        }
    }


}

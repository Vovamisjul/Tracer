using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer

{
    class TraceResult
    {
        public ConcurrentDictionary<int, MeasuredThread> threads = new ConcurrentDictionary<int, MeasuredThread>();

        public void AddNewThread(int id, MethodBase method)
        {
            threads.TryAdd(id, new MeasuredThread(method));
        }
    }

    class MeasuredMethod
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public long Time { get; set; }
        public List<MeasuredMethod> Methods = new List<MeasuredMethod>();
        private Stopwatch Watch { get; set; } = new Stopwatch();
        private int _currentMethod = 0;

        public MeasuredMethod(MethodBase method)
        {
            MethodName = method.Name;
            ClassName = method.DeclaringType.Name;
            Methods.Add(new MeasuredMethod(method));
            Watch.Start();
        }
        public void StopTrace()
        {
            Watch.Stop();
            Time = Watch.ElapsedMilliseconds;
        }
    }

    class MeasuredThread
    {
        public long Time { set; get; }
        public List<MeasuredMethod> Methods = new List<MeasuredMethod>();
        private Stack<MeasuredMethod> _stackMethods = new Stack<MeasuredMethod>();
        public MeasuredThread(MethodBase method)
        {
            var newMethod = new MeasuredMethod(method);
            if (_stackMethods.Count == 0)
                Methods.Add(newMethod);
            else
                _stackMethods.Peek().Methods.Add(newMethod);
            _stackMethods.Push(newMethod);
        }
        public void StopTrace()
        {
            _stackMethods.Pop().StopTrace();
        }

    }


}

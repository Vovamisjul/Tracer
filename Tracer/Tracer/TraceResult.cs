using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Tracer.Tracer

{
    class TraceResult
    {
        private List<MeasuredThread> threads = new List<MeasuredThread>();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void TryAddNewThread(int id, MethodBase method)
        {
            bool find = false;
            foreach (var thread in threads)
            {
                if (thread.Id == id)
                {
                    thread.AddNewMethod(new MeasuredMethod(method));
                    find = true;
                }
            }
            if (!find)
                threads.Add(new MeasuredThread(id, method));
        }

        public void StopTrace()
        {
            foreach (var thread in threads)
            {
                thread.StopTrace();
            }
        }
    }

    class MeasuredMethod
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public long Time { get; set; }
        public List<MeasuredMethod> Methods = new List<MeasuredMethod>();
        private Stopwatch Watch { get; set; } = new Stopwatch();

        public MeasuredMethod(MethodBase method)
        {
            MethodName = method.Name;
            ClassName = method.DeclaringType.Name;
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
        public int Id { set; get; }
        private Stack<MeasuredMethod> _stackMethods = new Stack<MeasuredMethod>();
        public MeasuredThread(int id, MethodBase method)
        {
            Id = id;
            var newMethod = new MeasuredMethod(method);
            if (_stackMethods.Count == 0)
                Methods.Add(newMethod);
            else
                _stackMethods.Peek().Methods.Add(newMethod);
            _stackMethods.Push(newMethod);
        }

        public void AddNewMethod(MeasuredMethod method)
        {
            if (_stackMethods.Count == 0)
                Methods.Add(method);
            else
                _stackMethods.Peek().Methods.Add(method);
            _stackMethods.Push(method);
        }
        public void StopTrace()
        {
            _stackMethods.Pop().StopTrace();
        }

    }


}

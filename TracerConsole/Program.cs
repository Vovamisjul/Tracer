using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracing.Trace;
using Tracing.Serializer;
using Tracing.Logging;

namespace TracerProgram
{
    class Program
    {
        static Tracer tracer = new Tracer();
        static ILogger logger = new FileLogger();
        static void Main(string[] args)
        {
            tracer.StartTrace();
            Console.WriteLine("main");
            A a = new A(tracer);
            a.kek();
            a.lol();
            tracer.StopTrace();
            new Thread(() =>
            {
                tracer.StartTrace();
                A a1 = new A(tracer);
                a1.lol();
                B b = new B(tracer);
                b.lol();
                tracer.StopTrace();
            }).Start();
            Thread.Sleep(1000);
            var result = tracer.GetTraceResult();
            logger.Log(new JsonTraceSerializer().Serialize(result));
        }
    }

    class A
    {
        private Tracer tracer;
        public A(Tracer tracer)
        {
            this.tracer = tracer;
        }
        public void kek()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            Console.WriteLine("a.kek()");
            tracer.StopTrace();
        }
        public void lol()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            Console.WriteLine("a.lol()");
            new B(tracer).kek();
            tracer.StopTrace();
        }
    }
    class B
    {
        private Tracer tracer;
        public B(Tracer tracer)
        {
            this.tracer = tracer;
        }
        public void kek()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            Console.WriteLine("b.kek()");
            tracer.StopTrace();
        }
        public void lol()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            Console.WriteLine("b.lol()");
            tracer.StopTrace();
        }
    }
}

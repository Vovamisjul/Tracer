using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracing.Trace;

namespace TracerProgram
{
    class Program
    {
        static Tracer tracer = new Tracer();
        static void Main(string[] args)
        {
            tracer.StartTrace();
            Console.WriteLine("aaaa");
            A a = new A(tracer);
            a.kek();
            a.lol();
            tracer.StopTrace();
            var result = tracer.GetTraceResult();
            Console.WriteLine(result);
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

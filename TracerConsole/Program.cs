using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TracerImpl;

namespace TracerProgram
{
    class Program
    {
        static Tracer tracer = new Tracer();
        static void Main(string[] args)
        {
            Console.WriteLine("aaaa");
            A a = new A();
            a.kek();
            a.lol();
        }
    }

    class A
    {
        public void kek()
        {
            Thread.Sleep(100);
            Console.WriteLine("a.kek()");
        }
        public void lol()
        {
            Thread.Sleep(100);
            Console.WriteLine("a.kek()");
            new B().kek();
        }
    }
    class B
    {
        public void kek()
        {
            Thread.Sleep(100);
            Console.WriteLine("b.kek()");
        }
        public void lol()
        {
            Thread.Sleep(100);
            Console.WriteLine("b.kek()");
        }
    }
}

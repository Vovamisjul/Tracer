using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracing.Trace;

namespace Tracing.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void When_NewThread_Expect_2Threads()
        {
            Tracer tracer = new Tracer();
            tracer.StartTrace();
            tracer.StopTrace();
            new Thread(() =>
            {
                tracer.StartTrace();
                tracer.StopTrace();
            }).Start();
            Thread.Sleep(100);
            Assert.AreEqual(tracer.GetTraceResult().threads.Count, 2);      
        }

        [Test]
        public void When_Alol_Expected_3Methods()
        {
            Tracer tracer = new Tracer();
            tracer.StartTrace();
            new A(tracer).lol();
            tracer.StopTrace();
            Assert.NotNull(tracer.GetTraceResult().threads[0].Methods[0].Methods[0].Methods[0]);
        }

        [Test]
        public void When_Blol_Expected_300ms()
        {
            Tracer tracer = new Tracer();
            tracer.StartTrace();
            new B(tracer).lol();
            tracer.StopTrace();
            Assert.GreaterOrEqual(tracer.GetTraceResult().threads[0].Methods[0].Methods[0].Time, 300);
        }

        [Test]
        public void When_nothing_Expected_1method()
        {

            Tracer tracer = new Tracer();
            tracer.StartTrace();
            new B(tracer).lol();
            tracer.StopTrace();
            Assert.AreEqual(tracer.GetTraceResult().threads[0].Methods.Count, 1);
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
            Thread.Sleep(300);
            Console.WriteLine("b.lol()");
            tracer.StopTrace();
        }
    }
}

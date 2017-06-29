using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Controller.printer;
using System.Threading;

namespace Controller
{
    public static class Shared
    {
        public static Boolean TestMode { get; set; }
        public static IPrinter Printer { get; set; }
        public static Worker Worker { get; set; }

        static Shared()
        {
            TestMode = true;

            if (!TestMode)
            {
                Printer = new Printer();
            }
            else
            {
                Printer = new TestPrinter();
            }
            Worker = new Worker(Printer);
            ThreadStart threadDelegate = new ThreadStart(Worker.Work);
            Thread t = new Thread(threadDelegate);
            t.Start();
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Logger
{
    public static class Logger
    {
        private static object _lock = new Object();

        public static void Log(Exception e)
        {
            lock (_lock)
            {
                using (StreamWriter writetext = new StreamWriter("Log-LukaDubrovnik-" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day +  ".txt", true))
                {
                    writetext.WriteLine(String.Format("{0} - {1}{2}{3}", DateTime.Now, e.Message, Environment.NewLine, e.StackTrace));
                }
            }
        }

        public static void Log(string message)
        {
            lock (_lock)
            {
                using (StreamWriter writetext = new StreamWriter("Log-LukaDubrovnik-" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + ".txt", true))
                {
                    writetext.WriteLine(String.Format("{0} - {1}", DateTime.Now, message));
                }
            }
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Database;

namespace Controller
{
    public static class CounterController
    {
        static Object _lock = new Object();
        public static int GetTicketCount(){
            lock (_lock)
            {
                return Counter.GetTicketCount();
            }
        }

        public static int IncrementCounter()
        {
            lock (_lock)
            {
                int ticketCount = Counter.GetTicketCount();
                ticketCount++;
                Counter.SetTicketCount(ticketCount);
                return ticketCount;
            }
        }

        public static void ResetCounter()
        {
            lock (_lock)
            {
                Counter.SetTicketCount(1);
            }
        }
    }
}

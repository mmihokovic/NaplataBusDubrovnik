using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Database
{
    public class ChargedTickets
    {
        static List<Ticket> chargedTickets;

        static ChargedTickets()
        {
            chargedTickets = new List<Ticket>();
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Database
{
    public static class TicketTypes
    {
        static List<TicketType> ticketTypes;

        static TicketTypes()
        {
            ticketTypes = new List<TicketType>();
            ticketTypes.Add(new TicketType() { Id = 1, Type = "Pojedinačni prijevoz" });
            ticketTypes.Add(new TicketType() { Id = 2, Type = "Dnevna karta" });
            ticketTypes.Add(new TicketType() { Id = 3, Type = "Tjedna karta" });
            ticketTypes.Add(new TicketType() { Id = 4, Type = "Mjesečna karta" });
            ticketTypes.Add(new TicketType() { Id = 5, Type = "Pretplatnička karta" });
        }

        public static List<TicketType> GetAllTicketTypes()
        {
            return ticketTypes;
        }
    }
}

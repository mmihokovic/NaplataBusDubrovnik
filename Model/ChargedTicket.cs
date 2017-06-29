using System;

namespace Model
{
    public class ChargedTicket
    {
        public string Username { get; set; }

        public string TicketType { get; set; }

        public string VehicleType { get; set; }

        public string LicencePlates { get; set; }

        public Decimal Charged { get; set; }
    }
}


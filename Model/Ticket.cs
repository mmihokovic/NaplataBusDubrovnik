using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Ticket
    {
        public String LicencePlates { get; set; }
        public String VehicleType { get; set; }
        public String TicketType { get; set; }
        public String Username { get; set; }
        public DateTime CheckedIn { get; set; }
        public DateTime CheckedOut { get; set; }
        public Decimal Charged { get; set; }
        public Decimal ChargedBase { get; set; }
        public Decimal ChargedTax { get; set; }
        public String JIR { get; set; }
        public String ZIK { get; set; }
        public bool ChargeTicket { get; set; }
        public int TicketCount { get; set; }
    }
}
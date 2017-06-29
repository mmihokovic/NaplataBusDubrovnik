using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Price
    {
        public decimal Charge { get; set; }
        public bool SummerTariff { get; set; }
        public String TicketType { get; set; }
        public String VehicleType { get; set; }
    }
}

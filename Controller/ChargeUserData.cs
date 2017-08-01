using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public class ChargeUserData
    {
        public string LicencePlate { get; set; }

        public ChargeSourceEnum ChargeSource { get; set; }

        public Subscriber Subscriber { get; set; }

        public DateTime SubscriptionValidTo { get; set; }

        public string VehicleType { get; set; }
    }
}

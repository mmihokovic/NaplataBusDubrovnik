using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Vehicle
    {
        public String LicencePlates { get; set; }
        public String VehicleType { get; set; }
        public bool CheckedIn { get; set; }
        public DateTime CheckedInDate { get; set; }
    }
}

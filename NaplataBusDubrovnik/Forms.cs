using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Controller.printer;
using Controller.barcode;
using System.Windows.Forms;

namespace NaplataBusDubrovnik
{
    public static class Forms
    {
        public static Administration Administration {get; set;}
        public static Login Login { get; set; }
        public static ChargeTickets ChargeTickets { get; set; }
        public static ChargeRegularUser ChargeRegularUser{get; set;}
        public static ChargeSubscriber ChargeSubscriber { get; set; }
        //public static Worker Worker { get; set; }
        public static String LogedUsername { get; set; }
        public static AdministrationCharge AdministrationCharge { get; set; }
        public static AdministrationUsers AdministrationUsers { get; set; }
        public static AdministrationAddUser AdministrationAddUser { get; set; }
        public static AdministrationSubscribers AdministrationSubscribers { get; set; }
        public static AdministrationAddSubscriber AdministrationAddSubscriber { get; set; }
    }
}

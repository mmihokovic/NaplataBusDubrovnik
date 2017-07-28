using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Controller.printer;
using Model;

namespace Controller.tickets
{
    public class SubscriberAddTicket : IPrintObject
    {
        String licencePlate;

        String vehicleType;

        public String VehicleType
        {
            get { return vehicleType; }
            set { vehicleType = value; }
        }

        public String LicencePlate
        {
            get { return licencePlate; }
            set { licencePlate = value; }
        }
        int months;

        public int Months
        {
            get { return months; }
            set { months = value; }
        }
        DateTime validTo;
        decimal charged;

        public decimal Charged
        {
            get { return charged; }
            set { charged = value; }
        }
        decimal chargedBase;
        decimal chargedTax; 
        String username;
        String JIR;
        String ZIK;
        int ticketCount;

        public Company company { get; set; }

        public SubscriberAddTicket(String licencePlate, String vehicleType, int months, DateTime validTo, 
            decimal charged, decimal chargedTax, decimal chargedBase, 
            String username, String JIR, String ZIK, int ticketCount, Company company)
        {
            this.licencePlate = licencePlate;
            this.vehicleType = vehicleType;
            this.months = months;
            this.validTo = validTo;
            this.charged = charged;
            this.chargedBase = chargedBase;
            this.chargedTax = chargedTax;
            this.username = username;
            this.JIR = JIR;
            this.ZIK = ZIK;
            this.ticketCount = ticketCount;
            this.company = company;
        }

        #region IPrintObject Members

        public bool Print()
        {
            Controller.Shared.Printer.SetJustification("center");
            Controller.Shared.Printer.AddTextLine("Luka Dubrovnik d.d.", 4, 0, 0, 10);
            Controller.Shared.Printer.AddTextLine("Obala pape", 5, 0, 0, 70);
            Controller.Shared.Printer.AddTextLine("Ivana Pavla II, 1", 5, 0, 0, 95);
            Controller.Shared.Printer.AddTextLine("20 000 Dubrovnik", 5, 0, 0, 120);
            Controller.Shared.Printer.AddTextLine("Telefon: +385 20 313 511", 5, 0, 0, 145);
            Controller.Shared.Printer.AddTextLine("OIB: 47148433806", 5, 0, 0, 170);

            String ticketHeadline = "RACUN " + ticketCount.ToString() + " - 1 - 1";

            String ticketTypeName = Decimal.Round(charged, 2).ToString("0.##") + " kn";

            Controller.Shared.Printer.AddHorizontalLine(0, 180);
            Controller.Shared.Printer.AddTextLine(ticketHeadline, 5, 2, 0, 210);
            Controller.Shared.Printer.AddTextLine(ticketTypeName, 5, 2, 0, 280);
            Controller.Shared.Printer.SetJustification("left");
            Controller.Shared.Printer.AddTextLine("Osnovica: ", 0, 3, 40, 380);
            Controller.Shared.Printer.AddTextLine(chargedBase.ToString("0.##") + " kn", 0, 3, 40, 420);
            Controller.Shared.Printer.AddTextLine("PDV po stopi 25%: ", 0, 3, 40, 460);
            Controller.Shared.Printer.AddTextLine(chargedTax.ToString("0.##") + " kn", 0, 3, 40, 500);
            Controller.Shared.Printer.AddTextLine("Naplata pretplate za", 0, 3, 40, 540);
            Controller.Shared.Printer.AddTextLine(vehicleType.ToLower() + " " + licencePlate, 0, 3, 40, 580);
            Controller.Shared.Printer.AddTextLine("Pretplata vrijedi do: ", 0, 3, 40, 620);
            
            Controller.Shared.Printer.AddTextLine(validTo.ToString(), 0, 3, 40, 660);
            
            Controller.Shared.Printer.AddTextLine("Operater: ", 0, 3, 40, 740);
            Controller.Shared.Printer.AddTextLine(username, 0, 3, 40, 780);
            Controller.Shared.Printer.AddTextLine("Datum izdavanja: ", 0, 3, 40, 820);
            Controller.Shared.Printer.AddTextLine(DateTime.Now.ToString(), 0, 3, 40, 860);
            Controller.Shared.Printer.AddTextLine("JIR: ", 0, 3, 40, 900);
            Controller.Shared.Printer.AddTextLine(JIR, 0, 0, 40, 940);
            Controller.Shared.Printer.AddTextLine("ZIK: ", 0, 3, 40, 980);
            Controller.Shared.Printer.AddTextLine(ZIK, 0, 0, 40, 1020);

            if (company != null)
            {
                Controller.Shared.Printer.AddHorizontalLine(0, 1030);
                Controller.Shared.Printer.AddTextLine("R1 racun za: ", 0, 3, 40, 1060);
                Controller.Shared.Printer.AddTextLine(company.Name + ",", 0, 3, 40, 1100);
                Controller.Shared.Printer.AddTextLine(company.Address + (String.IsNullOrEmpty(company.OIB) ? "" : ","), 0, 3, 40, 1140);
                if (!String.IsNullOrEmpty(company.OIB))
                {
                    Controller.Shared.Printer.AddTextLine(company.OIB, 0, 3, 40, 1180);
                }
            }

            bool error = Controller.Shared.Printer.Print();

            return error;
        }

        #endregion
    }
}

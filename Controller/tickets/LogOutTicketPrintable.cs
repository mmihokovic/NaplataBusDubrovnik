using Controller;
using Controller.printer;
using Database;
using Model;
using System;
using System.Collections.Generic;

namespace Controller.tickets
{
    internal class LogOutTicketPrintable : IPrintObject
    {
        private List<ChargedTicket> chargedTickets;
        private string username;

        public LogOutTicketPrintable(string username)
        {
            this.username = username;
            this.chargedTickets = Tickets.GetAllChargedTickets(username);
        }

        public bool Print()
        {
            Shared.Printer.SetJustification("center");
            Shared.Printer.AddTextLine("Luka Dubrovnik d.d.", 4, 0, 0, 10);
            Shared.Printer.AddTextLine("Obala pape", 5, 0, 0, 70);
            Shared.Printer.AddTextLine("Ivana Pavla II, 1", 5, 0, 0, 95);
            Shared.Printer.AddTextLine("20 000 Dubrovnik", 5, 0, 0, 120);
            Shared.Printer.AddTextLine("Telefon: +385 20 313 511", 5, 0, 0, 145);
            Shared.Printer.AddTextLine("OIB: 47148433806", 5, 0, 0, 170);
            string line1 = "ODJAVA";
            string line2 = this.username;
            Shared.Printer.AddHorizontalLine(0, 180);
            Shared.Printer.AddTextLine(line1, 5, 2, 0, 210);
            Shared.Printer.AddTextLine(line2, 5, 2, 0, 280);
            Shared.Printer.SetJustification("left");
            Shared.Printer.AddTextLine("u: " + DateTime.Now.ToString(), 5, 2, 40, 320);
            Shared.Printer.AddTextLine("Pojedinacan prijevoz", 5, 2, 40, 360);
            Shared.Printer.AddTextLine("Kamion: " + (object)this.countChargedTicket("Kamion", "Pojedinačni prijevoz"), 5, 2, 42, 400);
            Shared.Printer.AddTextLine("osobno-v: " + (object)this.countChargedTicket("osobno-v", "Pojedinačni prijevoz"), 5, 2, 42, 440);
            Shared.Printer.AddTextLine("Bus: " + (object)this.countChargedTicket("Bus", "Pojedinačni prijevoz"), 5, 2, 42, 480);
            Shared.Printer.AddTextLine("Dnevna karta", 5, 2, 40, 520);
            Shared.Printer.AddTextLine("Kamion: " + (object)this.countChargedTicket("Kamion", "Dnevna karta"), 5, 2, 42, 560);
            Shared.Printer.AddTextLine("osobno-v: " + (object)this.countChargedTicket("osobno-v:", "Dnevna karta"), 5, 2, 42, 600);
            Shared.Printer.AddTextLine("Bus: " + (object)this.countChargedTicket("Bus", "Dnevna karta"), 5, 2, 42, 640);
            Shared.Printer.AddTextLine("Tjedna karta", 5, 2, 40, 680);
            Shared.Printer.AddTextLine("Kamion: " + (object)this.countChargedTicket("Kamion", "Tjedna karta"), 5, 2, 42, 720);
            Shared.Printer.AddTextLine("osobno-v: " + (object)this.countChargedTicket("osobno-v", "Tjedna karta"), 5, 2, 42, 760);
            Shared.Printer.AddTextLine("Bus: " + (object)this.countChargedTicket("Bus", "Tjedna karta"), 5, 2, 42, 800);
            Shared.Printer.AddTextLine("Mjesecna karta", 5, 2, 40, 840);
            Shared.Printer.AddTextLine("Kamion " + (object)this.countChargedTicket("Kamion", "Mjesečna karta"), 5, 2, 42, 880);
            Shared.Printer.AddTextLine("osobno-v: " + (object)this.countChargedTicket("osobno-v", "Mjesečna karta"), 5, 2, 42, 920);
            Shared.Printer.AddTextLine("Bus: " + (object)this.countChargedTicket("Bus", "Mjesečna karta"), 5, 2, 42, 960);
            Shared.Printer.AddTextLine("Pretplatnička karta", 5, 2, 40, 1000);
            Shared.Printer.AddTextLine("Kamion " + (object)this.countChargedTicket("Kamion", "Pretplatnička karta"), 5, 2, 42, 1040);
            Shared.Printer.AddTextLine("osobno-v: " + (object)this.countChargedTicket("osobno-v", "Pretplatnička karta"), 5, 2, 42, 1080);
            Shared.Printer.AddTextLine("Bus: " + (object)this.countChargedTicket("Bus", "Pretplatnička karta"), 5, 2, 42, 1120);
            Shared.Printer.SetJustification("center");
            Shared.Printer.AddHorizontalLine(0, 1180);
            Shared.Printer.AddTextLine("Ukupno: " + this.calcCharged().ToString("0.##") + " kn", 5, 3, 0, 1220);
            Shared.Printer.AddTextLine("", 5, 3, 0, 1260);
            bool flag = Shared.Printer.Print();
            if (!flag)
                Tickets.RemoveChargedTickets(this.username);
            return flag;
        }

        private Decimal calcCharged()
        {
            Decimal d = new Decimal(0);
            if (this.chargedTickets == null)
                return d;
            foreach (ChargedTicket chargedTicket in this.chargedTickets)
                d += chargedTicket.Charged;
            return Decimal.Round(d, 2);
        }

        private int countChargedTicket(string vehicleType, string ticketType)
        {
            int num = 0;
            if (this.chargedTickets == null)
                return num;
            foreach (ChargedTicket chargedTicket in this.chargedTickets)
            {
                if (chargedTicket.VehicleType.ToLower().Equals(vehicleType.ToLower()) && chargedTicket.TicketType.ToLower().Equals(ticketType.ToLower()))
                    ++num;
            }
            return num;
        }
    }
}

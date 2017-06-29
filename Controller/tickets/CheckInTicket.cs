using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using Controller.printer;

namespace Controller.tickets
{
    public class CheckInTicket : IPrintObject
    {
        private Ticket ticket;

        public CheckInTicket(Ticket ticket)
        {
            this.ticket = ticket;
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

            String ticketHeadline = "IZLAZNA KARTA";


            String ticketTypeName = ticket.LicencePlates;

            Shared.Printer.AddHorizontalLine(0, 180);
            Shared.Printer.AddTextLine(ticketHeadline, 5, 2, 0, 210);
            Shared.Printer.AddTextLine(ticketTypeName, 5, 2, 0, 280);
            Shared.Printer.SetJustification("left");
            Shared.Printer.AddTextLine("Vrijeme ulaska: ", 0, 3, 40, 380);
            Shared.Printer.AddTextLine(ticket.CheckedIn.ToString(), 0, 3, 40, 420);
            Shared.Printer.AddTextLine("Oznaka vozila: ", 0, 3, 40, 470);
            Shared.Printer.SetJustification("center");
            Shared.Printer.AddTextLine(ticket.LicencePlates, 0, 3, 40, 520);
            Shared.Printer.SetJustification("left");
            Shared.Printer.AddTextLine("Vrsta vozila: " + ticket.VehicleType, 0, 3, 40, 570);
            Shared.Printer.AddBarCode(ticket.LicencePlates, 620);

            bool error = Shared.Printer.Print();

            return error;  
        }
    }
}

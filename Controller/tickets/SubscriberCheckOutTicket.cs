using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using Controller.printer;

namespace Controller.tickets
{
    public class SubscriberCheckOutTicket : IPrintObject
    {
        private Ticket ticket;

        public SubscriberCheckOutTicket(Ticket ticket)
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

            String ticketHeadline = "POTVRDA O BORAVKU";

            String ticketTypeName = ticket.LicencePlates.ToUpper();

            Shared.Printer.AddHorizontalLine(0, 180);
            Shared.Printer.AddTextLine(ticketHeadline, 5, 1, 0, 210);
            Shared.Printer.AddTextLine(ticketTypeName, 5, 2, 0, 280);
            Shared.Printer.SetJustification("left");
            Shared.Printer.AddTextLine("Vrsta karte: ", 0, 3, 40, 540);
            Shared.Printer.AddTextLine(ticket.TicketType, 0, 3, 40, 580);
            Shared.Printer.AddTextLine("Potvrda parkinga za: ", 0, 3, 40, 620);
            Shared.Printer.AddTextLine(ticket.LicencePlates, 0, 3, 40, 660);
            Shared.Printer.AddTextLine("u vremenu od: ", 0, 3, 40, 700);
            Shared.Printer.AddTextLine(ticket.CheckedIn.ToString() + " do:", 0, 3, 40, 740);
            Shared.Printer.AddTextLine(ticket.CheckedOut.ToString(), 0, 3, 40, 780);
            Shared.Printer.AddTextLine("Operater: ", 0, 3, 40, 820);
            Shared.Printer.AddTextLine(ticket.Username, 0, 3, 40, 860);

            bool error = Shared.Printer.Print();

            return error; 
        }
    }
}

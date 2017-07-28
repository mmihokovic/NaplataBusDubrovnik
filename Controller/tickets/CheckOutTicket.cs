using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using Controller.printer;

namespace Controller.tickets
{
    public class CheckOutTicket : IPrintObject
    {
        private Ticket ticket;
        private Company company;

        public Ticket Ticket
        {
            get { return ticket; }
            set { ticket = value; }
        }

        public CheckOutTicket(Ticket ticket, Company company)
        {
            this.ticket = ticket;
            this.company = company;
        }

        public bool Print()
        {
            Controller.Shared.Printer.SetJustification("center");
            Controller.Shared.Printer.AddTextLine("Luka Dubrovnik d.d.", 4, 0, 0, 10);
            Controller.Shared.Printer.AddTextLine("Obala pape", 5, 0, 0, 70);
            Controller.Shared.Printer.AddTextLine("Ivana Pavla II, 1", 5, 0, 0, 95);
            Controller.Shared.Printer.AddTextLine("20 000 Dubrovnik", 5, 0, 0, 120);
            Controller.Shared.Printer.AddTextLine("Telefon: +385 20 313 511", 5, 0, 0, 145);
            Controller.Shared.Printer.AddTextLine("OIB: 47148433806", 5, 0, 0, 170);

            String ticketHeadline = "RACUN " + Ticket.TicketCount.ToString() + " - 1 - 1";

            String ticketTypeName = Decimal.Round(ticket.Charged, 2).ToString("0.##") + " kn";

            Controller.Shared.Printer.AddHorizontalLine(0, 180);
            Controller.Shared.Printer.AddTextLine(ticket.TicketType, 5, 2, 0, 210);
            Controller.Shared.Printer.AddTextLine(ticketHeadline, 5, 2, 0, 280);
            Controller.Shared.Printer.AddTextLine(ticketTypeName, 5, 2, 0, 350);
            Controller.Shared.Printer.SetJustification("left");
            Controller.Shared.Printer.AddTextLine("Osnovica: ", 0, 3, 40, 450);
            Controller.Shared.Printer.AddTextLine(ticket.ChargedBase.ToString("0.##") + " kn", 0, 3, 40, 490);
            Controller.Shared.Printer.AddTextLine("PDV po stopi 25%: ", 0, 3, 40, 530);
            Controller.Shared.Printer.AddTextLine(ticket.ChargedTax.ToString("0.##") + " kn", 0, 3, 40, 570);
            Controller.Shared.Printer.AddTextLine("Naplata parkinga za ", 0, 3, 40, 610);
            Controller.Shared.Printer.AddTextLine(ticket.VehicleType.ToLower() + " :" 
                + ticket.LicencePlates, 0, 3, 40, 650);
            if (ticket.TicketType == "Pojedinačni prijevoz")
            {
                Controller.Shared.Printer.AddTextLine("u vremenu od: ", 0, 3, 40, 690);
                Controller.Shared.Printer.AddTextLine(ticket.CheckedIn.ToString() + " do:", 0, 3, 40, 730);
                Controller.Shared.Printer.AddTextLine(ticket.CheckedOut.ToString(), 0, 3, 40, 770);
            }
            else
            {
                Controller.Shared.Printer.AddTextLine("Vrijeme izdavanja: ", 0, 3, 40, 690);
                Controller.Shared.Printer.AddTextLine(ticket.CheckedIn.ToString(), 0, 3, 40, 730);
            }
            Controller.Shared.Printer.AddTextLine("Operater: ", 0, 3, 40, 810);
            Controller.Shared.Printer.AddTextLine(ticket.Username, 0, 3, 40, 850);
            Controller.Shared.Printer.AddTextLine("JIR: ", 0, 3, 40, 890);
            Controller.Shared.Printer.AddTextLine(ticket.JIR, 0, 0, 40, 930);
            Controller.Shared.Printer.AddTextLine("ZIK: ", 0, 3, 40, 970);
            Controller.Shared.Printer.AddTextLine(ticket.ZIK, 0, 0, 40, 1010);

            if (company != null)
            {
                Controller.Shared.Printer.AddHorizontalLine(0, 1020);
                Controller.Shared.Printer.AddTextLine("R1 racun za: ", 0, 3, 40, 1050);
                Controller.Shared.Printer.AddTextLine(company.Name + ",", 0, 3, 40, 1090);
                Controller.Shared.Printer.AddTextLine(company.Address + (String.IsNullOrEmpty(company.OIB) ? "" : ","), 0, 3, 40, 1130);
                if (!String.IsNullOrEmpty(company.OIB))
                {
                    Controller.Shared.Printer.AddTextLine(company.OIB, 0, 3, 40, 1170);
                }
            }

            bool error = Controller.Shared.Printer.Print();


            return error; 
        }
    }
}

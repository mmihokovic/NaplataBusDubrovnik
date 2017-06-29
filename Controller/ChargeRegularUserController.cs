using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using Controller.tickets;
using System.Net;
using Database;

namespace Controller
{
    public static class ChargeRegularUserController
    {
        public static List<VehicleType> GetAllVehicleTypes()
        {
            return Database.VehicleTypes.GetAllVehicleTypes();
        }

        public static List<TicketType> GetAllTicketTypes()
        {
            return Database.TicketTypes.GetAllTicketTypes();
        }

        public static void CheckIn(String licencePlates, String vehicleType, String ticketType)
        {
            if (ticketType != "Pojedinačni prijevoz")
            {
                Prepaid(licencePlates, vehicleType, ticketType);
            }

            Vehicle vehicle = VehicleController.CheckIn(licencePlates, vehicleType);
            if(vehicle == null)
            {
                throw new Exception("Ne postoji vozilo s oznakom " + licencePlates + " u bazi.");
            }
            Ticket ticket = new Ticket();
            ticket.LicencePlates = licencePlates;
            ticket.VehicleType = vehicleType;
            ticket.TicketType = ticketType;
            ticket.CheckedIn = vehicle.CheckedInDate;
            ticket.CheckedOut = vehicle.CheckedInDate;
            Database.Tickets.AddTicket(ticket);
            CheckInTicket checkInTicket = new CheckInTicket(ticket);
            Shared.Worker.Print(checkInTicket);
        }

        public static int GetRecipientCount()
        {
            return CounterController.GetTicketCount();
        }

        public static void Prepaid(String licencePlates, String vehicleType, String ticketType)
        {
            if (!Controller.ChargeRegularUserController.HasInternet())
            {
                throw new Exception("Uređaj nije spojen na Internet. Računi se ne mogu fiskalizirati.");
            }

            decimal chargeBase = 0;
            decimal chargeTax = 0;

            Vehicle vehicle = VehicleController.CheckIn(licencePlates, vehicleType);
            Ticket ticket = new Ticket();
            ticket.LicencePlates = licencePlates;
            ticket.VehicleType = vehicleType;
            ticket.TicketType = ticketType;
            ticket.CheckedIn = vehicle.CheckedInDate;
            ticket.CheckedOut = vehicle.CheckedInDate;
            ticket.Username = LoginController.LoggedInUser.FirstName + " " + LoginController.LoggedInUser.LastName;
            String JIR;
            String ZIK;
            ticket.Charged = ChargesController.GetCharge(ticket.TicketType, vehicle.VehicleType, out chargeBase, out chargeTax);

            if (!Shared.TestMode)
            {
                ChargesController.Fiskaliziraj(vehicle.LicencePlates, vehicle.CheckedInDate, LoginController.LoggedInUser.OIB, ticket.Charged, chargeBase, chargeTax, out ZIK, out JIR);                
            }
            else
            {
                JIR = "550e8400-e29b-41d4-a716-446655440000";
                ZIK = "e4d909c290d0fb1ca068ffaddf22cbd0";
            }
            ticket.ChargedBase = chargeBase;
            ticket.ChargedTax = chargeTax;
            ticket.JIR = JIR;
            ticket.ZIK = ZIK;
            ticket.ChargeTicket = true;
            Database.Tickets.AddTicket(ticket);
            ticket.TicketCount = CounterController.GetTicketCount();

            CheckOutTicket checkOutTicket = new CheckOutTicket(ticket);
            Shared.Worker.Print(checkOutTicket);
            Shared.Worker.Print(checkOutTicket);
            Tickets.AddLogOutTicket(LoginController.LoggedInUser.Username,
                checkOutTicket.Ticket.TicketType, checkOutTicket.Ticket.VehicleType,
                checkOutTicket.Ticket.LicencePlates, checkOutTicket.Ticket.Charged);
            CounterController.IncrementCounter();
        }

        public static void CheckOut(String licencePlate)
        {
            if (!Controller.ChargeRegularUserController.HasInternet())
            {
                throw new Exception("Uređaj nije spojen na Internet. Računi se ne mogu fiskalizirati.");
            }

            Vehicle vehicle = VehicleController.CheckOut(licencePlate);
            if (vehicle == null)
            {
                throw new Exception("Vozilo s oznakom " + licencePlate + " nije prijavljeno prilikom ulaska na parkiralište.");
            }
            decimal chargeBase = 0;
            decimal chargeTax = 0;
            Ticket ticket = Database.Tickets.GetTicket(vehicle.LicencePlates, vehicle.VehicleType, vehicle.CheckedInDate, false);
            if (ticket == null || ticket.ChargeTicket)
            {
                throw new Exception("Vozilo s oznakom " + licencePlate + " nije prijavljeno prilikom ulaska na parking.");
            }
            if (ticket.TicketType != "Pojedinačni prijevoz")
            {
                throw new Exception("Izlazna karta se naplačuje samo vozilima s pojedinačnom kartom");
            }
            ticket.CheckedOut = DateTime.Now;
            ticket.Username = LoginController.LoggedInUser.FirstName + " " + LoginController.LoggedInUser.LastName;
            String JIR;
            String ZIK;
            ticket.Charged = ChargesController.GetCharge(ticket.TicketType, vehicle.VehicleType, vehicle.CheckedInDate, out chargeBase, out chargeTax);
            decimal dailyTicketCharge = ChargesController.GetCharge("Dnevna karta", vehicle.VehicleType);
            
            if (ticket.TicketType == "Pojedinačni prijevoz" && ticket.Charged > dailyTicketCharge)
            {
                ticket.TicketType = "Dnevna karta";
                ticket.Charged = ChargesController.GetCharge(ticket.TicketType, 
                    vehicle.VehicleType, out chargeBase, out chargeTax);
            }
            
            //porezna
            if (!Shared.TestMode)
            {
                ChargesController.Fiskaliziraj(vehicle.LicencePlates, vehicle.CheckedInDate, "12345678901", ticket.Charged, chargeBase, chargeTax, out ZIK, out JIR);
            }
            else
            {
                JIR = "550e8400-e29b-41d4-a716-446655440000";
                ZIK = "e4d909c290d0fb1ca068ffaddf22cbd0";
            }
            ticket.ChargedBase = chargeBase;
            ticket.ChargedTax = chargeTax;
            ticket.JIR = JIR;
            ticket.ZIK = ZIK;
            ticket.ChargeTicket = true;
            Database.Tickets.UpdateTicket(ticket);
            ticket.TicketCount = CounterController.GetTicketCount();

            CheckOutTicket checkOutTicket = new CheckOutTicket(ticket);
            Shared.Worker.Print(checkOutTicket);
            Shared.Worker.Print(checkOutTicket);
            Tickets.AddLogOutTicket(LoginController.LoggedInUser.Username, 
                checkOutTicket.Ticket.TicketType, checkOutTicket.Ticket.VehicleType, 
                checkOutTicket.Ticket.LicencePlates, checkOutTicket.Ticket.Charged);
            CounterController.IncrementCounter();
        }

        public static bool HasInternet()
        {
            if (Shared.TestMode)
            {
                return true;
            }
            bool connected = false;
            HttpWebRequest request;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create("http://www.provjeri-racun.hr/provjeraracuna");
                response = (HttpWebResponse)request.GetResponse();
                request.Abort();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    connected = true;
                }
            }
            catch (WebException ex)
            {
                Logger.Logger.Log(ex);
                connected = false;
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                connected = false;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
            return connected;
        }
    }
}

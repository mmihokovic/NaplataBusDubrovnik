using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using Controller.tickets;
using Database;

namespace Controller
{
    public static class ChargeSubscriberController
    {
        public static void CheckIn(string licencePlate){
            Subscriber subscriber = Controller.SubscriberController.GetSubscriber(licencePlate);
            if (subscriber == null)
            {
                throw new Exception("Ne postoji pretplatnik s oznakom " + licencePlate + " u bazi.");
            }
            Vehicle vehicle = VehicleController.CheckIn(subscriber.LicencePlates);
            if (vehicle == null)
            {
                throw new Exception("Ne postoji vozilo s oznakom " + licencePlate + " u bazi.");
            }
            Ticket ticket = new Ticket();
            ticket.LicencePlates = vehicle.LicencePlates;
            ticket.VehicleType = vehicle.VehicleType;
            ticket.CheckedIn = vehicle.CheckedInDate;
            ticket.CheckedOut = vehicle.CheckedInDate;
            ticket.TicketType = "Pretplatnička karta";
            Database.Tickets.AddTicket(ticket);
            SubscriberCheckInTicket checkInTicket = new SubscriberCheckInTicket(ticket);
            Shared.Worker.Print(checkInTicket);
        }

        public static void CheckOut(string licencePlate)
        {
            Vehicle vehicle = VehicleController.CheckOut(licencePlate);
            if (vehicle == null)
            {
                throw new Exception("Ne postoji vozilo s oznakom " + licencePlate + " u bazi.");
            }
            Ticket ticket = Database.Tickets.GetTicket(vehicle.LicencePlates, vehicle.VehicleType, vehicle.CheckedInDate, false);
            if (ticket == null)
            {
                throw new Exception("Vozilo s oznakom " + licencePlate + " nije prijavljeno prilikom ulaska na parking.");
            }
            ticket.CheckedOut = DateTime.Now;
            ticket.Username = LoginController.LoggedInUser.FirstName + " " + LoginController.LoggedInUser.LastName;
            ticket.ChargedBase = 0;
            ticket.ChargedTax = 0;
            ticket.JIR = "";
            ticket.ZIK = "";
            ticket.ChargeTicket = true;
            SubscriberCheckOutTicket checkOutTicket = new SubscriberCheckOutTicket(ticket);
            Database.Tickets.UpdateTicket(ticket);
            Shared.Worker.Print(checkOutTicket);
        }

        public static void ChargeSubscription(String licencePlate, int months, DateTime validTo, Company company)
        {
             if (!Controller.ChargeRegularUserController.HasInternet())
            {
                throw new Exception("Uređaj nije spojen na Internet. Računi se ne mogu fiskalizirati.");
            }

            Vehicle vehicle = Database.Vehicles.GetVehicle(licencePlate);
            if (vehicle == null)
            {
                throw new Exception("Ne postoji vozilo s oznakom " + licencePlate + " u bazi.");
            }
            decimal chargeBase = 0;
            decimal chargeTax = 0;
            
      
            String username = LoginController.LoggedInUser.FirstName + " " + LoginController.LoggedInUser.LastName;
            String JIR;
            String ZIK;
            decimal charged = ChargesController.GetCharge("Pretplatnička karta", vehicle.VehicleType, out chargeBase, out chargeTax) * months;
            
            if (!Shared.TestMode)
            {
                 ChargesController.Fiskaliziraj(vehicle.LicencePlates, vehicle.CheckedInDate, "12345678901", charged, chargeBase, chargeTax, out ZIK, out JIR);
            }
            else
            {
                JIR = "550e8400-e29b-41d4-a716-446655440000";
                ZIK = "e4d909c290d0fb1ca068ffaddf22cbd0";
            }
            SubscriberAddTicket addSubscriberTicket = new SubscriberAddTicket(licencePlate, vehicle.VehicleType,
                months, validTo, charged, chargeTax, chargeBase, username, JIR, ZIK, CounterController.GetTicketCount(), company);

            

            Shared.Worker.Print(addSubscriberTicket);
            Shared.Worker.Print(addSubscriberTicket);
            Tickets.AddLogOutTicket(LoginController.LoggedInUser.Username, 
                "Pretplatnička karta", addSubscriberTicket.VehicleType, 
                addSubscriberTicket.LicencePlate, addSubscriberTicket.Charged);
            CounterController.IncrementCounter();
        }
    }
}

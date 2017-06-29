using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public static class VehicleController
    {
        public static Vehicle AddVehicle(string licencePlates, string vehicleType)
        {
            Vehicle vehicle = Database.Vehicles.GetVehicle(licencePlates);
            if (vehicle == null)
            {
                vehicle = Database.Vehicles.AddVehicle(licencePlates, vehicleType, true, DateTime.Now);
            }
            return vehicle;
        }

        public static Vehicle CheckIn(string licencePlates, string vehicleType)
        {
            Vehicle vehicle = AddVehicle(licencePlates, vehicleType);
            vehicle.CheckedIn = true;
            vehicle.CheckedInDate = DateTime.Now;
            if(!Database.Vehicles.UpdateVehicle(vehicle)){
                return null;
            }
            return vehicle;
        }

        public static Vehicle CheckIn(string licencePlates)
        {
            Vehicle vehicle = Database.Vehicles.GetVehicle(licencePlates);
            if (vehicle == null)
            {
                return null;
            }
            vehicle.CheckedIn = true;
            vehicle.CheckedInDate = DateTime.Now;
            if (!Database.Vehicles.UpdateVehicle(vehicle))
            {
                return null;
            }
            return vehicle;
        }

        public static Vehicle CheckOut(string licencePlates)
        {
            Vehicle vehicle = Database.Vehicles.GetVehicle(licencePlates);
            if (vehicle == null)
            {
                return null;
            }
            vehicle.CheckedIn = false;
            Database.Vehicles.UpdateVehicle(vehicle);
            return vehicle;
        }
    }
}

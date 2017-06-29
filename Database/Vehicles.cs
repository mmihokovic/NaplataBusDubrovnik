using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data.SqlServerCe;
using System.Data;

namespace Database
{
    public static class Vehicles
    {


        static Vehicles()
        {

        }

        public static Vehicle GetVehicle(String licencePlates)
        {
            if (licencePlates == null || licencePlates.Length == 0)
            {
                return null;
            }

            string sql = "select licencePlates, vehicleType, checkedIn, checkedInDate from vehicle where licencePlates=@licencePlates";
            Vehicle vehicle = null;
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.Parameters.AddWithValue("@licencePlates", licencePlates);
            cmd.CommandType = CommandType.Text;
            SqlCeResultSet rs = cmd.ExecuteResultSet(
            ResultSetOptions.Scrollable);
            if (rs.HasRows)
            {
                int ordLicencePlates = rs.GetOrdinal("licencePlates");
                int ordVehicleType = rs.GetOrdinal("vehicleType");
                int ordCheckedIn = rs.GetOrdinal("checkedIn");
                int ordCheckedInDate = rs.GetOrdinal("checkedInDate");

                rs.ReadFirst();
                vehicle = new Vehicle();
                vehicle.LicencePlates = rs.GetString(ordLicencePlates);
                vehicle.VehicleType = rs.GetString(ordVehicleType);
                vehicle.CheckedIn = rs.GetBoolean(ordCheckedIn);
                vehicle.CheckedInDate = rs.GetDateTime(ordCheckedInDate);
            }
            return vehicle;
        }

        public static Vehicle AddVehicle(String licencePlates, String vehicleType, bool checkedIn, DateTime checkInDate)
        {
            string sql = "insert into vehicle "
                    + "(licencePlates, vehicleType, checkedIn, checkedInDate) "
                    + "values (@licencePlates, @vehicleType, @checkedIn, @checkedInDate)";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@licencePlates", licencePlates);
                cmd.Parameters.AddWithValue("@vehicleType", vehicleType);
                cmd.Parameters.AddWithValue("@checkedIn", checkedIn);
                cmd.Parameters.AddWithValue("@checkedInDate", checkInDate);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();
            return new Vehicle() { LicencePlates = licencePlates, VehicleType = vehicleType, CheckedIn = checkedIn, CheckedInDate = checkInDate };
        }

        public static bool UpdateVehicle(Vehicle vehicle)
        {
            string sql = "update vehicle "
          + "set licencePlates=@licencePlates, vehicleType=@vehicleType, checkedIn=@checkedIn, checkedInDate=@checkedInDate "
          + "where licencePlates=@licencePlates";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@licencePlates", vehicle.LicencePlates);
                cmd.Parameters.AddWithValue("@vehicleType", vehicle.VehicleType);
                cmd.Parameters.AddWithValue("@checkedIn", vehicle.CheckedIn);
                cmd.Parameters.AddWithValue("@checkedInDate", vehicle.CheckedInDate);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                return false;
            }
            DatabaseConnector.Disconnect();
            return true;
        }
    }
}

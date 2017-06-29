using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data.SqlServerCe;
using System.Data;

namespace Database
{
    public static class VehicleTypes
    {
        static VehicleTypes()
        {
            if (GetAllVehicleTypes().Count == 0)
            {
                AddVehicleType("Kamion");
                AddVehicleType("osobno-v");
                AddVehicleType("Bus");
            }
        }

        public static void AddVehicleType(String type)
        {
            string sql = "insert into vehicleType "
            + "(type) "
            + "values (@type)";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();
        }

        public static List<VehicleType> GetAllVehicleTypes()
        {
            List<VehicleType> vehicleTypes = new List<VehicleType>();
            string sql = "select type from vehicleType";

            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.CommandType = CommandType.Text;
            SqlCeResultSet rs = cmd.ExecuteResultSet(
            ResultSetOptions.Scrollable);
            if (rs.HasRows)
            {
                int ordType = rs.GetOrdinal("type");
  
                rs.ReadFirst();
                VehicleType vehicleType = new VehicleType();
                vehicleType.Type = rs.GetString(ordType);
                vehicleTypes.Add(vehicleType);

                while (rs.Read())
                {
                    vehicleType = new VehicleType();
                    vehicleType.Type = rs.GetString(ordType);
                    vehicleTypes.Add(vehicleType);
                }
            }
            return vehicleTypes;
        }

        public static VehicleType GetVehicleType(String vehicleType)
        {
            if (vehicleType == null || vehicleType.Length == 0)
            {
                return null;
            }

            string sql = "select type from vehicleType where type=@vehicleType";
            VehicleType type = null;
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.Parameters.AddWithValue("@vehicleType", vehicleType);
            cmd.CommandType = CommandType.Text;
            SqlCeResultSet rs = cmd.ExecuteResultSet(
            ResultSetOptions.Scrollable);
            if (rs.HasRows)
            {
                int ordType = rs.GetOrdinal("type");

                rs.ReadFirst();
                type = new VehicleType();
                type.Type = rs.GetString(ordType);
            }
            return type;
        }
    }
}

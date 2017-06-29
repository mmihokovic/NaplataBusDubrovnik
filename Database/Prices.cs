using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data.SqlServerCe;
using System.Data;

namespace Database
{
    public class Prices
    {
        static Prices()
        {

        }

        public static Price AddPrice(decimal charge, bool summerTariff, string ticketType, string vehicleType)
        {
            string sql = "insert into price "
                + "(charge, summerTariff, ticketType, vehicleType) "
                + "values (@charge, @summerTariff, @ticketType, @vehicleType)";

            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@charge", charge);
                cmd.Parameters.AddWithValue("@summerTariff", summerTariff);
                cmd.Parameters.AddWithValue("@ticketType", ticketType);
                cmd.Parameters.AddWithValue("@vehicleType", vehicleType);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();
            return new Price() { Charge = charge, SummerTariff = summerTariff, TicketType = ticketType, VehicleType = vehicleType };
        }

        public static Price UpdatePrice(decimal charge, bool summerTariff, string ticketType, string vehicleType)
        {
            string sql = "update price "
         + "set charge=@charge, SummerTariff=@SummerTariff, ticketType=@ticketType, vehicleType=@vehicleType "
         + "where SummerTariff=@SummerTariff and ticketType=@ticketType and vehicleType=@vehicleType";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@charge", charge);
                cmd.Parameters.AddWithValue("@SummerTariff", summerTariff);
                cmd.Parameters.AddWithValue("@ticketType", ticketType);
                cmd.Parameters.AddWithValue("@vehicleType", vehicleType);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                return null;
            }
            DatabaseConnector.Disconnect();
            return new Price() { Charge = charge, SummerTariff = summerTariff, TicketType = ticketType, VehicleType = vehicleType };
        }

        public static void UpdatePrice(Price price)
        {
            UpdatePrice(price.Charge, price.SummerTariff, price.TicketType, price.VehicleType);
        }

        public static Price GetPrice(bool summerTariff, string ticketType, string vehicleType)
        {
            if (ticketType == null || ticketType.Length == 0 || vehicleType == null || vehicleType.Length == 0)
            {
                return null;
            }

            string sql = "select charge, summerTariff, ticketType, vehicleType from price where summerTariff=@summerTariff and ticketType=@ticketType and vehicleType=@vehicleType";
            Price price = null;
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.Parameters.AddWithValue("@summerTariff", summerTariff);
            cmd.Parameters.AddWithValue("@ticketType", ticketType);
            cmd.Parameters.AddWithValue("@vehicleType", vehicleType);
            cmd.CommandType = CommandType.Text;
            SqlCeResultSet rs = cmd.ExecuteResultSet(
            ResultSetOptions.Scrollable);
            if (rs.HasRows)
            {
                int ordCharge = rs.GetOrdinal("charge");
                int ordSummerTariff = rs.GetOrdinal("summerTariff");
                int ordTicketType = rs.GetOrdinal("ticketType");
                int ordVehicleType = rs.GetOrdinal("vehicleType");

                rs.ReadFirst();
                price = new Price();
                price.Charge = rs.GetDecimal(ordCharge);
                price.SummerTariff = rs.GetBoolean(ordSummerTariff);
                price.TicketType = rs.GetString(ordTicketType);
                price.VehicleType = rs.GetString(ordVehicleType);
            }
            return price;
        }
    }
}

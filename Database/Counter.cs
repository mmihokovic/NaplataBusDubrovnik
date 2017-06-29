using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace Database
{
    public static class Counter
    {
        public static int GetTicketCount()
        {
            string sql = "select * from counter";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);


                 SqlCeResultSet rs = cmd.ExecuteResultSet(
            ResultSetOptions.Scrollable);
                 if (rs.HasRows)
                 {
                     int ordticketCounter = rs.GetOrdinal("ticketCounter");
                     rs.ReadFirst();
                     int count = rs.GetInt32(ordticketCounter);
                     return count;
                 }
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            return -1;
        }

        public static void SetTicketCount(int ticketCountValue)
        {
            string sql = "update counter "
        + "set ticketCounter=@ticketCounter";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@ticketCounter", ticketCountValue);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();
        }

        public static void AddTicketValueCouner()
        {
            string sql = "insert into counter "
               + "(ticketCounter)"
           + "values (1)";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();

        }
    }
}

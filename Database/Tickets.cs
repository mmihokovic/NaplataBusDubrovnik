using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using Model;
using System.Data;

namespace Database
{
    public static class Tickets
    {
        static Tickets()
        {

        }

        public static void AddTicket(Ticket ticket)
        {
            string sql = "insert into ticket "
                + "(licencePlates, TicketType, vehicleType, username, checkedIn, checkedOut, charged, chargedBase, chargedTax, JIR, ZIK, ChargeTicket  )"
            +"values (@licencePlates, @TicketType, @vehicleType, @username, @checkedIn, @checkedOut, @charged, @chargedBase, @chargedTax, @JIR, @ZIK, @ChargeTicket)";

            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@licencePlates", ticket.LicencePlates);
                cmd.Parameters.AddWithValue("@TicketType", ticket.TicketType);
                cmd.Parameters.AddWithValue("@vehicleType", ticket.VehicleType);
                cmd.Parameters.AddWithValue("@username", ticket.Username != null ? ticket.Username : "");
                cmd.Parameters.AddWithValue("@checkedIn", ticket.CheckedIn);
                cmd.Parameters.AddWithValue("@checkedOut", ticket.CheckedOut);
                cmd.Parameters.AddWithValue("@charged", ticket.Charged);
                cmd.Parameters.AddWithValue("@chargedBase", ticket.ChargedBase);
                cmd.Parameters.AddWithValue("@chargedTax", ticket.ChargedTax);
                cmd.Parameters.AddWithValue("@JIR", ticket.JIR != null ? ticket.JIR : "");
                cmd.Parameters.AddWithValue("@ZIK", ticket.ZIK != null ? ticket.ZIK : "");
                cmd.Parameters.AddWithValue("@ChargeTicket", ticket.ChargeTicket);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();
        }

        public static int TicketCount()
        {
            string sql = "select count(*) from ticket";
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.CommandType = CommandType.Text;
            int count = (int)cmd.ExecuteScalar();
            return count;
        }

        public static Ticket GetTicket(string licencePlates, string vehicleType, DateTime checkedIn, bool chargeTicket)
        {
            string sql = "select licencePlates, TicketType, vehicleType, username, checkedIn, checkedOut, charged, chargedBase, chargedTax,  JIR, ZIK, ChargeTicket from ticket where licencePlates=@licencePlates and vehicleType=@vehicleType and checkedIn=@checkedIn and chargeTicket=@chargeTicket";
            Ticket ticket = null;
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.Parameters.AddWithValue("@licencePlates", licencePlates);
            cmd.Parameters.AddWithValue("@vehicleType", vehicleType);
            cmd.Parameters.AddWithValue("@checkedIn", checkedIn);
            cmd.Parameters.AddWithValue("@ChargeTicket", chargeTicket);
            cmd.CommandType = CommandType.Text;
            SqlCeResultSet rs = cmd.ExecuteResultSet(
            ResultSetOptions.Scrollable);
            if (rs.HasRows)
            {
                int ordLicencePlates = rs.GetOrdinal("licencePlates");
                int ordTicketType = rs.GetOrdinal("TicketType");
                int ordVehicleType = rs.GetOrdinal("vehicleType");
                int ordUsername = rs.GetOrdinal("username");
                int ordCheckedIn = rs.GetOrdinal("checkedIn");
                int ordCheckedOut = rs.GetOrdinal("checkedOut");
                int ordCharged = rs.GetOrdinal("charged");
                int ordChargedBase = rs.GetOrdinal("chargedBase");
                int ordChargedTax = rs.GetOrdinal("chargedTax");
                int ordJIR = rs.GetOrdinal("JIR");
                int ordZIK = rs.GetOrdinal("ZIK");
                int ordChargeTicket = rs.GetOrdinal("ChargeTicket");

                rs.ReadFirst();
                ticket = new Ticket();
                ticket.LicencePlates = rs.GetString(ordLicencePlates);
                ticket.TicketType = rs.GetString(ordTicketType);
                ticket.VehicleType = rs.GetString(ordVehicleType);
                ticket.Username = rs.GetString(ordUsername);
                ticket.CheckedIn = rs.GetDateTime(ordCheckedIn);
                ticket.CheckedOut = rs.GetDateTime(ordCheckedOut);
                ticket.Charged = rs.GetDecimal(ordCharged);
                ticket.ChargedBase = rs.GetDecimal(ordChargedBase);
                ticket.ChargedTax = rs.GetDecimal(ordChargedTax);
                ticket.JIR = rs.GetString(ordJIR);
                ticket.ZIK = rs.GetString(ordZIK);
                ticket.ChargeTicket = rs.GetBoolean(ordChargeTicket);
            }
            return ticket;
        }

        public static void UpdateTicket(Ticket ticket){
            string sql = "update ticket "
        + "set licencePlates=@licencePlates, TicketType=@TicketType, vehicleType=@vehicleType, username=@username, checkedIn=@checkedIn, checkedOut=@checkedOut, charged=@charged, chargedBase=@chargedBase, chargedTax=@chargedTax, JIR=@JIR, ZIK=@ZIK, ChargeTicket=@ChargeTicket "
            + "where licencePlates=@licencePlates and ticketType=@TicketType and vehicleType=@vehicleType and checkedIn=@checkedIn";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@licencePlates", ticket.LicencePlates);
                cmd.Parameters.AddWithValue("@TicketType", ticket.TicketType);
                cmd.Parameters.AddWithValue("@vehicleType", ticket.VehicleType);
                cmd.Parameters.AddWithValue("@username", ticket.Username);
                cmd.Parameters.AddWithValue("@checkedIn", ticket.CheckedIn);
                cmd.Parameters.AddWithValue("@checkedOut", ticket.CheckedOut);
                cmd.Parameters.AddWithValue("@charged", ticket.Charged);
                cmd.Parameters.AddWithValue("@chargedBase", ticket.ChargedBase);
                cmd.Parameters.AddWithValue("@chargedTax", ticket.ChargedTax);
                cmd.Parameters.AddWithValue("@JIR", ticket.JIR);
                cmd.Parameters.AddWithValue("@ZIK", ticket.ZIK);
                cmd.Parameters.AddWithValue("@ChargeTicket", ticket.ChargeTicket);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();
        }

        public static void AddLogOutTicket(string username, string ticketType, string vehicleType, string licencePlates, Decimal charged)
        {
            string commandText = "insert into chargedTicket (username, ticketType, vehicleType, licencePlates, charged) values (@username, @ticketType, @vehicleType, @licencePlates, @charged)";
            try
            {
                SqlCeCommand sqlCeCommand = new SqlCeCommand(commandText, DatabaseConnector.DatabaseConnection);
                sqlCeCommand.Parameters.AddWithValue("@username", (object)username);
                sqlCeCommand.Parameters.AddWithValue("@ticketType", (object)ticketType);
                sqlCeCommand.Parameters.AddWithValue("@vehicleType", (object)vehicleType);
                sqlCeCommand.Parameters.AddWithValue("@licencePlates", (object)licencePlates);
                sqlCeCommand.Parameters.AddWithValue("@charged", (object)Decimal.Round(charged, 8));
                sqlCeCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();
        }

        public static void UpdateLogOutTicket(string username, string ticketType, string vehicleType, string licencePlates, Decimal charged)
        {
            string commandText = "update chargedTicket set username=@username, ticketType=@ticketType, vehicleType=@vehicleType, licencePlates=@licencePlates, charged=@charged where username=@username and ticketType=@ticketType and vehicleType=@vehicleType";
            try
            {
                SqlCeCommand sqlCeCommand = new SqlCeCommand(commandText, DatabaseConnector.DatabaseConnection);
                sqlCeCommand.Parameters.AddWithValue("@username", (object)username);
                sqlCeCommand.Parameters.AddWithValue("@ticketType", (object)ticketType);
                sqlCeCommand.Parameters.AddWithValue("@vehicleType", (object)vehicleType);
                sqlCeCommand.Parameters.AddWithValue("@licencePlates", (object)licencePlates);
                sqlCeCommand.Parameters.AddWithValue("@charged", (object)Decimal.Round(charged, 8));
                sqlCeCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();
        }

        public static ChargedTicket GetChargedTicket(string username, string ticketType, string vehicleType)
    {
      string commandText = "select username, ticketType, vehicleType, licencePlates, charged from chargedTicket where username=@username and ticketType=@ticketType and vehicleType=@vehicleType";
      ChargedTicket chargedTicket = (ChargedTicket) null;
      try
      {
        SqlCeCommand sqlCeCommand = new SqlCeCommand(commandText, DatabaseConnector.DatabaseConnection);
        sqlCeCommand.Parameters.AddWithValue("@username", (object) username);
        sqlCeCommand.Parameters.AddWithValue("@ticketType", (object) ticketType);
        sqlCeCommand.Parameters.AddWithValue("@vehicleType", (object) vehicleType);
        sqlCeCommand.CommandType = CommandType.Text;
        SqlCeResultSet sqlCeResultSet = sqlCeCommand.ExecuteResultSet(ResultSetOptions.Scrollable);
        if (sqlCeResultSet.HasRows)
        {
          int ordinal1 = sqlCeResultSet.GetOrdinal("username");
          int ordinal2 = sqlCeResultSet.GetOrdinal("ticketType");
          int ordinal3 = sqlCeResultSet.GetOrdinal("vehicleType");
          int ordinal4 = sqlCeResultSet.GetOrdinal("licencePlates");
          int ordinal5 = sqlCeResultSet.GetOrdinal("charged");
          sqlCeResultSet.ReadFirst();
          chargedTicket = new ChargedTicket();
          chargedTicket.Username = sqlCeResultSet.GetString(ordinal1);
          chargedTicket.TicketType = sqlCeResultSet.GetString(ordinal2);
          chargedTicket.VehicleType = sqlCeResultSet.GetString(ordinal3);
          chargedTicket.LicencePlates = sqlCeResultSet.GetString(ordinal4);
          chargedTicket.Charged = sqlCeResultSet.GetDecimal(ordinal5);
        }
      }
      catch(Exception e)
      {
          Logger.Logger.Log(e);
      }
      return chargedTicket;
    }

    public static List<ChargedTicket> GetAllChargedTickets(string username)
    {
      List<ChargedTicket> list = new List<ChargedTicket>();
      SqlCeCommand sqlCeCommand = new SqlCeCommand("select * from chargedTicket where username=@username", DatabaseConnector.DatabaseConnection);
      sqlCeCommand.CommandType = CommandType.Text;
      sqlCeCommand.Parameters.AddWithValue("@username", username);
      SqlCeResultSet sqlCeResultSet = sqlCeCommand.ExecuteResultSet(ResultSetOptions.Scrollable);
      if (sqlCeResultSet.HasRows)
      {
        int ordinal1 = sqlCeResultSet.GetOrdinal("username");
        int ordinal2 = sqlCeResultSet.GetOrdinal("ticketType");
        int ordinal3 = sqlCeResultSet.GetOrdinal("vehicleType");
        int ordinal4 = sqlCeResultSet.GetOrdinal("licencePlates");
        int ordinal5 = sqlCeResultSet.GetOrdinal("charged");
        sqlCeResultSet.ReadFirst();
        list.Add(new ChargedTicket()
        {
          Username = sqlCeResultSet.GetString(ordinal1),
          TicketType = sqlCeResultSet.GetString(ordinal2),
          VehicleType = sqlCeResultSet.GetString(ordinal3),
          LicencePlates = sqlCeResultSet.GetString(ordinal4),
          Charged = sqlCeResultSet.GetDecimal(ordinal5)
        });
        while (sqlCeResultSet.Read())
          list.Add(new ChargedTicket()
          {
            Username = sqlCeResultSet.GetString(ordinal1),
            TicketType = sqlCeResultSet.GetString(ordinal2),
            VehicleType = sqlCeResultSet.GetString(ordinal3),
            LicencePlates = sqlCeResultSet.GetString(ordinal4),
            Charged = sqlCeResultSet.GetDecimal(ordinal5)
          });
      }
      return list;
    }

    public static void RemoveChargedTickets(string username)
    {
      SqlCeCommand sqlCeCommand = new SqlCeCommand("delete from chargedTicket where username=@username", DatabaseConnector.DatabaseConnection);
      sqlCeCommand.Parameters.AddWithValue("@username", (object) username);
      sqlCeCommand.ExecuteNonQuery();
      DatabaseConnector.Disconnect();
    }
    }
}

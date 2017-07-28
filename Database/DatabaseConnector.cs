using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.IO;

namespace Database
{
    public static class DatabaseConnector
    {
        static SqlCeConnection databaseConnection;
        static String connectionString;

        static DatabaseConnector()
        {
            CreateDatabase();
            CreateCompanyTable();
        }

        public static void CreateDatabase()
        {
            
            string fileName = "Database1.sdf";
            string password = "arcanecode";

            connectionString = string.Format(
            "DataSource=\"{0}\"; Password='{1}'", fileName, password);

            if (File.Exists(fileName))
            {
                return;
            }

            SqlCeEngine en = new SqlCeEngine(connectionString);
            en.CreateDatabase();

            CreateUserTable();
            Users.AddUser("user", "", "Ivo", "Peric", "Operater", "12345678901");
            Users.AddUser("Administrator", "", "Administrator", "Administrator", "Administrator", "");

            CreateVehicleTypeTable();

            CreateVehicleTable();

            CreatePriceTable();
            Prices.AddPrice(50, true, "Pojedinačni prijevoz", "Kamion");
            Prices.AddPrice(20, true, "Pojedinačni prijevoz", "osobno-v");
            Prices.AddPrice(50, true, "Pojedinačni prijevoz", "Bus");
            Prices.AddPrice(300, true, "Dnevna karta", "Kamion");
            Prices.AddPrice(200, true, "Dnevna karta", "osobno-v");
            Prices.AddPrice(300, true, "Dnevna karta", "Bus");
            Prices.AddPrice(1500, true, "Tjedna karta", "Kamion");
            Prices.AddPrice(1000, true, "Tjedna karta", "osobno-v");
            Prices.AddPrice(1500, true, "Tjedna karta", "Bus");
            Prices.AddPrice(1500, true, "Mjesečna karta", "Kamion");
            Prices.AddPrice(1000, true, "Mjesečna karta", "osobno-v");
            Prices.AddPrice(1500, true, "Mjesečna karta", "Bus");
            Prices.AddPrice(50, false, "Pojedinačni prijevoz", "Kamion");
            Prices.AddPrice(20, false, "Pojedinačni prijevoz", "osobno-v");
            Prices.AddPrice(50, false, "Pojedinačni prijevoz", "Bus");
            Prices.AddPrice(300, false, "Dnevna karta", "Kamion");
            Prices.AddPrice(200, false, "Dnevna karta", "osobno-v");
            Prices.AddPrice(300, false, "Dnevna karta", "Bus");
            Prices.AddPrice(1500, false, "Tjedna karta", "Kamion");
            Prices.AddPrice(1000, false, "Tjedna karta", "osobno-v");
            Prices.AddPrice(1500, false, "Tjedna karta", "Bus");
            Prices.AddPrice(1500, false, "Mjesečna karta", "Kamion");
            Prices.AddPrice(1000, false, "Mjesečna karta", "osobno-v");
            Prices.AddPrice(1500, false, "Mjesečna karta", "Bus");
            Prices.AddPrice(1000, false, "Pretplatnička karta", "Kamion");
            Prices.AddPrice(1000, false, "Pretplatnička karta", "osobno-v");
            Prices.AddPrice(1000, false, "Pretplatnička karta", "Bus");
            Prices.AddPrice(1000, true, "Pretplatnička karta", "Kamion");
            Prices.AddPrice(1000, true, "Pretplatnička karta", "osobno-v");
            Prices.AddPrice(1000, true, "Pretplatnička karta", "Bus");
            CreateTicketTable();

            CreateSubscriberTable();

            CreateCounterTable();
            Counter.AddTicketValueCouner();
            Counter.SetTicketCount(93);
            CreateChargedTicketTable();
        }

        public static void CreateCounterTable()
        {
            Connect();
            string sql = "create table counter ("
                + "ticketCounter integer)";
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException sqlexception)
            {
                Logger.Logger.Log(sqlexception);
            }
            databaseConnection.Close();
        }

        public static void CreateUserTable()
        {
            Connect();
            string sql = "create table users ("
             + "Username nvarchar (20) not null, "
             + "Password nvarchar (20), "
             + "FirstName nvarchar (40), " 
             + "LastName nvarchar (40), " 
             + "UserType nvarchar (20), "
             + "oib nvarchar(13))";

            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException sqlexception)
            {
                Logger.Logger.Log(sqlexception);
            }
            databaseConnection.Close();
        }

        public static void CreateVehicleTypeTable()
        {
            Connect();
            string sql = "create table vehicleType ("
             + "type nvarchar (20) not null) ";

            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException sqlexception)
            {
                Logger.Logger.Log(sqlexception);
            }
            databaseConnection.Close();
        }

        public static void CreateVehicleTable()
        {
            Connect();
            string sql = "create table vehicle (licencePlates nvarchar (20) not null, vehicleType nvarchar (20) not null, "
                + "checkedIn bit, checkedInDate datetime)";
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException sqlexception)
            {
                Logger.Logger.Log(sqlexception);
            }
            databaseConnection.Close();
        }

        public static void CreateSubscriberTable()
        {

            Connect();
            string sql = "create table subscriber (licencePlates nvarchar (20) not null, ValidTo datetime)";

            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException sqlexception)
            {
                Logger.Logger.Log(sqlexception);
            }
            databaseConnection.Close();
        }

        public static void CreatePriceTable()
        {
            Connect();
            string sql = "create table price (charge numeric(18, 9), SummerTariff bit, TicketType nvarchar (20), vehicleType nvarchar (20))";

            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException sqlException)
            {
                Logger.Logger.Log(sqlException);
            }
            databaseConnection.Close();
        }

        public static void CreateTicketTable()
        {
            Connect();
            string sql = "create table ticket (licencePlates nvarchar (20) not null, TicketType nvarchar (20), vehicleType nvarchar (20), username nvarchar (20), checkedIn datetime, checkedOut datetime, charged numeric, chargedBase numeric, chargedTax numeric, JIR nvarchar (40), ZIK nvarchar (40), ChargeTicket bit)";

            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException sqlException)
            {
                Logger.Logger.Log(sqlException);
            }
            databaseConnection.Close();
        }

        public static void CreateCompanyTable()
        {
            Connect();
            string sql = "create table company (id INT IDENTITY NOT NULL PRIMARY KEY, name nvarchar (200) not null, address nvarchar (200), OIB nvarchar (11))";

            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnection);
            try
            {
                string existSql = "SELECT Count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'company'";
                SqlCeCommand existsCmd = new SqlCeCommand(existSql, DatabaseConnector.DatabaseConnection);
                existsCmd.CommandType = CommandType.Text;
                int count = (int)existsCmd.ExecuteScalar();
                if(!count.Equals(1)){
                    cmd.ExecuteNonQuery();
                }                
            }
            catch (SqlCeException sqlException)
            {
                Logger.Logger.Log(sqlException);
            }
            databaseConnection.Close();
        }

        public static void CreateChargedTicketTable()
        {
            Connect();
            string sql = "create table chargedTicket (username nvarchar (20), ticketType nvarchar (20), vehicleType nvarchar (20), licencePlates nvarchar (20), charged numeric(18, 9))";

            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException sqlException)
            {
                Logger.Logger.Log(sqlException);
            }
            databaseConnection.Close();

        }

        public static void Connect()
        {
                if (databaseConnection == null)
                {
                    databaseConnection = new SqlCeConnection(connectionString);
                }
                if (databaseConnection.State == ConnectionState.Closed)
                {
                    databaseConnection.Open();
                }
        }

        public static void Disconnect()
        {
            lock (DatabaseConnector.connectionString)
            {
                if (databaseConnection != null)
                {
                    databaseConnection.Close();
                }
            }
        }

        public static SqlCeConnection DatabaseConnection
        {
            get
            {
                lock (DatabaseConnector.connectionString)
                { Connect(); return databaseConnection; }
            }
        }
    }
}

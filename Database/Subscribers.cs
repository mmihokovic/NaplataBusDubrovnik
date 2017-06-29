using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using Model;
using System.Data;

namespace Database
{
    public static class Subscribers
    {
        static Subscribers()
        {

        }

        public static void AddSubscriber(string licencePlates, DateTime validTo)
        {
            string sql = "insert into subscriber "
            + "(licencePlates, validTo) "
            + "values (@licencePlates, @validTo)";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@licencePlates", licencePlates);
                cmd.Parameters.AddWithValue("@validTo", validTo);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();
        }

        public static Subscriber GetSubscriber(string licencePlates)
        {
            string sql = "select licencePlates, validTo from subscriber where licencePlates=@licencePlates";
            Subscriber subscriber = null;
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.Parameters.AddWithValue("@licencePlates", licencePlates);
            cmd.CommandType = CommandType.Text;
            SqlCeResultSet rs = cmd.ExecuteResultSet(
            ResultSetOptions.Scrollable);
            if (rs.HasRows)
            {
                int ordLicencePlates = rs.GetOrdinal("licencePlates");
                int ordValidTo = rs.GetOrdinal("validTo");

                rs.ReadFirst();
                subscriber = new Subscriber();
                subscriber.LicencePlates = rs.GetString(ordLicencePlates);
                subscriber.ValidTo = rs.GetDateTime(ordValidTo);
            }
            return subscriber;
        }

        public static void UpdateSubscriber(Subscriber subscriber)
        {
            string sql = "update subscriber "
          + "set licencePlates=@licencePlates, validTo=@validTo "
          + "where licencePlates=@licencePlates";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@licencePlates", subscriber.LicencePlates);
                cmd.Parameters.AddWithValue("@validTo", subscriber.ValidTo);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();
        }

        public static void RemoveSubscriber(string licencePlates)
        {
            string sql = "delete from subscriber where licencePlates=@licencePlates";
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.Parameters.AddWithValue("@licencePlates", licencePlates);
            cmd.ExecuteNonQuery();
            DatabaseConnector.Disconnect();
        }

        public static List<Subscriber> GetAllSubscribers()
        {
            List<Subscriber> subscribers = new List<Subscriber>();
            string sql = "select licencePlates, validTo from subscriber";

            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.CommandType = CommandType.Text;
            SqlCeResultSet rs = cmd.ExecuteResultSet(
            ResultSetOptions.Scrollable);
            if (rs.HasRows)
            {
                int ordLicencePlates = rs.GetOrdinal("licencePlates");
                int ordValidTo = rs.GetOrdinal("validTo");

                rs.ReadFirst();
                Subscriber subscriber = new Subscriber();
                subscriber.LicencePlates = rs.GetString(ordLicencePlates);
                subscriber.ValidTo = rs.GetDateTime(ordValidTo);

                subscribers.Add(subscriber);

                while (rs.Read())
                {
                    subscriber = new Subscriber();
                    subscriber.LicencePlates = rs.GetString(ordLicencePlates);
                    subscriber.ValidTo = rs.GetDateTime(ordValidTo);
                    subscribers.Add(subscriber);
                }
            }
            return subscribers;
        }
    }
}

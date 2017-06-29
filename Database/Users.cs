using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data.SqlServerCe;
using System.Data;

namespace Database
{
    public static class Users
    {
        static Users()
        {

        }

        public static void AddUser(String username, String password, string firstName, string lastName, string userType, string oib)
        {
            string sql = "insert into users "
            + "(Username, Password, FirstName, LastName, UserType, OIB) "
            + "values (@username, @password, @firstname, @lastname, @usertype, @OIB)";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@firstname", firstName);
                cmd.Parameters.AddWithValue("@lastname", lastName);
                cmd.Parameters.AddWithValue("@usertype", userType);
                cmd.Parameters.AddWithValue("@OIB", oib);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
            }
            DatabaseConnector.Disconnect();
        }

        public static User UpdateUser(String username, String password, string firstName, string lastName, string userType, string oib)
        {
            string sql = "update users "
          + "set username=@username, password=@password, firstName=@firstName, lastName=@lastName, userType=@userType, oib=@oib "
          + "where username=@username";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@userType", userType);
                cmd.Parameters.AddWithValue("@oib", oib);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                return null;
            }
            DatabaseConnector.Disconnect();
            return new User() { Username = username, Password = password, FirstName = firstName, LastName = lastName, UserType = userType, OIB = oib };
        }

        public static User GetUser(string username)
        {
            string sql = "select Username, Password, FirstName, LastName, UserType, OIB from users where Username=@username";
            User user = null;
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.CommandType = CommandType.Text;
            SqlCeResultSet rs = cmd.ExecuteResultSet(
            ResultSetOptions.Scrollable);
            if (rs.HasRows)
            {
                int ordUsername = rs.GetOrdinal("Username");
                int ordPassword = rs.GetOrdinal("Password");
                int ordUserType = rs.GetOrdinal("UserType");
                int ordLastName = rs.GetOrdinal("LastName");
                int ordFirstname = rs.GetOrdinal("FirstName");
                int ordOIB = rs.GetOrdinal("OIB");

                rs.ReadFirst();
                user = new User();
                user.FirstName = rs.GetString(ordFirstname);
                user.LastName = rs.GetString(ordLastName);
                user.Username = rs.GetString(ordUsername);
                user.Password = rs.GetString(ordPassword);
                user.UserType = rs.GetString(ordUserType);
                user.OIB = rs.GetString(ordOIB);
            }
            return user;
        }

        public static void RemoveUser(string username)
        {
            string sql = "delete from users where Username=@username";
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            DatabaseConnector.Disconnect();
        }

        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            string sql = "select Username, Password, FirstName, LastName, UserType, OIB from users";

            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.CommandType = CommandType.Text;
            SqlCeResultSet rs = cmd.ExecuteResultSet(
            ResultSetOptions.Scrollable);
            if (rs.HasRows)
            {
                int ordUsername = rs.GetOrdinal("Username");
                int ordPassword = rs.GetOrdinal("Password");
                int ordUserType = rs.GetOrdinal("UserType");
                int ordLastName = rs.GetOrdinal("LastName");
                int ordFirstname = rs.GetOrdinal("FirstName");
                int ordOIB = rs.GetOrdinal("OIB");

                rs.ReadFirst();
                User user = new User();
                user.FirstName = rs.GetString(ordFirstname);
                user.LastName = rs.GetString(ordLastName);
                user.Username = rs.GetString(ordUsername);
                user.Password = rs.GetString(ordPassword);
                user.UserType = rs.GetString(ordUserType);
                user.OIB = rs.GetString(ordOIB);
                users.Add(user);

                while (rs.Read())
                {
                    user = new User();
                    user.FirstName = rs.GetString(ordFirstname);
                    user.LastName = rs.GetString(ordLastName);
                    user.Username = rs.GetString(ordUsername);
                    user.Password = rs.GetString(ordPassword);
                    user.UserType = rs.GetString(ordUserType);
                    user.OIB = rs.GetString(ordOIB);
                    users.Add(user);
                }
            }
            return users;
        }
    }
}

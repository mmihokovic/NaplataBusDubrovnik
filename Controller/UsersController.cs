using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public class UsersController
    {
        public static User GetUser(string username)
        {
            return Database.Users.GetUser(username);
        }

        public static void AddUser(String username, String password, string firstName, string lastName, string userType, string oib)
        {
            Database.Users.AddUser(username, password, firstName, lastName, userType, oib);
        }

        public static User UpdateUser(String username, String password, string firstName, string lastName, string userType, string oib)
        {
            return Database.Users.UpdateUser(username, password, firstName, lastName, userType, oib);
        }

        public static User UpdateUser(User user)
        {
            return UpdateUser(user.Username, user.Password, user.FirstName, user.LastName, user.UserType, user.OIB);
        }

        public static void RemoveUser(string username)
        {
            Database.Users.RemoveUser(username);
        }
    }
}

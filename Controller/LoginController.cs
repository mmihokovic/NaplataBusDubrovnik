using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using Database;
using Controller.printer;
using Controller.tickets;

namespace Controller
{
    public static class LoginController
    {
        public static User LoggedInUser { get; set; }
        public static String CheckLogin(string username, string password)
        {
            if (username == null || username.Length == 0)
            {
                return String.Empty;
            }
            User user = Database.Users.GetUser(username);
            if(user == null){
                return String.Empty;
            }
            if(user.Password.Equals(password)){
                LoggedInUser = user;
                return user.UserType;
            }
            return String.Empty;
        }

        public static List<String> GetAllUsernames()
        {
            List<User> users = Database.Users.GetAllUsers();
            List<String> usernames = new List<string>();
            if (users == null)
            {
                return usernames;
            }
            foreach (User u in users)
            {
                usernames.Add(u.Username);
            }
            return usernames;
        }

        public static void LogOut()
        {
            Shared.Worker.Print((IPrintObject)new LogOutTicketPrintable(LoginController.LoggedInUser.Username));
            Tickets.RemoveChargedTickets(LoggedInUser.Username);
            LoggedInUser = null;
        }
    }
}

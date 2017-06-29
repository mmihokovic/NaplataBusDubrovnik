using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace NaplataBusDubrovnik
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Forms.Login = this;
            errorMessage.Hide();
            foreach (String username in Controller.LoginController.GetAllUsernames())
            {
                usernameBox.Items.Add(username);
            }
            if (usernameBox.Items.Count > 0)
            {
                usernameBox.SelectedIndex = 0;
            }

            label6.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (Controller.Shared.Worker != null)
            {
                Controller.Shared.Worker.SelfDestruct = true;
            }
            Application.Exit();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            String userType = Controller.LoginController.CheckLogin(usernameBox.Text, passwordBox.Text);
            if (userType == null || userType.Length == 0)
            {
                errorMessage.Show();
            }
            else if (userType.Equals("Operater"))
            {
                Forms.ChargeTickets = new ChargeTickets();
                Forms.ChargeTickets.Show();
                this.Hide();
                errorMessage.Hide();
            }
            else if (userType.Equals("Administrator"))
            {
                Forms.Administration = new Administration();
                Forms.Administration.Show();
                this.Hide();
                errorMessage.Hide();
                passwordBox.Text = "";
            }
            else
            {
                errorMessage.Show();
            }
        }

        private void label6_ParentChanged(object sender, EventArgs e)
        {

        }
    }
}
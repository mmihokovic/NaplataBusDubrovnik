using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;

namespace NaplataBusDubrovnik
{
    public partial class AdministrationUsers : Form
    {
        public AdministrationUsers()
        {
            InitializeComponent();
            Init();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Administration.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Forms.AdministrationAddUser == null)
                {
                    Forms.AdministrationAddUser = new AdministrationAddUser();
                }
                Forms.AdministrationAddUser.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        public void Init()
        {
            try
            {
                usernameBox.Items.Clear();
                foreach (String username in Controller.LoginController.GetAllUsernames())
                {
                    usernameBox.Items.Add(username);
                }


                roleBox.Items.Clear();
                roleBox.Items.Add("Operater");
                roleBox.Items.Add("Administrator");

                if (usernameBox.Items.Count != 0)
                {
                    usernameBox.SelectedIndex = 0;
                    setUserData();
                }
                else
                {
                    firstNameBox.Text = "";
                    lastNameBox.Text = "";
                    passwordBox.Text = "";
                    oibBox.Text = "";
                    roleBox.SelectedIndex = 0;
                }
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                MessageBox.Show(e.Message);
            }
        }

        private void setUserData()
        {
            try
            {
                User user = Controller.UsersController.GetUser(usernameBox.Text);
                firstNameBox.Text = user.FirstName;
                lastNameBox.Text = user.LastName;
                passwordBox.Text = user.Password;
                if (user.UserType.Equals("Operater"))
                {
                    roleBox.SelectedIndex = roleBox.Items.IndexOf("Operater");
                }
                else
                {
                    roleBox.SelectedIndex = roleBox.Items.IndexOf("Administrator");
                }
                oibBox.Text = user.OIB;
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                MessageBox.Show(e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Controller.UsersController.RemoveUser(usernameBox.Text);
                Init();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (firstNameBox.Text == null || firstNameBox.Text.Length == 0 || lastNameBox.Text.Length > 40)
                {
                    MessageBox.Show("Neispravno ime korisnika", "Pogrešan unos", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (lastNameBox.Text == null || lastNameBox.Text.Length == 0 || lastNameBox.Text.Length > 40)
                {
                    MessageBox.Show("Neispravno prezime korisnika", "Pogrešan unos", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (passwordBox.Text == null || passwordBox.Text.Length > 20)
                {
                    MessageBox.Show("Lozinka ima previše znakova", "Pogrešan unos", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (oibBox.Text == null || oibBox.Text.Length != 11)
                {
                    MessageBox.Show("Neispravan OIB korisnika", "Pogrešan unos", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    return;
                }

                Controller.UsersController.UpdateUser(usernameBox.Text, passwordBox.Text, firstNameBox.Text, lastNameBox.Text, roleBox.Text, oibBox.Text);
                Init();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void usernameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setUserData();
        }
    }
}
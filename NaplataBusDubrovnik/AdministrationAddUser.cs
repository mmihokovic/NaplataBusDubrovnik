using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NaplataBusDubrovnik
{
    public partial class AdministrationAddUser : Form
    {
        public AdministrationAddUser()
        {
            InitializeComponent();
            roleBox.Items.Clear();
            roleBox.Items.Add("Operater");
            roleBox.Items.Add("Administrator");
            roleBox.SelectedIndex = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void usernameBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AdministrationUsers.Show();
                usernameBox.Text = "";
                passwordBox.Text = "";
                firstNameBox.Text = "";
                lastNameBox.Text = "";
                oibBox.Text = "";
                this.Hide();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (usernameBox.Text == null | usernameBox.Text.Length == 0 || usernameBox.Text.Length > 20)
                {
                    MessageBox.Show("Neispravno korisničko ime", "Pogrešan unos", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    return;
                }

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

                Controller.UsersController.AddUser(usernameBox.Text, passwordBox.Text, firstNameBox.Text, lastNameBox.Text, roleBox.Text, oibBox.Text);
                
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Forms.AdministrationUsers.Show();
                Forms.AdministrationUsers.Init();
                usernameBox.Text = "";
                passwordBox.Text = "";
                firstNameBox.Text = "";
                lastNameBox.Text = "";
                oibBox.Text = "";
                this.Hide();
            }
        }

        private void AdministrationAddUser_Load(object sender, EventArgs e)
        {

        }
    }
}
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
    public partial class Administration : Form
    {
        public Administration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Forms.AdministrationUsers == null)
                {
                    Forms.AdministrationUsers = new AdministrationUsers();
                }
                Forms.AdministrationUsers.Show();
                this.Hide();
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
                if (Forms.AdministrationCharge == null)
                {
                    Forms.AdministrationCharge = new AdministrationCharge();
                }
                Forms.AdministrationCharge.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Forms.AdministrationSubscribers == null)
                {
                    Forms.AdministrationSubscribers = new AdministrationSubscribers();
                }
                Forms.AdministrationSubscribers.Show();
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

        private void menuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Controller.LoginController.LogOut();
                Forms.Login.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Controller.CounterController.ResetCounter();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
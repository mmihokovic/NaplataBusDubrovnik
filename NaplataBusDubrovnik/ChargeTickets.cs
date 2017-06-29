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
    public partial class ChargeTickets : Form
    {
        public ChargeTickets()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.ChargeRegularUser = new ChargeRegularUser();
                Forms.ChargeRegularUser.Show();
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
                Forms.ChargeSubscriber = new ChargeSubscriber();
                Forms.ChargeSubscriber.Show();
                Forms.ChargeSubscriber.init();
                this.Hide();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
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
    }
}
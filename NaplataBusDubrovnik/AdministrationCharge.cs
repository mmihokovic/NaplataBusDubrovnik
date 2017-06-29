using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;
using System.Globalization;

namespace NaplataBusDubrovnik
{
    public partial class AdministrationCharge : Form
    {
        public AdministrationCharge()
        {
            InitializeComponent();

            Forms.AdministrationCharge = this;

            foreach (VehicleType type in Controller.ChargeRegularUserController.GetAllVehicleTypes())
            {
                VehicleTypeBox.Items.Add(type.Type);
            }
            VehicleTypeBox.SelectedIndex = 0;

            foreach (TicketType type in Controller.ChargeRegularUserController.GetAllTicketTypes())
            {
                TicketTypeBox.Items.Add(type.Type);
            }
            TicketTypeBox.SelectedIndex = 0;

            comboBox1.Items.Add("Da");
            comboBox1.Items.Add("Ne");
            comboBox1.SelectedIndex = 0;

            populateCharge();
        }

        private void VehicleTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                populateCharge();
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

        private bool convertBoolean(String value)
        {
            if (value != null && value.ToLower().Equals("da"))
            {
                return true;
            }
            return false;
        }

        private void populateCharge()
        {
            decimal charge = Controller.ChargesController.GetCharge(TicketTypeBox.Text, VehicleTypeBox.Text, convertBoolean(comboBox1.Text));
            textBox1.Text = Decimal.Round(charge, 2).ToString();
        }

        private void TicketTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                populateCharge();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                populateCharge();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                decimal newCharge = 0;
                try
                {
                    newCharge = Decimal.Parse(textBox1.Text, NumberStyles.Number ^ NumberStyles.AllowThousands);
                    newCharge = Decimal.Round(newCharge, 2);
                }
                catch
                {                    
                    MessageBox.Show("Neispravan unos cijene", "Greška unosa", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
                if (newCharge < 0 || newCharge > 5000)
                {
                    MessageBox.Show("Neispravan unos cijene", "Greška unosa", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
                Controller.ChargesController.UpdateCharge(newCharge, convertBoolean(comboBox1.Text), TicketTypeBox.Text, VehicleTypeBox.Text);
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Forms.Administration.Show();
            this.Hide();
        }
    }
}
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
    public partial class ChargeRegularUser : Form
    {       

        public ChargeRegularUser()
        {
            try
            {
                InitializeComponent();
                foreach (VehicleType type in Controller.ChargeRegularUserController.GetAllVehicleTypes())
                {
                    VehicleTypeBox.Items.Add(type.Type);
                }
                VehicleTypeBox.SelectedIndex = 0;

                foreach (TicketType type in Controller.ChargeRegularUserController.GetAllTicketTypes())
                {
                    if (type.Type == "Pretplatnička karta")
                    {
                        continue;
                    }
                    TicketTypeBox.Items.Add(type.Type);
                }
                TicketTypeBox.SelectedIndex = 0;
                if (!Controller.ChargeRegularUserController.HasInternet())
                {
                    MessageBox.Show("Uređaj nema vezu s Internetom, računi se ne mogu fiskalizirati");
                }
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                MessageBox.Show(e.Message);
            }
        }

        private void VehicleTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Forms.ChargeTickets.Show();
            this.Hide();
            this.textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Controller.Shared.TestMode)
                {
                    BarcodeReader.barcode.BarcodeReadType.BarcodeType = Controller.barcode.BarcodeType.READ_REGULAR_USER_TICKET;
                    BarcodeReader.barcode.BarcodeReader r = new Controller.barcode.BarcodeReader();
                    r.Open();
                    r.ToggleTriger();
                }
                else
                {
                    Controller.ChargeRegularUserController.CheckOut("ZG-1234-ST");
                }
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
                String licencePlate = textBox1.Text.ToUpper();
                if (licencePlate.Length < 6)
                {
                    MessageBox.Show("Neispravna registracijska oznaka. Premalo znakova", 
                        "Greška", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (!validBarcode(licencePlate))
                {
                    MessageBox.Show("Neispravna registracijska oznaka. Neispravni znakovi.", 
                        "Greška", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (Controller.SubscriberController.GetSubscriber(licencePlate) != null)
                {
                    MessageBox.Show("Korisnik " + licencePlate + " je pretplatnik",
                        "Greška", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    return;
                }

                String vehicleType = VehicleTypeBox.Text;
                String ticketType = TicketTypeBox.Text;
 
                Controller.ChargeRegularUserController.CheckIn(licencePlate, vehicleType, ticketType);
            }
            catch(Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private bool validBarcode(String input)
        {
            bool isValid = true;
            foreach (Char c in input.ToCharArray())
            {
                if (c < 32 && c > 95)
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private void TicketTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;
using Controller;

namespace NaplataBusDubrovnik
{
    public partial class AdministrationAddSubscriber : Form
    {
        public AdministrationAddSubscriber()
        {
            try
            {
                InitializeComponent();
                foreach (VehicleType type in Controller.ChargeRegularUserController.GetAllVehicleTypes())
                {
                    comboBox1.Items.Add(type.Type);
                }
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                MessageBox.Show(e.Message);
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AdministrationSubscribers.Show();
                this.subscriberBox.Text = "";
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
                String licencePlate = subscriberBox.Text.ToUpper();
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

                var dialogResult = MessageBox.Show("Želite li R1 račun ?", "R1", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.No)
                {
                    Vehicle vehicle = Controller.VehicleController.AddVehicle(licencePlate, comboBox1.Text);
                    Controller.SubscriberController.AddSubscriber(vehicle.LicencePlates, dateTimePicker1.Value, null);
                    Forms.Administration.Show();
                    Forms.AdministrationSubscribers.Init();
                    this.subscriberBox.Text = "";
                    this.Hide();
                }
                if (dialogResult == DialogResult.Yes)
                {
                    Forms.R1Form = new R1Form();
                    Forms.R1Form.ChargeUserData = new ChargeUserData
                    {
                        ChargeSource = ChargeSourceEnum.AddSubscriber,
                        LicencePlate = licencePlate,
                        VehicleType = comboBox1.Text,
                        SubscriptionValidTo = dateTimePicker1.Value
                    };
                    Forms.R1Form.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
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
    }
}
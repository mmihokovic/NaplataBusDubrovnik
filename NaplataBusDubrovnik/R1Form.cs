using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Controller;
using Model;

namespace NaplataBusDubrovnik
{
    public partial class R1Form : Form
    {
        public ChargeUserData ChargeUserData { get; set; }

        public R1Form()
        {
            InitializeComponent();            
            companyComboBox.SelectedIndexChanged += new EventHandler(companyComboBox_SelectedIndexChanged);
            this.Activated += new EventHandler(R1Form_Activated);
        }

        void R1Form_Activated(object sender, EventArgs e)
        {
            initCompany();
        }

        private void initCompany(){
            companyComboBox.Items.Clear();
            nameTextBox.Text = "";
            addressTextBox.Text = "";
            OIBtextBox.Text = "";

            companyComboBox.Items.Add(new Company());
            foreach (var company in Controller.CompaniesController.GetAllCompanies())
            {
                companyComboBox.Items.Add(company);
            }            
            companyComboBox.SelectedIndex = 0;
        }

        void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCompany = (Company) companyComboBox.SelectedItem;
            nameTextBox.Text = selectedCompany.Name;
            addressTextBox.Text = selectedCompany.Address;
            OIBtextBox.Text = selectedCompany.OIB;
        }

        private void ispisBezR1Click(object sender, EventArgs e)
        {
            if (ChargeUserData.ChargeSource == ChargeSourceEnum.ChargeRegularUser)
            {
                try
                {
                    Controller.ChargeRegularUserController.CheckOut(this.ChargeUserData.LicencePlate, null);
                }
                catch (Exception ex)
                {
                    Logger.Logger.Log(ex);
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
                this.Hide();
                Forms.ChargeRegularUser.Show();
            }
            else if (ChargeUserData.ChargeSource == ChargeSourceEnum.ChargeSubscriber)
            {
                try
                {
                    Controller.SubscriberController.UpdateSubscriber(ChargeUserData.Subscriber, null);
                }
                catch (Exception ex)
                {
                    Logger.Logger.Log(ex);
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
                Forms.AdministrationSubscribers.Show();
                Forms.AdministrationSubscribers.Init();
                this.Hide();
                
            }
            else if (ChargeUserData.ChargeSource == ChargeSourceEnum.AddSubscriber)
            {
                try
                {
                    Vehicle vehicle = Controller.VehicleController.AddVehicle(ChargeUserData.LicencePlate, ChargeUserData.VehicleType);
                    Controller.SubscriberController.AddSubscriber(vehicle.LicencePlates, ChargeUserData.SubscriptionValidTo, null);
                }
                catch (Exception ex)
                {
                    Logger.Logger.Log(ex);
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }

                Forms.AdministrationSubscribers.Show();
                Forms.AdministrationSubscribers.Init();
                Forms.AdministrationAddSubscriber.subscriberBox.Text = "";
                this.Hide();
            }
        }

        private void ispis_Click(object sender, EventArgs ev)
        {
            var selectedCompany = (Company)companyComboBox.SelectedItem;

            if (String.IsNullOrEmpty(nameTextBox.Text) || nameTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Naziv tvrtke je obavezan", "Neispravni podaci", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (String.IsNullOrEmpty(addressTextBox.Text) || addressTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Adresa tvrtke je obavezana", "Neispravni podaci", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!String.IsNullOrEmpty(OIBtextBox.Text) && !CheckOIB(OIBtextBox.Text))
            {
                MessageBox.Show("OIB tvrtke je neispravan", "Neispravni podaci", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            selectedCompany.Name = nameTextBox.Text;
            selectedCompany.Address = addressTextBox.Text;
            selectedCompany.OIB = OIBtextBox.Text;

            selectedCompany = Controller.CompaniesController.AddUpdateCompany(selectedCompany);

            if (ChargeUserData.ChargeSource == ChargeSourceEnum.ChargeRegularUser)
            {
                try
                {
                    Controller.ChargeRegularUserController.CheckOut(ChargeUserData.LicencePlate, selectedCompany);
                }
                catch (Exception e)
                {
                    Logger.Logger.Log(e);
                    MessageBox.Show(e.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
                
                Forms.ChargeRegularUser.Show();
                this.Hide();
            }
            else if(ChargeUserData.ChargeSource == ChargeSourceEnum.ChargeSubscriber)
            {
                try
                {
                    Controller.SubscriberController.UpdateSubscriber(ChargeUserData.Subscriber, selectedCompany);
                }
                catch (Exception e)
                {
                    Logger.Logger.Log(e);
                    MessageBox.Show(e.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }

                Forms.AdministrationSubscribers.Show();
                Forms.AdministrationSubscribers.Init();
                this.Hide();
            }
            else if (ChargeUserData.ChargeSource == ChargeSourceEnum.AddSubscriber)
            {
                try
                {
                    Vehicle vehicle = Controller.VehicleController.AddVehicle(ChargeUserData.LicencePlate, ChargeUserData.VehicleType);
                    Controller.SubscriberController.AddSubscriber(vehicle.LicencePlates, ChargeUserData.SubscriptionValidTo, selectedCompany);
                }
                catch (Exception e)
                {
                    Logger.Logger.Log(e);
                    MessageBox.Show(e.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }

                Forms.AdministrationSubscribers.Show();
                Forms.AdministrationSubscribers.Init();
                Forms.AdministrationAddSubscriber.subscriberBox.Text = "";
                this.Hide();
            }

            
        }

        public static bool CheckOIB(string oib)
        {
            if (oib.Length != 11) return false;

            try
            {
                long.Parse(oib);
            }
            catch
            {
                return false;
            }

            int a = 10;
            for (int i = 0; i < 10; i++)
            {
                a = a + Convert.ToInt32(oib.Substring(i, 1));
                a = a % 10;
                if (a == 0) a = 10;
                a *= 2;
                a = a % 11;
            }
            int kontrolni = 11 - a;
            if (kontrolni == 10) kontrolni = 0;

            return kontrolni == Convert.ToInt32(oib.Substring(10, 1));
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            if (ChargeUserData.ChargeSource == ChargeSourceEnum.ChargeRegularUser)
            {
                Forms.ChargeRegularUser.Show();
            }
            else if (ChargeUserData.ChargeSource == ChargeSourceEnum.ChargeSubscriber)
            {
                Forms.AdministrationSubscribers.Show();
                Forms.AdministrationSubscribers.Init();
            }
            else if (ChargeUserData.ChargeSource == ChargeSourceEnum.AddSubscriber)
            {
                Forms.AdministrationAddSubscriber.Show();                
            }
            this.Hide();
        }
    }
}
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
    public partial class AdministrationR1 : Form
    {
        public AdministrationR1()
        {
            InitializeComponent();
            companyComboBox.SelectedIndexChanged += new EventHandler(companyComboBox_SelectedIndexChanged);
            this.Activated += new EventHandler(R1Form_Activated);
        }

        void R1Form_Activated(object sender, EventArgs e)
        {
            initCompany();
        }

        private void initCompany()
        {
            companyComboBox.Items.Clear();
            nameTextBox.Text = "";
            addressTextBox.Text = "";
            OIBtextBox.Text = "";

            foreach (var company in Controller.CompaniesController.GetAllCompanies())
            {
                companyComboBox.Items.Add(company);
            }

            if (companyComboBox.Items.Count != 0)
            {
                companyComboBox.SelectedIndex = 0;
            }
        }

        void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCompany = (Company)companyComboBox.SelectedItem;
            nameTextBox.Text = selectedCompany.Name;
            addressTextBox.Text = selectedCompany.Address;
            OIBtextBox.Text = selectedCompany.OIB;
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

        private void AdministrationR1_Load(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click_1(object sender, EventArgs e)
        {
            var selectedCompany = (Company)companyComboBox.SelectedItem;
            Controller.CompaniesController.DeleteCompany(selectedCompany);
            initCompany();
        }

        private void saveButton_Click_1(object sender, EventArgs e)
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
            initCompany();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Forms.AdministrationR1AddCompany == null)
                {
                    Forms.AdministrationR1AddCompany = new AdministrationR1AddCompany();
                }
                Forms.AdministrationR1AddCompany.Show();
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
                Forms.Administration.Show();
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
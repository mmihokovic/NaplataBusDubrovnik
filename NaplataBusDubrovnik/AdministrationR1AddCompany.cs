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
    public partial class AdministrationR1AddCompany : Form
    {
        public AdministrationR1AddCompany()
        {
            InitializeComponent();
        }

        private void cancelMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AdministrationR1.Show();
                nameTextBox.Text = "";
                addressTextBox.Text = "";
                OIBtextBox.Text = "";
                this.Hide();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedCompany = new Company();

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

                if (String.IsNullOrEmpty(OIBtextBox.Text) || !CheckOIB(OIBtextBox.Text))
                {
                    MessageBox.Show("OIB tvrtke je neispravan", "Neispravni podaci", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                selectedCompany.Name = nameTextBox.Text;
                selectedCompany.Address = addressTextBox.Text;
                selectedCompany.OIB = OIBtextBox.Text;

                selectedCompany = Controller.CompaniesController.AddUpdateCompany(selectedCompany);
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Forms.AdministrationR1.Show();
                nameTextBox.Text = "";
                addressTextBox.Text = "";
                OIBtextBox.Text = "";
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
    }
}
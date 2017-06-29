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
    public partial class ChargeSubscriber : Form
    {
        public ChargeSubscriber()
        {
            InitializeComponent();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Forms.ChargeTickets.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (subscriberBox.Items.Count != 0)
                {
                    Controller.ChargeSubscriberController.CheckIn(subscriberBox.Text);
                }
                else
                {
                    MessageBox.Show("Nije odabran niti jedan pretplatnik.");
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Controller.barcode.BarcodeReadType.BarcodeType = Controller.barcode.BarcodeType.READ_SUBSCRIBER;
                Controller.barcode.BarcodeReader r = new Controller.barcode.BarcodeReader();
                r.Open();
                r.ToggleTriger();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        public void init()
        {
            try
            {
                subscriberBox.Items.Clear();
                foreach (Subscriber s in Controller.SubscriberController.GetAllValidSubscribers())
                {
                    subscriberBox.Items.Add(s.LicencePlates.ToUpper());
                }
                if (subscriberBox.Items.Count != 0)
                {
                    subscriberBox.SelectedIndex = 0;
                }
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                MessageBox.Show(e.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Controller.Shared.TestMode)
                {
                    Controller.barcode.BarcodeReadType.BarcodeType = Controller.barcode.BarcodeType.READ_SUBSCRIBER_TICKET;
                    Controller.barcode.BarcodeReader r = new Controller.barcode.BarcodeReader();
                    r.Open();
                    r.ToggleTriger();
                }
                else
                {
                    Controller.ChargeSubscriberController.CheckOut("ZG-1234-ET");
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
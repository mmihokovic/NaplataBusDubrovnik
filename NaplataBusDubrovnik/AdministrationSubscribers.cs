﻿using System;
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
    public partial class AdministrationSubscribers : Form
    {
        public AdministrationSubscribers()
        {
            InitializeComponent();
            Init();
        }

        private void subscriberBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker1.Value = Controller.SubscriberController.GetSubscriber(subscriberBox.Text).ValidTo;
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                MessageBox.Show(ex.Message);
            }
        }

        public void Init()
        {
            try
            {
                subscriberBox.Items.Clear();
                foreach (Subscriber s in Controller.SubscriberController.GetAllSubscribers())
                {
                    subscriberBox.Items.Add(s.LicencePlates);
                }

                if (subscriberBox.Items.Count != 0)
                {
                    subscriberBox.SelectedIndex = 0;
                    dateTimePicker1.Value = Controller.SubscriberController.GetSubscriber(subscriberBox.Text).ValidTo;
                }
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                MessageBox.Show(e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (subscriberBox.Items.Count != 0)
                {
                    Controller.SubscriberController.RemoveSubscriber(subscriberBox.Text);
                    Init();
                }
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
                if (subscriberBox.Items.Count != 0)
                {
                    Subscriber s = Controller.SubscriberController.GetSubscriber(subscriberBox.Text);
                    s.ValidTo = dateTimePicker1.Value;

                    var dialogResult = MessageBox.Show("Želite li R1 račun ?", "R1", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.No)
                    {
                        Controller.SubscriberController.UpdateSubscriber(s, null);
                        Init();
                    }
                    if (dialogResult == DialogResult.Yes)
                    {
                        Forms.R1Form = new R1Form();
                        Forms.R1Form.ChargeUserData = new ChargeUserData
                        {
                            ChargeSource = ChargeSourceEnum.ChargeSubscriber,
                            Subscriber = s
                        };
                        Forms.R1Form.Show();
                        this.Hide();
                    }
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
                if (Forms.AdministrationAddSubscriber == null)
                {
                    Forms.AdministrationAddSubscriber = new AdministrationAddSubscriber();
                }
                Forms.AdministrationAddSubscriber.Show();
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
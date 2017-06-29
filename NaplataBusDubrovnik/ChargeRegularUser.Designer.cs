﻿namespace NaplataBusDubrovnik
{
    partial class ChargeRegularUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.VehicleTypeBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TicketTypeBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Natrag";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Ispis tiketa";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 20);
            this.label1.Text = "Registracijska oznaka vozila:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.textBox1.Location = new System.Drawing.Point(23, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(201, 31);
            this.textBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 20);
            this.label2.Text = "Vrsta vozila:";
            // 
            // VehicleTypeBox
            // 
            this.VehicleTypeBox.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.VehicleTypeBox.Location = new System.Drawing.Point(23, 102);
            this.VehicleTypeBox.Name = "VehicleTypeBox";
            this.VehicleTypeBox.Size = new System.Drawing.Size(201, 32);
            this.VehicleTypeBox.TabIndex = 3;
            this.VehicleTypeBox.SelectedIndexChanged += new System.EventHandler(this.VehicleTypeBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(23, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 20);
            this.label3.Text = "Vrsta karte:";
            // 
            // TicketTypeBox
            // 
            this.TicketTypeBox.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.TicketTypeBox.Location = new System.Drawing.Point(23, 164);
            this.TicketTypeBox.Name = "TicketTypeBox";
            this.TicketTypeBox.Size = new System.Drawing.Size(201, 32);
            this.TicketTypeBox.TabIndex = 5;
            this.TicketTypeBox.SelectedIndexChanged += new System.EventHandler(this.TicketTypeBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(3, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(234, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "Očitaj izlaznu kartu";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(240, 20);
            this.label4.Text = "___________________________________";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ChargeRegularUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TicketTypeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.VehicleTypeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "ChargeRegularUser";
            this.Text = "Redovni korisnik";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox VehicleTypeBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TicketTypeBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}
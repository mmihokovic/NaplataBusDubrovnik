namespace NaplataBusDubrovnik
{
    partial class AdministrationR1AddCompany
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
            this.cancelMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.OIBtextBox = new System.Windows.Forms.TextBox();
            this.OIBlabel = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.adresaLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.TvrtkaLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.cancelMenuItem);
            this.mainMenu1.MenuItems.Add(this.saveMenuItem);
            // 
            // cancelMenuItem
            // 
            this.cancelMenuItem.Text = "Odustani";
            this.cancelMenuItem.Click += new System.EventHandler(this.cancelMenuItem_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Text = "Spremi";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // OIBtextBox
            // 
            this.OIBtextBox.Location = new System.Drawing.Point(7, 198);
            this.OIBtextBox.Name = "OIBtextBox";
            this.OIBtextBox.Size = new System.Drawing.Size(224, 21);
            this.OIBtextBox.TabIndex = 19;
            // 
            // OIBlabel
            // 
            this.OIBlabel.Enabled = false;
            this.OIBlabel.Location = new System.Drawing.Point(7, 174);
            this.OIBlabel.Name = "OIBlabel";
            this.OIBlabel.Size = new System.Drawing.Size(100, 20);
            this.OIBlabel.Text = "OIB:";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(7, 135);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(227, 21);
            this.addressTextBox.TabIndex = 18;
            // 
            // adresaLabel
            // 
            this.adresaLabel.Location = new System.Drawing.Point(7, 112);
            this.adresaLabel.Name = "adresaLabel";
            this.adresaLabel.Size = new System.Drawing.Size(100, 20);
            this.adresaLabel.Text = "Adresa";
            // 
            // nameTextBox
            // 
            this.nameTextBox.AcceptsReturn = true;
            this.nameTextBox.Location = new System.Drawing.Point(7, 74);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(224, 21);
            this.nameTextBox.TabIndex = 17;
            // 
            // TvrtkaLabel
            // 
            this.TvrtkaLabel.Location = new System.Drawing.Point(7, 50);
            this.TvrtkaLabel.Name = "TvrtkaLabel";
            this.TvrtkaLabel.Size = new System.Drawing.Size(100, 20);
            this.TvrtkaLabel.Text = "Naziv tvrtke:";
            // 
            // AdministrationR1AddCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.OIBtextBox);
            this.Controls.Add(this.OIBlabel);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.adresaLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.TvrtkaLabel);
            this.Menu = this.mainMenu1;
            this.Name = "AdministrationR1AddCompany";
            this.Text = "Dodaj tvrtku za R1 račun";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem cancelMenuItem;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private System.Windows.Forms.TextBox OIBtextBox;
        private System.Windows.Forms.Label OIBlabel;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label adresaLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label TvrtkaLabel;
    }
}
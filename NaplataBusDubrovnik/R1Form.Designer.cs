namespace NaplataBusDubrovnik
{
    partial class R1Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R1Form));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.companyComboBox = new System.Windows.Forms.ComboBox();
            this.TvrtkaLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.adresaLabel = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.OIBlabel = new System.Windows.Forms.Label();
            this.OIBtextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            this.mainMenu1.MenuItems.Add(this.menuItem3);
            // 
            // menuItem1
            // 
            resources.ApplyResources(this.menuItem1, "menuItem1");
            this.menuItem1.Click += new System.EventHandler(this.ispis_Click);
            // 
            // menuItem2
            // 
            resources.ApplyResources(this.menuItem2, "menuItem2");
            this.menuItem2.Click += new System.EventHandler(this.ispisBezR1Click);
            // 
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // companyComboBox
            // 
            resources.ApplyResources(this.companyComboBox, "companyComboBox");
            this.companyComboBox.Name = "companyComboBox";
            // 
            // TvrtkaLabel
            // 
            resources.ApplyResources(this.TvrtkaLabel, "TvrtkaLabel");
            this.TvrtkaLabel.Name = "TvrtkaLabel";
            // 
            // nameTextBox
            // 
            this.nameTextBox.AcceptsReturn = true;
            resources.ApplyResources(this.nameTextBox, "nameTextBox");
            this.nameTextBox.Name = "nameTextBox";
            // 
            // adresaLabel
            // 
            resources.ApplyResources(this.adresaLabel, "adresaLabel");
            this.adresaLabel.Name = "adresaLabel";
            // 
            // addressTextBox
            // 
            resources.ApplyResources(this.addressTextBox, "addressTextBox");
            this.addressTextBox.Name = "addressTextBox";
            // 
            // OIBlabel
            // 
            resources.ApplyResources(this.OIBlabel, "OIBlabel");
            this.OIBlabel.Name = "OIBlabel";
            // 
            // OIBtextBox
            // 
            resources.ApplyResources(this.OIBtextBox, "OIBtextBox");
            this.OIBtextBox.Name = "OIBtextBox";
            // 
            // R1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.OIBtextBox);
            this.Controls.Add(this.OIBlabel);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.adresaLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.TvrtkaLabel);
            this.Controls.Add(this.companyComboBox);
            this.Menu = this.mainMenu1;
            this.Name = "R1Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox companyComboBox;
        private System.Windows.Forms.Label TvrtkaLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label adresaLabel;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label OIBlabel;
        private System.Windows.Forms.TextBox OIBtextBox;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
    }
}
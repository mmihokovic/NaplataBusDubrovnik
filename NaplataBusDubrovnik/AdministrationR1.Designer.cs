namespace NaplataBusDubrovnik
{
    partial class AdministrationR1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdministrationR1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.OIBtextBox = new System.Windows.Forms.TextBox();
            this.OIBlabel = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.adresaLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.TvrtkaLabel = new System.Windows.Forms.Label();
            this.companyComboBox = new System.Windows.Forms.ComboBox();
            this.spremiButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            resources.ApplyResources(this.menuItem1, "menuItem1");
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            resources.ApplyResources(this.menuItem2, "menuItem2");
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // OIBtextBox
            // 
            resources.ApplyResources(this.OIBtextBox, "OIBtextBox");
            this.OIBtextBox.Name = "OIBtextBox";
            // 
            // OIBlabel
            // 
            resources.ApplyResources(this.OIBlabel, "OIBlabel");
            this.OIBlabel.Name = "OIBlabel";
            // 
            // addressTextBox
            // 
            resources.ApplyResources(this.addressTextBox, "addressTextBox");
            this.addressTextBox.Name = "addressTextBox";
            // 
            // adresaLabel
            // 
            resources.ApplyResources(this.adresaLabel, "adresaLabel");
            this.adresaLabel.Name = "adresaLabel";
            // 
            // nameTextBox
            // 
            this.nameTextBox.AcceptsReturn = true;
            resources.ApplyResources(this.nameTextBox, "nameTextBox");
            this.nameTextBox.Name = "nameTextBox";
            // 
            // TvrtkaLabel
            // 
            resources.ApplyResources(this.TvrtkaLabel, "TvrtkaLabel");
            this.TvrtkaLabel.Name = "TvrtkaLabel";
            // 
            // companyComboBox
            // 
            resources.ApplyResources(this.companyComboBox, "companyComboBox");
            this.companyComboBox.Name = "companyComboBox";
            // 
            // spremiButton
            // 
            resources.ApplyResources(this.spremiButton, "spremiButton");
            this.spremiButton.Name = "spremiButton";
            this.spremiButton.Click += new System.EventHandler(this.saveButton_Click_1);
            // 
            // deleteButton
            // 
            resources.ApplyResources(this.deleteButton, "deleteButton");
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click_1);
            // 
            // AdministrationR1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.spremiButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.OIBtextBox);
            this.Controls.Add(this.OIBlabel);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.adresaLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.TvrtkaLabel);
            this.Controls.Add(this.companyComboBox);
            this.Menu = this.mainMenu1;
            this.Name = "AdministrationR1";
            this.Load += new System.EventHandler(this.AdministrationR1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox OIBtextBox;
        private System.Windows.Forms.Label OIBlabel;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label adresaLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label TvrtkaLabel;
        private System.Windows.Forms.ComboBox companyComboBox;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Button spremiButton;
        private System.Windows.Forms.Button deleteButton;
    }
}
namespace NaplataBusDubrovnik
{
    partial class AdministrationAddUser
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
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.oibBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.roleBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lastNameBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.firstNameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(26, 135);
            this.passwordBox.MaxLength = 20;
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(189, 21);
            this.passwordBox.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(26, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "Lozinka:";
            // 
            // oibBox
            // 
            this.oibBox.Location = new System.Drawing.Point(26, 211);
            this.oibBox.MaxLength = 13;
            this.oibBox.Name = "oibBox";
            this.oibBox.Size = new System.Drawing.Size(189, 21);
            this.oibBox.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(26, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.Text = "OIB:";
            // 
            // roleBox
            // 
            this.roleBox.Location = new System.Drawing.Point(26, 173);
            this.roleBox.Name = "roleBox";
            this.roleBox.Size = new System.Drawing.Size(189, 22);
            this.roleBox.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(26, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Uloga:";
            // 
            // lastNameBox
            // 
            this.lastNameBox.Location = new System.Drawing.Point(115, 93);
            this.lastNameBox.MaxLength = 40;
            this.lastNameBox.Name = "lastNameBox";
            this.lastNameBox.Size = new System.Drawing.Size(100, 21);
            this.lastNameBox.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(115, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Prezime:";
            // 
            // firstNameBox
            // 
            this.firstNameBox.Location = new System.Drawing.Point(26, 93);
            this.firstNameBox.MaxLength = 40;
            this.firstNameBox.Name = "firstNameBox";
            this.firstNameBox.Size = new System.Drawing.Size(83, 21);
            this.firstNameBox.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(26, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Ime:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(26, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Korisničko ime:";
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(26, 55);
            this.usernameBox.MaxLength = 20;
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(189, 21);
            this.usernameBox.TabIndex = 32;
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Odustani";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Spremi";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // AdministrationAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.oibBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.roleBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lastNameBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.firstNameBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "AdministrationAddUser";
            this.Text = "Dodaj operatera";
            this.Load += new System.EventHandler(this.AdministrationAddUser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox oibBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox roleBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lastNameBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox firstNameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.TextBox usernameBox;
    }
}
using Google.GData.Client;

namespace ReLive
{
    partial class GoogleLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoogleLogin));
            this.Username = new System.Windows.Forms.TextBox();
            this.labAccount = new System.Windows.Forms.Label();
            this.labPassword = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.linkCreateAccount = new System.Windows.Forms.LinkLabel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.linkAddPicasaweb = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(68, 12);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(156, 20);
            this.Username.TabIndex = 0;
            // 
            // labAccount
            // 
            this.labAccount.AutoSize = true;
            this.labAccount.Location = new System.Drawing.Point(4, 15);
            this.labAccount.Name = "labAccount";
            this.labAccount.Size = new System.Drawing.Size(58, 13);
            this.labAccount.TabIndex = 1;
            this.labAccount.Text = "Username:";
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Location = new System.Drawing.Point(6, 43);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(56, 13);
            this.labPassword.TabIndex = 2;
            this.labPassword.Text = "Password:";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(68, 40);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(156, 20);
            this.Password.TabIndex = 3;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(68, 83);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // linkCreateAccount
            // 
            this.linkCreateAccount.AutoSize = true;
            this.linkCreateAccount.Location = new System.Drawing.Point(62, 67);
            this.linkCreateAccount.Name = "linkCreateAccount";
            this.linkCreateAccount.Size = new System.Drawing.Size(81, 13);
            this.linkCreateAccount.TabIndex = 5;
            this.linkCreateAccount.TabStop = true;
            this.linkCreateAccount.Text = "Create Account";
            this.linkCreateAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCreateAccount_LinkClicked);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(149, 83);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Skip";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // linkAddPicasaweb
            // 
            this.linkAddPicasaweb.AutoSize = true;
            this.linkAddPicasaweb.Location = new System.Drawing.Point(150, 66);
            this.linkAddPicasaweb.Name = "linkAddPicasaweb";
            this.linkAddPicasaweb.Size = new System.Drawing.Size(81, 13);
            this.linkAddPicasaweb.TabIndex = 7;
            this.linkAddPicasaweb.TabStop = true;
            this.linkAddPicasaweb.Text = "Add Picasaweb";
            this.linkAddPicasaweb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAddPicasaweb_LinkClicked);
            // 
            // GoogleLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 118);
            this.Controls.Add(this.linkAddPicasaweb);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.linkCreateAccount);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.labAccount);
            this.Controls.Add(this.Username);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GoogleLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Google Login";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private string authToken;

        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label labAccount;
        private System.Windows.Forms.Label labPassword;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.LinkLabel linkCreateAccount;
        private System.Windows.Forms.Button buttonCancel;
        private Service service;
        private System.Windows.Forms.LinkLabel linkAddPicasaweb;
    }
}
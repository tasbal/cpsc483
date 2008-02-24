using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Google.GData.Client;

namespace ReLive
{
    public partial class GoogleLogin : Form
    {
        public GoogleLogin(Service serviceToUse)
        {
            InitializeComponent();

            this.service = serviceToUse;
        }

        public string AuthenticationToken
        {
            get
            {
                return this.authToken;
            }
        }

        public string User
        {
            get
            {
                return this.Username.Text;
            }
        }

        private void linkCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.google.com/accounts/NewAccount"); 
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.authToken = null;
            Close();
        }

        private void GoogleLogin_Load(object sender, EventArgs e)
        {
           
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.service.setUserCredentials(this.Username.Text, this.Password.Text);
            this.authToken = this.service.QueryAuthenticationToken();
            this.Close();
        }
    }
}
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
            System.Diagnostics.Process.Start("https://www.google.com/accounts/NewAccount?hl=en_US&continue=http%3A%2F%2Fpicasaweb.google.com%2Flh%2Flogin%3Fcontinue%3Dhttp%253A%252F%252Fpicasaweb.google.com%252F&service=lh2&passive=true"); 
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.authToken = null;
            Close();
        }

        private void GoogleLogin_Load(object sender, EventArgs e)
        {
           
        }

        //need better exception catching
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.service.setUserCredentials(this.Username.Text, this.Password.Text);

            try
            {
                authToken = this.service.QueryAuthenticationToken();
            }
            catch (InvalidCredentialsException)
            {
                MessageBox.Show("Invalid Credentials");
            }
            catch (AuthenticationException)
            {
                MessageBox.Show("Invalid Credentials");
            }
            if (authToken != null) //proceed once login accepted
                Close();
        }

        private void linkAddPicasaweb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.picasaweb.google.com");
        }
    }
}
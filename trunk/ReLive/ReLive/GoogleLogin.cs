using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReLive
{
    public partial class GoogleLogin : Form
    {
        public GoogleLogin()
        {
            InitializeComponent();
        }

        private void linkCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.google.com/accounts/NewAccount"); 
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GoogleLogin_Load(object sender, EventArgs e)
        {
       
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

        }
    }
}
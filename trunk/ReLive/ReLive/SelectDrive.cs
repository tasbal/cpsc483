using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace reLive
{
    public partial class SelectDrive : Form
    {
        public int choice;
        public bool noChoice = true;
        public SelectDrive()
        {
            InitializeComponent();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            choice = deviceBox.SelectedIndex;
            if (choice != -1)
            {
                noChoice = false;
                this.Close();
            }
            else
                MessageBox.Show("No drive was selected.  One will need to be selected in order to sync, save config, or format a drive.");
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No drive was selected.  One will need to be selected in order to sync, save config, or format a drive.");
            this.Close();
        }
    }
}
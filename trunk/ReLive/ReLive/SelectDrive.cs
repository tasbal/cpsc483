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

        private void button1_Click(object sender, EventArgs e)
        {
            choice = comboBox1.SelectedIndex;
            noChoice = false;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No drive was selected.  One will need to be selected in order to sync or format a drive.");
            this.Close();
        }
    }
}
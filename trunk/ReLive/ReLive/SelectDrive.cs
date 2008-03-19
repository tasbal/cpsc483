using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReLive
{
    public partial class SelectDrive : Form
    {
        public int choice;
        public SelectDrive()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            choice = comboBox1.SelectedIndex;
            this.Close();
        }

        private void SelectDrive_Load(object sender, EventArgs e)
        {

        }
    }
}
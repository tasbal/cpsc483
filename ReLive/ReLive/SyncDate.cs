using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace reLive
{
    public partial class SyncDate : Form
    {
        public SyncDate()
        {
            InitializeComponent();
        }

        public DateTime getDate()
        {
            return syncDataPicker.Value.Date;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReLive
{
    public partial class ProgPopup : Form
    {
        public int fileLength = 0;
        public ProgPopup()
        {
            InitializeComponent();
        }

        private void ProgPopup_Load(object sender, EventArgs e)
        {
            progressBar1.BringToFront();
            progressBar1.Visible = true;
            progressBar1.Step = progressBar1.Width / fileLength;
        }
    }
}
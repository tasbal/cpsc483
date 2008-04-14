using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace reLive
{
    public partial class MapBrowser : Form
    {
        public MapBrowser()
        {
            InitializeComponent();
        }

        private void MapBrowser_Load(object sender, EventArgs e)
        {

        }

        private void albumMap_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //stop users from navigating page, but allow for load of map
            albumMap.AllowNavigation = false;
        }
    }
}
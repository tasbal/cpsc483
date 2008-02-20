using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LabName.Text = "< Dynamically Loaded Name >";
    }
    protected void LinkUser_Click(object sender, EventArgs e)
    {
        LabAlbum.Text = LinkUser.Text;

    }
    protected void LinkDate_Click(object sender, EventArgs e)
    {
        LabAlbum.Text = LinkDate.Text;
    }

    protected void LinkLocation_Click(object sender, EventArgs e)
    {
        LabAlbum.Text = LinkLocation.Text;
    }
    protected void LinkPortrait_Click(object sender, EventArgs e)
    {
        LabAlbum.Text = LinkPortrait.Text;
    }
}

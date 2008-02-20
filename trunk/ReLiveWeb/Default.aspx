<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div>
        <h1 style="text-align: center">
            <asp:Label ID="LabName" runat="server"></asp:Label><span style="font-family: Calibri">&nbsp;</span></h1>
        <p style="text-align: center">
            <table id="TabAlbums" style="width: 800px; height: 48px; font-family: Calibri;" onclick="return TABLE1_onclick()">
                <tr>
                    <td style="height: 23px; width: 200px;">
                        Albums:</td>
                    <td style="height: 23px; width: 200px;">
                    </td>
                    <td style="width: 200px; height: 23px">
                    </td>
                    <td style="width: 200px; height: 23px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px">
                        <asp:LinkButton ID="LinkUser" runat="server" OnClick="LinkUser_Click">User Created</asp:LinkButton></td>
                    <td style="width: 166px">
                        <asp:LinkButton ID="LinkDate" runat="server" OnClick="LinkDate_Click">By Date</asp:LinkButton></td>
                        <td style="width: 125px"> 
                            <asp:LinkButton ID="LinkLocation" runat="server" OnClick="LinkLocation_Click">By Location</asp:LinkButton></td>
                    <td style="width: 181px">
                        <asp:LinkButton ID="LinkPortrait" runat="server" OnClick="LinkPortrait_Click">With Portraits</asp:LinkButton></td>
                </tr>
                
            </table>
        </p>
    
    </div>
        <hr style="width: 800px; font-family: Calibri;" />
        <br />
        <table style="width: 800px; height: 400px; font-family: Calibri;">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="LabAlbum" runat="server"></asp:Label></td>
                <td style="width: 400px">
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    </td>
                <td style="width: 400px">
                    <asp:Image ID="Image2" runat="server" Height="400px" ImageUrl="~/App_Data/google map.gif"
                        Width="400px" /></td>
            </tr>
            <tr>
                <td style="width: 400px">
                    </td>
                <td style="width: 400px">
                    <asp:Image ID="Image1" runat="server" Height="400px" ImageUrl="~/App_Data/StatueLiberty.jpg" /></td>
            </tr>
        </table>
    </form>
</body>
</html>

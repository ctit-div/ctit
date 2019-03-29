<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CS.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        input[type=text]
        {
            margin-bottom: 10pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlTextBoxes" runat="server">
    </asp:Panel>
    <hr />
        <asp:TextBox ID="TextBoxCount" runat="server"></asp:TextBox>
    <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="AddTextBox" />
    <asp:Button ID="btnGet" runat="server" Text="Account Ex." OnClick="GetTextBoxValues" />
        <asp:TextBox ID="TextBoxLength" runat="server"></asp:TextBox>
    </form>
</body>
</html>

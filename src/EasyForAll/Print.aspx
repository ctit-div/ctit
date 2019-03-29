<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Print" %>

<%@ Register assembly="Telerik.ReportViewer.WebForms, Version=7.2.14.127, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" namespace="Telerik.ReportViewer.WebForms" tagprefix="telerik" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
            <LocalReport ReportPath="RptCurrency.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="MyDataset" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="MyDatasetTableAdapters.tCurrencyTableAdapter"></asp:ObjectDataSource>
    </form>
</body>
</html>

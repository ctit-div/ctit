<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcArchivedInvoices.ascx.cs" Inherits="UcArchivedInvoices" %>
<script type="text/javascript" language="JavaScript">
    var iCount = 0;
    function CheckChange(ckb) {
        if (ckb.checked) {
            iCount += 1;
            document.getElementById("lblTotalPatientsSelected").innerText = iCount;
            document.getElementById(ckb).checked = true;
        }
        else {
            iCount -= 1;
            document.getElementById("lblTotalPatientsSelected").innerText = iCount;
            document.getElementById(ckb).checked = false;
            document.getElementById("chkAll").checked = false;
        }
    }
    function SelectAllCheckboxes(spanChk) {
        // Added as ASPX uses SPAN for checkbox
        var oItem = spanChk.children;
        var theBox = (spanChk.type == "checkbox") ?
            spanChk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (i = 0; i < elm.length; i++)
            if (elm[i].type == "checkbox" &&
                elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
    }
</script>
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="table table-bordered"
    AlternatingRowStyle-CssClass="alt"
    PagerStyle-CssClass="pgr"
    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="InvoiceNo"
    EmptyDataText="No Items List" Width="90%" DataSourceID="SqlDataSource1">
    <AlternatingRowStyle CssClass="alt" HorizontalAlign="left"></AlternatingRowStyle>
    <Columns>

        <%--<asp:TemplateField Visible="true">
            <HeaderTemplate>
                <input id="chkAll" onclick="javascript: SelectAllCheckboxes(this);"
                    runat="server" type="checkbox" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="ck" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:BoundField DataField="StatusText_Ar" HeaderText="Status Text" />
        <asp:TemplateField HeaderText="Enter" Visible="False">
            <ItemTemplate>
                <asp:TextBox ID="TxtAmount" runat="server" Width="80%"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>


        <asp:TemplateField HeaderText="Notes">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Invoice Date" SortExpression="InvoiceDate">
            <ItemTemplate>
                <asp:Label ID="lblInvoiceDate" runat="server" SkinID="txt"
                    Text='<%# Eval("InvoiceDate") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Required Amount" SortExpression="InvoiceTotal">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Eval("InvoiceTotal") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Invoice No" SortExpression="InvoiceNo">
            <ItemTemplate>
                <asp:Label ID="lblInvoiceNo" runat="server" SkinID="lbl"
                    Text='<%# Eval("InvoiceNo") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle ForeColor="#0099CC" HorizontalAlign="left" />
    <PagerStyle CssClass="pgr"></PagerStyle>
    <RowStyle CssClass="Lbl" HorizontalAlign="left" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT distinct QuotationId, CompanyId, BranchId, InvoiceNo, InvoiceDate, InvoiceType, PricingType, CustomerId, DiscountAmount, DiscountPercent, InvoiceTotal, Notes, IsActive, StatusText_Ar, QuotationStatusId FROM View_Quotation  WHERE (QuotationStatusId= 8)" UpdateCommand="UPDATE tQuotations SET IsActive = 1, Notes = @Note WHERE (InvoiceNo = @InvoiceNo)">
    <UpdateParameters>
        <asp:SessionParameter Name="Note" SessionField="Note" />
        <asp:SessionParameter Name="InvoiceNo" SessionField="InvoiceNo" />
    </UpdateParameters>
</asp:SqlDataSource>
<br />

<br />

<div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">

   <%-- <div class="row">
        <div class="col-md-2">
            <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="btnUpdate_Click" Visible="true"><i class="glyphicon glyphicon-floppy-save"></i> Confirm Payment</asp:LinkButton>
        </div>
          <div class="col-md-2">
            <asp:LinkButton ID="BtnArchive" CssClass="btn btn-primary" runat="server" OnClick="btnArchive_Click" Visible="true"><i class="glyphicon glyphicon-floppy-save"></i>Handed & Archive</asp:LinkButton>
        </div>
        <div class="col-md-8">

            <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>
        </div>
    </div>--%>
</div>

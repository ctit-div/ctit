<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcMyOrders.ascx.cs" Inherits="UcMyOrders" %>
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
    EmptyDataText="No Items List" Width="100%">
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
    <Columns>
        <asp:TemplateField Visible="true">
            <HeaderTemplate>
                <input id="chkAll" onclick="javascript: SelectAllCheckboxes(this);"
                    runat="server" type="checkbox" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="ck" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Invoice No" SortExpression="InvoiceNo">
            <ItemTemplate>
                <asp:Label ID="lblInvoiceNo" runat="server" SkinID="lbl"
                    Text='<%# Eval("InvoiceNo") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Invoice Date" SortExpression="InvoiceDate">
            <ItemTemplate>
                <asp:Label ID="lblInvoiceDate" runat="server" SkinID="txt"
                    Text='<%# Convert.ToDateTime(Eval("InvoiceDate")).ToString("dd/MM/yyyy") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Required Amount" SortExpression="InvoiceTotal">
            <ItemTemplate>
                <asp:TextBox ID="txtInvoiceTotal" runat="server" Enabled="false"
                    Text='<%# Eval("InvoiceTotal") %>' Width="80%"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>

          <asp:TemplateField HeaderText="Enter">
                            <ItemTemplate>
                                <asp:TextBox ID="TxtAmount" Placeholder="Enter the amount" runat="server" Width="80%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>


        <asp:TemplateField HeaderText="Transaction No">
            <ItemTemplate>
                <asp:TextBox ID="TxtTransNo" Placeholder="Enter Transaction No" runat="server"
                    Width="80%"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="StatusText_Ar" HeaderText="Status Text" />

    </Columns>
    <HeaderStyle ForeColor="#3399FF" />
    <PagerStyle CssClass="pgr"></PagerStyle>
    <RowStyle CssClass="Lbl" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT distinct QuotationId, CompanyId, BranchId, InvoiceNo, InvoiceDate, InvoiceType, PricingType, CustomerId, DiscountAmount, DiscountPercent, InvoiceTotal, Notes, IsActive,StatusText_Ar FROM View_Quotation WHERE (CustomerId = @CustomerId) AND (QuotationStatusId not in(8))" UpdateCommand="UPDATE tQuotations SET IsActive = 1, Notes = @Note WHERE (InvoiceNo = @InvoiceNo)">
    <SelectParameters>
        <asp:SessionParameter Name="CustomerId" SessionField="UserCode" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:SessionParameter Name="Note" SessionField="Note" />
        <asp:SessionParameter Name="InvoiceNo" SessionField="InvoiceNo" />
    </UpdateParameters>
</asp:SqlDataSource>
<br />

<br />

<div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">

    <div class="row">
        <div class="col-md-2">
            <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="btnUpdate_Click"><i class="glyphicon glyphicon-floppy-save"></i> Pay</asp:LinkButton>
        </div>
         <div class="col-md-10">
           
               <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>
        </div>
    </div>
</div>

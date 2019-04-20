<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcPreviousInvoice.ascx.cs" Inherits="UcPreviousInvoice" %>
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="table table-bordered"
    AlternatingRowStyle-CssClass="alt"
    PagerStyle-CssClass="pgr"
    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="InvoiceNo"
    EmptyDataText="No Items List" Width="90%">
<AlternatingRowStyle CssClass="alt" HorizontalAlign="left"></AlternatingRowStyle>
    <Columns>
        <asp:TemplateField Visible="False">
            <HeaderTemplate>
                <input id="chkAll" onclick="javascript: SelectAllCheckboxes(this);"
                    runat="server" type="checkbox" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="ck" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>

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
                    Text='<%# Convert.ToDateTime(Eval("InvoiceDate")).ToString("dd/MM/yyyy") %>'></asp:Label>
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

        <asp:BoundField DataField="StatusText_Ar" HeaderText="Status Text" />

    </Columns>
    <HeaderStyle ForeColor="#3399FF" />
    <PagerStyle CssClass="pgr"></PagerStyle>
    <RowStyle CssClass="Lbl" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT distinct QuotationId, CompanyId, InvoiceNo, InvoiceDate, InvoiceType, PricingType, UserId, DiscountAmount, DiscountPercent, InvoiceTotal, Notes, IsActive, StatusText_Ar, QuotationStatusId FROM View_Quotation  WHERE (UserId = @CustomerId)  AND ((QuotationStatusId = 8) OR (QuotationStatusId = 6))" UpdateCommand="UPDATE tQuotations SET IsActive = 1, Notes = @Note WHERE (InvoiceNo = @InvoiceNo)">
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
            <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" Visible="false"><i class="glyphicon glyphicon-floppy-save"></i> Pay</asp:LinkButton>
        </div>
         <div class="col-md-10">
           
               <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>
        </div>
    </div>
</div>

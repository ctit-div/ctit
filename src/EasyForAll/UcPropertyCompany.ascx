<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcPropertyCompany.ascx.cs" Inherits="UcPropertyCompany" %>



<asp:UpdatePanel ID="hhh222" runat="server">
    <ContentTemplate>


        <div class="row">

            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="القسم"></asp:Label>

            </div>
            <div class="col-md-4">

                <asp:DropDownList ID="Ddlist" runat="server" CssClass="form-control" OnSelectedIndexChanged="Ddlist_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
               </div>
                  <div class="col-md-1">
                <asp:ImageButton ID="ImageButtonDown" runat="server" ImageUrl="~/img/down-arrow.png" Width="45px" CausesValidation="False" OnClick="ImageButtonDown_Click" />
               </div>
                  <div class="col-md-1">
                      <asp:ImageButton ID="ImageButtonUp" runat="server" ImageUrl="~/img/Arrow_Up.png" Width="45px" CausesValidation="False" OnClick="ImageButtonUp_Click" />
            </div>
            <div class="col-md-4">
                <asp:Label ID="LabelErrorMessage" runat="server" Font-Size="XX-Large" CssClass="LblMessage"></asp:Label>
                
                <asp:SqlDataSource ID="SqlDataSourceCompany" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT CompanyID, CMPCompanyName, CompanyIDParent, IsActive FROM tCompanys WHERE (IsActive = 1) and CompanyIDParent=0"></asp:SqlDataSource>

                <asp:SqlDataSource ID="SqlDataSourceDivision" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT CompanyID, CMPCompanyName, CompanyIDParent, IsActive FROM tCompanys WHERE (IsActive = 1)"></asp:SqlDataSource>
            </div>

        </div>
      

    </ContentTemplate>

</asp:UpdatePanel>

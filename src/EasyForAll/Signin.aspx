<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Signin.aspx.cs" Inherits="Signin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div>
                  
                   <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>
            <br /><br /><br /><br /><br />
   <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">
        <div class="row">
            <div class="col-md-4 offset-md-4 rcorners2" style="align-content:center; text-align: center;">
                  <div>
                 
                      <img src="Images/WEC-Logo.png" width:"30%" width="300" /><br />
                     <%--  <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" CssClass="Lbl" Text="Water Equipment Center"></asp:Label>--%>
                </div>
               


            </div>
        </div>
        <div class="row">
            <div class="col-md-10 offset-md-1">

            </div>
        </div>
        <div  class=" col-md-4 offset-md-4 rcorners3" style="align-content:center; text-align: center;">
        <div class="row">


             <div class="col-md-10 offset-md-1">
                <div>
                  <%--  <asp:Label ID="Label6" runat="server" CssClass="Lbl" Text="Email"></asp:Label>--%>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="LblMessage" DisplayMode="List" Font-Size="Small" />
                </div>
                <asp:TextBox ID="_EmailText" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                    ControlToValidate="_EmailText" CssClass="LblMessage"
                    ErrorMessage="Email is required">*</asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="row">

            <div class="col-md-10 offset-md-1">
                <div>
                   <%-- <asp:Label ID="Label7" runat="server" CssClass="Lbl" Text="Password"></asp:Label>--%>
                </div>
                <asp:TextBox ID="_PasswordText" runat="server" placeholder="Enter Password" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                    ControlToValidate="_PasswordText" CssClass="LblMessage"
                    ErrorMessage="Password is required">*</asp:RequiredFieldValidator>

            </div>


        </div>
        <div class="row">

            <div class="col-md-10 offset-md-1">


                <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" defaultbutton="BtnSave" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Login</asp:LinkButton>
                &nbsp;&nbsp;&nbsp;  
 
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;            

                 <br />
                <asp:Label ID="LblMessage" runat="server" CssClass="LblMessage"></asp:Label>

            </div>


        </div>
          </div>

    </div>
       </ContentTemplate>

    </asp:UpdatePanel>
        </div>

</asp:Content>


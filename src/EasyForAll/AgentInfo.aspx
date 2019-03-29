<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentInfo.aspx.cs" Inherits="AgentInfo" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalpopup {
            background-color: #ADADAD;
            filter: Alpha(Opacity=70);
            opacity: 0.70;
            -moz-opacity: 0.70;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

           <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="Agent Info" CssClass="Title"></asp:Label>
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>)

                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                            CssClass="LblMessage" DisplayMode="List" Font-Size="Small" />

                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="Label2" runat="server" CssClass="Lbl" Text="Agent Name"></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="TxtUserName" CssClass="LblMessage"
                            ErrorMessage="User Name is required">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="Mobile"></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="TxtMobile" CssClass="LblMessage"
                            ErrorMessage="Mobile is required">*</asp:RequiredFieldValidator>

                        <ajaxToolkit:FilteredTextBoxExtender ID="TxtMobile_FilteredTextBoxExtender" runat="server" TargetControlID="TxtMobile" ValidChars="1234567890" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="Label6" runat="server" CssClass="Lbl" Text="Email"></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                            ControlToValidate="TxtEmail" CssClass="LblMessage"
                            ErrorMessage="Email is required">*</asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtEmail" CssClass="LblMsg" ErrorMessage="Email not right" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="Label10" runat="server" CssClass="Lbl" Text="Details"></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtAddress" runat="server" CssClass="form-control" Height="70px" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ControlToValidate="TxtAddress" CssClass="LblMessage"
                            ErrorMessage="Address is required">*</asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="Label9" runat="server" CssClass="Lbl" Text="Status"></asp:Label>
                        </div>
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">Active</asp:ListItem>
                            <asp:ListItem Value="1">Not Active</asp:ListItem>
                        </asp:RadioButtonList>


                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div>
                            <asp:Label ID="Label4" runat="server" CssClass="Lbl" Text="Upload Image"></asp:Label>
                        </div>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                        <asp:HyperLink ID="H1" runat="server" Target="_blank">View Logo</asp:HyperLink>

                    </div>

                    <div class="col-md-6">
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">


                        <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>
                    </div>
                    <div class="col-md-2">
                        <asp:LinkButton ID="BtnPrint" CssClass="btn btn-success" CausesValidation="false" runat="server" OnClick="BtnPrint_Click"><i class="glyphicon glyphicon-floppy-save"></i> Print</asp:LinkButton>
                    </div>
                    <div class="col-md-2">
                        <asp:LinkButton ID="BtnReset" CssClass="btn btn-success" CausesValidation="false" runat="server" OnClick="BtnReset_Click"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>

                    </div>
                    <div class="col-md-2">
                        <asp:LinkButton ID="BtnEdit" CssClass="btn btn-primary" runat="server" OnClick="BtnEdit_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>

                    </div>
                    <div class="col-md-2">
                        <asp:LinkButton ID="BtnDelete" CssClass="btn btn-success" runat="server" OnClick="BtnDelete_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Delete</asp:LinkButton>



                    </div>



                </div>

                <div class="row">
                    <div class="col-md-3">

                        <br />

                        <asp:Label ID="LblMessage" runat="server" CssClass="LblMessage"></asp:Label>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM [GroceryInfoTable]" ProviderName="<%$ ConnectionStrings:FinanceConnStr.ProviderName %>"></asp:SqlDataSource>
                        <br />
                    </div>
                </div>




                <div class="row">
                    <div class="col-md-12">

                        <div class="panel panel-grey margin-bottom-40">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-credit-card"></i>All Users Names</h3>
                            </div>
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>


                                        <asp:Panel ID="pnlTextBoxes" runat="server" Visible="false">
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="BtnEdit" />
                                        <asp:PostBackTrigger ControlID="BtnSave" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:DataList ID="DataList1" runat="server" DataKeyField="AgentCode"
                                    OnSelectedIndexChanged="DataList1_SelectedIndexChanged" RepeatColumns="2"
                                    Width="60%">
                                    <ItemTemplate>
                                        <table align="left" width="500">
                                            <tr class="auto-style1">
                                                <td class="style5">User Code:</td>
                                                <td class="style4">
                                                    <asp:Label ID="AgentCodeLabel" runat="server" Text='<%# Eval("AgentCode") %>' />
                                                </td>
                                            </tr>
                                            <tr class="auto-style1">
                                                <td class="style5">User Name:
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("UserName") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style2">Mobile:
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="MobileLabel" runat="server" Text='<%# Eval("Mobile") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style2">Email:</td>
                                                <td class="style4">
                                                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                                                </td>
                                            </tr>



                                            <tr>
                                                <td class="auto-style2">Status: </td>
                                                <td class="style4">
                                                    <asp:Label ID="StatusLabel1" runat="server" Text='<%# Eval("Status") %>' />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="auto-style2">Details:</td>
                                                <td class="style4">
                                                    <asp:Label ID="AddressLabel" runat="server" Text='<%# Eval("Address") %>' />
                                                </td>
                                            </tr>



                                            <tr>
                                                <td class="auto-style2">
                                                    <%-- <a href='<%# DataBinder.Eval(Container.DataItem,"ImageURL") %>' target="_blank">
                                        <asp:Image ID="picture" runat="server" Height="100px" ImageUrl='<%# Eval("ImageURL") %>' Width="100px" />
                                        </a>--%>
                                                    <a href='<%# DataBinder.Eval(Container.DataItem,"LogoImage") %>' target="_blank">
                                                        <asp:Image ID="picture" runat="server" Height="100px" ImageUrl='<%# Eval("LogoImage") %>' Width="100px" />
                                                    </a>
                                                </td>
                                                <td class="style4">
                                                    <asp:ImageButton ID="btn" runat="server" CausesValidation="False" CommandName="select" Height="25" ImageUrl="~/Images/edit-button-blue-hi.png" Width="45" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style5">
                                                    <%--   <asp:Label ID="CategoryCodeLabel" runat="server" Text='<%# Eval("CategoryCode") %>' Visible="False" />--%>
                                                </td>
                                                <td class="style4">&nbsp;</td>
                                            </tr>
                                        </table>

                                        <br />
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>





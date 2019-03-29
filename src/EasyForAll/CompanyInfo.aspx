<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CompanyInfo.aspx.cs" Inherits="CompanyInfo" %>

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
            <div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="Company Info" CssClass="Title"></asp:Label>
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                        )
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM tCompany"></asp:SqlDataSource>

                        <hr />
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div>
                            <asp:Label ID="LabelCompanyName" runat="server" CssClass="Lbl" Text="Company Name"></asp:Label>
                        </div>
                        <asp:TextBox ID="_CMPCompanyNameText" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_CMPCompanyNameText" CssClass="LblMessage" ErrorMessage="Company Name is required"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-3">
                        <div>
                        </div>
                    </div>

                    <div class="col-md-2">
                    </div>
                </div>


                <%--<div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div>
                            <asp:Label ID="LabelCMPShortName" runat="server" CssClass="Lbl" Text="Company Short Name"></asp:Label>
                        </div>
                        <asp:TextBox ID="_CMPShortNameText" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_CMPShortNameText" CssClass="LblMessage" ErrorMessage="Company Short Name is required"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-3">
                        <div>
                        </div>
                    </div>

                    <div class="col-md-2">
                    </div>
                </div>--%>

                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div>
                            <asp:Label ID="LabelCMPAddress" runat="server" CssClass="Lbl" Text="Company Address "></asp:Label>
                        </div>
                        <asp:TextBox ID="_CMPAddressText" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="_CMPAddressText" CssClass="LblMessage" ErrorMessage="Company Address is required"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-3">
                        <div>
                        </div>
                    </div>

                    <div class="col-md-2">
                    </div>
                </div>


                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div>
                            <asp:Label ID="LabelEmail" runat="server" CssClass="Lbl" Text="Company Email"></asp:Label>
                        </div>
                        <asp:TextBox ID="_EmailText" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="_EmailText" CssClass="LblMessage" ErrorMessage="Company Email is required"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-3">
                        <div>
                        </div>
                    </div>

                    <div class="col-md-2">
                    </div>
                </div>


                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div>
                            <asp:Label ID="LabelMobile" runat="server" CssClass="Lbl" Text="Company Mobile"></asp:Label>
                        </div>
                        <asp:TextBox ID="_MobileText" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="_MobileText" CssClass="LblMessage" ErrorMessage="Mobile is required"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-3">
                        <div>
                        </div>
                    </div>

                    <div class="col-md-2">
                    </div>
                </div>

                <%--<div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div>
                            <asp:Label ID="LabelCMPAccountingMonth" runat="server" CssClass="Lbl" Text="Accounting Month"></asp:Label>
                        </div>
                        <asp:DropDownList ID="_CMPAccountingMonthText" runat="server" CssClass="form-control">
                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                            <asp:ListItem Value="1">Jan</asp:ListItem>
                            <asp:ListItem Value="2">Feb</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">Aug</asp:ListItem>
                            <asp:ListItem Value="9">Sept</asp:ListItem>
                            <asp:ListItem Value="10">Oct</asp:ListItem>
                            <asp:ListItem Value="11">Nov</asp:ListItem>
                            <asp:ListItem Value="12">Dec</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="_CMPAccountingMonthText" CssClass="LblMessage" ErrorMessage="Accounting Month is required"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-3">
                        <div>
                        </div>
                    </div>

                    <div class="col-md-2">
                    </div>
                </div>--%>

                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div>


                            <br />

                            <asp:Label ID="LabelPassword" runat="server" CssClass="Lbl" Text="Password"></asp:Label>
                        </div>
                        <asp:TextBox ID="_PasswordText" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="_PasswordText" CssClass="LblMessage" ErrorMessage="Password is required"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-3">
                        <div>
                        </div>
                    </div>

                    <div class="col-md-2">
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-6">


                        <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;  
 <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false" runat="server" OnClick="BtnClear_Click"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
      <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary" runat="server" OnClick="BtnUpdate_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>

                        &nbsp;&nbsp;
      <asp:LinkButton ID="BtnChangeStatus" CssClass="btn btn-success" runat="server" OnClick="BtnChangeStatus_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Change Status</asp:LinkButton>

                        <br />

                    </div>


                </div>

                <div class="row">
                    <div class="col-md-3">

                        <br />

                        <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>
                        <br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <asp:Label ID="LabelId" runat="server" CssClass="Lbl" Visible="False"></asp:Label>
                    </div>

                </div>


                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">

                        <div class="panel panel-grey margin-bottom-40">
                            <div class="panel-heading">
                                <%if (Session["dir"].ToString() == "ltr")
                                    {
                                %>
                                <h3 class="panel-title"><i class="icon-globe"></i>All Companys</h3>

                                <%}
                                else
                                { %>
                                <h3 class="panel-title"><i class="icon-globe"></i>جميع الشركات</h3>

                                <%} %>
                            </div>
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>


                                        <asp:Panel ID="pnlTextBoxes" runat="server" Visible="false">
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:GridView ID="GvCompany" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True"
                                    CssClass="table table-bordered"
                                    AlternatingRowStyle-CssClass="alt"
                                    PagerStyle-CssClass="pgr" DataKeyNames="CompanyId" PageSize="5" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GvCompany_SelectedIndexChanged" OnRowDeleting="GvCompany_RowDeleting" OnRowDeleted="GvCompany_RowDeleted">
                                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>


                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />


                                        <asp:TemplateField ShowHeader="False" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    CommandArgument='<%# Bind("CompanyId") %>' CssClass="btn btn-danger btn-xs" OnClientClick="return confirm('Are you sure to delete, confirm deleting?');"> Delete<i class="icon-trash" style="font-size:16px;"> </i> </asp:LinkButton>
                                                &nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="CompanyID" HeaderText="Company ID" InsertVisible="False" ReadOnly="True" SortExpression="CompanyID" />
                                        <asp:BoundField DataField="CMPCompanyName" HeaderText="Company Name" SortExpression="CMPCompanyName" />

                                        <asp:BoundField DataField="CMPShortName" Visible="false" HeaderText="Short Name" />
                                        <asp:BoundField DataField="CMPAddress" HeaderText="Address" />
                                        <asp:BoundField DataField="CMPEmail" HeaderText="Email" />
                                        <asp:BoundField DataField="CMPPhone" HeaderText="Mobile" />



                                    </Columns>

                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <RowStyle CssClass="Lbl" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>


            </div>

        </ContentTemplate>

    </asp:UpdatePanel>


</asp:Content>



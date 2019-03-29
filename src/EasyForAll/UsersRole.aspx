<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UsersRole.aspx.cs" Inherits="UsersRole" %>


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
                    <div class="col-md-12">
                        <asp:Label ID="Label1" runat="server" Text="UserRoles Name" CssClass="Title"></asp:Label>
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                        )
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">

                        <asp:SqlDataSource ID="SqlDataSourceCompany" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT [CompanyID], [CMPCompanyName], [CMPShortName] FROM [tCompanys]"></asp:SqlDataSource>

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM tUserRoles"></asp:SqlDataSource>

                        <hr />
                    </div>
                </div>



                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="Label2" runat="server" CssClass="Lbl" Text="UserRole Name "></asp:Label>
                        </div>
                        <asp:TextBox ID="_UserRoleNameText" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_UserRoleNameText" CssClass="LblMessage" ErrorMessage="UserRole Name is required"></asp:RequiredFieldValidator>

                    </div>


                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="Description"></asp:Label>
                        </div>
                        <asp:TextBox ID="_DescriptionText" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_DescriptionText" CssClass="LblMessage" ErrorMessage="Description Name is required"></asp:RequiredFieldValidator>

                    </div>

                </div>


                <div class="row">
                    <div class="col-md-2">


                        <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>
                    </div>

                    <div class="col-md-2">
                        <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false" runat="server" OnClick="BtnClear_Click"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
                    </div>
                    <div class="col-md-2">
                        <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary" runat="server" OnClick="BtnUpdate_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>
                    </div>
                    <div class="col-md-2">
                    </div>



                </div>

                <div class="row">
                    <div class="col-md-6">


                        <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>

                    </div>

                    <div class="col-md-6">
                        <asp:Label ID="LabelId" runat="server" CssClass="Lbl" Visible="False"></asp:Label>
                    </div>

                </div>




                <div class="row">
                    <div class="col-md-12">

                        <div class="panel panel-grey margin-bottom-40">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="icon-globe"></i>All UserRole Names</h3>
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
                                    PagerStyle-CssClass="pgr" DataKeyNames="UserRoleId" PageSize="5" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GvCompany_SelectedIndexChanged" OnRowDeleting="GvCompany_RowDeleting" OnRowDeleted="GvCompany_RowDeleted">
                                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                                    <Columns>
                                        <%--<asp:CommandField ShowSelectButton="True" />--%>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="UserRoleName" HeaderText="UserRole Name" SortExpression="UserRoleName" />

                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />


                                        <asp:TemplateField HeaderText="UserRoleId" SortExpression="UserRoleId" Visible="False">
                                            <EditItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("UserRoleId") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("UserRoleId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    CommandArgument='<%# Bind("UserRoleId") %>' CssClass="btn btn-danger btn-xs" OnClientClick="return confirm('Are you sure to delete, confirm deleting UserRole?');"> Delete <i class="icon-trash" style="font-size:16px;"> </i> </asp:LinkButton>
                                                &nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
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




<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FinancialYear.aspx.cs" Inherits="FinancialYear" %>

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
                    <div class="col-md-12">
                        <asp:Label ID="Label1" runat="server" Text="Financial Year" CssClass="Title"></asp:Label>
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                        )

                        <asp:SqlDataSource ID="SqlDataSourceCompany" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT [CompanyID], [CMPCompanyName], [CMPShortName] FROM [tCompanys]"></asp:SqlDataSource>

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM View_FinancialYear"></asp:SqlDataSource>

                        <hr />
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="Label2" runat="server" CssClass="Lbl" Text="Company Name"></asp:Label>
                        </div>
                        <asp:DropDownList ID="_CMPlist" runat="server" AutoPostBack="True" CssClass="form-control" DataSourceID="SqlDataSourceCompany" DataTextField="CMPCompanyName" DataValueField="CompanyID" OnSelectedIndexChanged="_CMPlist_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>

                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="Start Date"></asp:Label>
                        </div>
                        <asp:TextBox ID="_StartDateText" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/Calendar.gif" OnClick="ImageButton1_Click" CausesValidation="False" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_StartDateText" CssClass="LblMessage" ErrorMessage="Start Date is required"></asp:RequiredFieldValidator>

                        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" Visible="False"></asp:Calendar>

                    </div>



                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="Label4" runat="server" CssClass="Lbl" Text="End Date"></asp:Label>
                        </div>
                        <asp:TextBox ID="_EndDateText" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/img/Calendar.gif" OnClick="ImageButton2_Click" CausesValidation="False" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_EndDateText" CssClass="LblMessage" ErrorMessage="End Date is required"></asp:RequiredFieldValidator>

                        <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged" Visible="False"></asp:Calendar>

                    </div>

                </div>
                <div class="row">
                    <div class="col-md-3">


                        <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>

                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false" runat="server" OnClick="BtnClear_Click"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary" runat="server" OnClick="BtnUpdate_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton ID="BtnChangeStatus" CssClass="btn btn-success" runat="server" OnClick="BtnChangeStatus_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Change Status</asp:LinkButton>



                    </div>


                </div>

                <div class="row">
                    <div class="col-md-9">


                        <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>
                        <div class="col-md-3">
                            <asp:Label ID="LabelId" runat="server" CssClass="Lbl" Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-12">

                        <div class="panel panel-grey margin-bottom-40">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="icon-globe"></i>All Financial Year</h3>
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
                                    PagerStyle-CssClass="pgr" DataKeyNames="FinYearID" PageSize="5" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GvCompany_SelectedIndexChanged">
                                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                                    <Columns>
                                        <%--<asp:CommandField ShowSelectButton="True" />--%>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:yyyy/MM/dd}" HtmlEncode="false" SortExpression="StartDate" />

                                        <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:yyyy/MM/dd}" HtmlEncode="false" SortExpression="EndDate" />

                                        <asp:BoundField DataField="CMPCompanyName" HeaderText="Company Name" SortExpression="CMPCompanyName" />
                                        <asp:TemplateField HeaderText="FinYearID" SortExpression="FinYearID" Visible="False">
                                            <EditItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("FinYearID") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("FinYearID")%>'></asp:Label>
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



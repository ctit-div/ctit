<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Cities.aspx.cs" Inherits="Cities" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <style>

        
.form-control {
  display: block;
  width: 90%;
  padding: 0.5rem 0.75rem;
  font-size: 0.875rem;
  line-height: 1.25;
  color: #607d8b;
  background-color: #fff;
  background-image: none;
  background-clip: padding-box;
  border: 1px solid rgba(0, 0, 0, 0.15);
  transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s; }
  .form-control::-ms-expand {
    background-color: transparent;
    border: 0; }
  .form-control:focus {
    color: #607d8b;
    background-color: #fff;
    border-color: #66afe9;
    outline: none; }
  .form-control::placeholder {
    color: #999;
    opacity: 1; }
  .form-control:disabled, .form-control[readonly] {
    background-color: #cfd8dc;
    opacity: 1; }
  .form-control:disabled {
    cursor: not-allowed; }

select.form-control:not([size]):not([multiple]) {
  height: 2.3125rem; }
select.form-control:focus::-ms-value {
  color: #607d8b;
  background-color: #fff; }

.form-control-file,
.form-control-range {
  display: block; }

    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">

                    <ol class="breadcrumb">
                        <%-- <li class="breadcrumb-item">
                         <a href="#">معلومات المدن</a>
                        </li>--%>
                        <li class="breadcrumb-item active">معلومات المدن</li>
                    </ol>

                    
                    <asp:UpdatePanel ID="hhh222" runat="server">
                        <ContentTemplate>


                            <%--  <div class="row">
                                <div class="col-md-6">
                                   <%-- <asp:Label ID="Label1" runat="server" Text="معلومات المدن " CssClass="Title"></asp:Label--%>
                            <%-- &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                        )
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="LblMessage" />
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM View_Cities"></asp:SqlDataSource>
                                    <hr />
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-md-2">

                                    <asp:Label ID="LabelCityName_Ar" runat="server" CssClass="Lbl" Text="اسم المدينة "></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="_CityName_ArText" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_CityName_ArText" CssClass="LblMessage" ErrorMessage="اسم المدينة مطلوب">*</asp:RequiredFieldValidator>

                                </div>



                            </div>


                            <div class="row">
                                <div class="col-md-4">


                                    <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> حفظ</asp:LinkButton>
                                </div>
                                <div class="col-md-4">
                                    <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false" runat="server" OnClick="BtnClear_Click"><i class="glyphicon glyphicon-refresh"></i> مسح</asp:LinkButton>
                                </div>
                                <div class="col-md-4">
                                    <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary" runat="server" OnClick="BtnUpdate_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> تعديل</asp:LinkButton>
                                </div>


                            </div>

                            <div class="row">
                                <div class="col-md-3">

                                    <asp:Label ID="LabelId" runat="server" CssClass="Lbl" Visible="False"></asp:Label>

                                    <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>

                                </div>
                            </div>




                            <div class="row">
                                <div class="col-md-12">

                                    <div class="panel panel-grey margin-bottom-40">
                                        <div class="panel-heading">
                                            <h3 class="panel-title"><i class="icon-globe"></i>قائمة المدن</h3>
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
                                                PagerStyle-CssClass="pgr" DataKeyNames="CityId" PageSize="5" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GvCompany_SelectedIndexChanged" OnRowDeleting="GvCompany_RowDeleting" OnRowDeleted="GvCompany_RowDeleted">
                                                <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" />
                                                    <asp:TemplateField ShowHeader="False" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                                CommandArgument='<%# Bind("CityId") %>' CssClass="btn btn-danger btn-xs" OnClientClick="return confirm('هل انت متأكد من الحذف, تأكيد الحذف?');"> حذف<i class="icon-trash" style="font-size:16px;"> </i> </asp:LinkButton>
                                                            &nbsp;
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:CommandField ShowSelectButton="True" />--%>
                                                    <asp:BoundField DataField="CityId" HeaderText="كود المدينة" InsertVisible="False" ReadOnly="True" SortExpression="CityId" />
                                                    <asp:BoundField DataField="CityName_Ar" HeaderText="اسم المدينة" SortExpression="CityName_Ar" />


                                                </Columns>
                                                <PagerStyle CssClass="pgr"></PagerStyle>
                                                <RowStyle CssClass="Lbl" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>



                        </ContentTemplate>

                    </asp:UpdatePanel>
                    <a class="scroll-to-top rounded" href="#page-top">
                        <i class="fa fa-angle-up"></i>
                    </a>

                </div>
            </div>
        </div>

    </div>
</asp:Content>


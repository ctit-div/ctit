<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CustomerEdit.aspx.cs" Inherits="CustomerEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>
           <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="معلومات العميل " CssClass="Title"></asp:Label>
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                        )
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="LblMessage" />
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM View_tUsers"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM tCity"></asp:SqlDataSource>
                        <hr />
                    </div>
                </div>

                <div class="row">

                    <div class="col-md-2">

                        <asp:Label ID="LabelCompanyName" runat="server" CssClass="Lbl" Text="الاسم "></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="_CustomerNameText" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_CustomerNameText" CssClass="LblMessage" ErrorMessage="اسم العميل مطلوب">*</asp:RequiredFieldValidator>

                    </div>

                    <div class="col-md-2">

                        <asp:Label ID="Label2" runat="server" CssClass="Lbl" Text="الجوال"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="_MobileText" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="_MobileText" CssClass="LblMessage" ErrorMessage="رقم الموبايل مطلوب">*</asp:RequiredFieldValidator>

                    </div>


                </div>

                <div class="row">

                    <div class="col-md-2">

                        <asp:Label ID="Label4" runat="server" CssClass="Lbl" Text="كلمة المرور"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="_PasswordText" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_PasswordText" CssClass="LblMessage" ErrorMessage="كلمة المرور مطلوبة">*</asp:RequiredFieldValidator>

                    </div>


                    <div class="col-md-2">

                        <asp:Label ID="Label8" runat="server" CssClass="Lbl" Text="تأكيد كلمة المرور"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="_PasswordConfirmText" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="_PasswordConfirmText" CssClass="LblMessage" ErrorMessage="تأكيد كلمة المرور مطلوبة">*</asp:RequiredFieldValidator>

                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="_PasswordText" ControlToValidate="_PasswordConfirmText" CssClass="LblMessage" ErrorMessage="تأكد من كلمة المرور">*</asp:CompareValidator>

                    </div>

                </div>







                <div class="row">
                    <div class="col-md-2">

                        <asp:Label ID="Label5" runat="server" CssClass="Lbl" Text="الايميل"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="_EmailText" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="_EmailText" CssClass="LblMessage" ErrorMessage="الايميل مطلوب">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="_EmailText" CssClass="LblMessage" ErrorMessage="الايميل غير صحيح" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </div>


                    <div class="col-md-2">

                        <asp:Label ID="Label6" runat="server" CssClass="Lbl" Text="تأكيد الايميل"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="_EmailConfirmText" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="_EmailText" ControlToValidate="_EmailConfirmText" CssClass="LblMessage" ErrorMessage="ادخال الايميل غير صحيح">*</asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="_EmailConfirmText" CssClass="LblMessage" ErrorMessage="تأكيد الايميل مطلوب">*</asp:RequiredFieldValidator>

                    </div>

                    </div>
                    <div class="row">


                        <div class="col-md-2">

                            <asp:Label ID="Label7" runat="server" CssClass="Lbl" Text="رقم الهوية"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="CUSIdNumber" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="CUSIdNumber" CssClass="LblMessage" ErrorMessage="رقم الهوية الزامي">*</asp:RequiredFieldValidator>

                        </div>

                        <div class="col-md-2">

                            <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="نوع الهوية"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="_CUSIDType" runat="server" CssClass="form-control">
                                <asp:ListItem Selected="True" Value="0">الرجاء الاختيار</asp:ListItem>
                                <asp:ListItem Value="1">الهوية الوطنية</asp:ListItem>
                                <asp:ListItem Value="2">الاقامة</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="0" ControlToValidate="_CUSIDType" CssClass="LblMessage" ErrorMessage="الرجاء اختيار نوع الهوية">*</asp:RequiredFieldValidator>

                        </div>
                    </div>




                    <div class="row">


                        <div class="col-md-2">

                            <asp:Label ID="Label9" runat="server" CssClass="Lbl" Text="العنوان"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="_CUSAddress" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="_CUSAddress" CssClass="LblMessage" ErrorMessage="العنوان مطلوب">*</asp:RequiredFieldValidator>

                        </div>


                        <div class="col-md-2">

                            <asp:Label ID="Label10" runat="server" CssClass="Lbl" Text="اسم المدينة"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="_CustCityInfo" runat="server" CssClass="form-control" DataSourceID="SqlDataSource2" DataTextField="CityName_Ar" DataValueField="CityId">
                                <asp:ListItem Selected="True" Value="0">الرجاء الاختيار</asp:ListItem>


                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" InitialValue="0" ControlToValidate="_CustCityInfo" CssClass="LblMessage" ErrorMessage="الرجاء اختيار اسم المدينة">*</asp:RequiredFieldValidator>

                        </div>
                    </div>





                    <div class="row">
                        <div class="col-md-3">


                         
                        </div>
                        <div class="col-md-3">
                            <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false" runat="server" OnClick="BtnClear_Click"><i class="glyphicon glyphicon-refresh"></i> مسح</asp:LinkButton>
                        </div>
                        <div class="col-md-3">
                            <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary" runat="server" OnClick="BtnUpdate_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> تعديل</asp:LinkButton>
                        </div>
                        <div class="col-md-3">

                          

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
                                    <h3 class="panel-title"><i class="icon-globe"></i>قائمة العملاء</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>


                                            <asp:Panel ID="pnlTextBoxes" runat="server" Visible="false">
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
        </ContentTemplate>

    </asp:UpdatePanel>


</asp:Content>



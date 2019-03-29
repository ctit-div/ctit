<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Customers.aspx.cs" Inherits="Customers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>
            <div class="main" dir="rtl">

                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="معلومات العميل "></asp:Label>
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                        )
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="LblMessage" />
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
                        <asp:TextBox ID="_PasswordText" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_PasswordText" CssClass="LblMessage" ErrorMessage="كلمة المرور مطلوبة">*</asp:RequiredFieldValidator>

                    </div>


                    <div class="col-md-2">

                        <asp:Label ID="Label8" runat="server" CssClass="Lbl" Text="تأكيد كلمة المرور"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="_PasswordConfirmText"  TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
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

                        <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="تأكيد الايميل"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="_EmailConfirmText" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="_EmailText" ControlToValidate="_EmailConfirmText" CssClass="LblMessage" ErrorMessage="ادخال الايميل غير صحيح">*</asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="_EmailConfirmText" CssClass="LblMessage" ErrorMessage="تأكيد الايميل مطلوب">*</asp:RequiredFieldValidator>

                    </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">


                            <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Register</asp:LinkButton>
                        </div>
                        <div class="col-md-3">
                            <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false" runat="server" Visible="false" OnClick="BtnClear_Click"><i class="glyphicon glyphicon-refresh"></i> Go To Login</asp:LinkButton>
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-3">
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-md-12">

                            <asp:Label ID="LabelId" runat="server" CssClass="Lbl" Visible="False"></asp:Label>

                            <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>

                        </div>
                    </div>




                    


                </div>
        </ContentTemplate>

    </asp:UpdatePanel>


</asp:Content>




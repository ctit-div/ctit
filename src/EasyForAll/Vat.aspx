﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Vat.aspx.cs" Inherits="Vat" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="VAT Info" CssClass="Title"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="LblMessage" />
                        <asp:Label ID="LabelId" runat="server" CssClass="Lbl" Visible="False"></asp:Label>

                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="LblMessage" runat="server" CssClass="LblMessage"></asp:Label>
                    </div>
                    <hr />
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div>
                            <asp:Label ID="Label10" runat="server" CssClass="Label" Text="VAT Value"></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtVAT_Value" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtVAT_Value" CssClass="LblMsg" ErrorMessage="VAT Value is rquired">*</asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-6">
                        <div>
                            <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="Remarks"></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtRemarks" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtRemarks" CssClass="LblMessage" ErrorMessage="Remarks  is required">*</asp:RequiredFieldValidator>

                    </div>
                </div>





                <div class="row">
                    <div class="col-md-4">


                        <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton ID="BtnCleart" CssClass="btn btn-success" CausesValidation="false" runat="server" OnClick="BtnCleart_Click"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
                    </div>
                    <div class="col-md-4">

                        <%--<asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Visible="False" OnClick="BtnUpdate_Click"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>--%>



                    </div>


                </div>





                <div class="row">
                    <div class="col-md-12">

                        <div class="panel panel-grey margin-bottom-40">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-briefcase"></i>All Units<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" DeleteCommand="DELETE FROM [tVAT] WHERE [VAT_Id] = @VAT_Id" InsertCommand="INSERT INTO [tVAT] ([VAT_Value], [Remarks], [CompanyId], [IsActive]) VALUES (@VAT_Value, @Remarks, @CompanyId, @IsActive)" SelectCommand="SELECT [VAT_Id], [VAT_Value], [Remarks], [CompanyId], [IsActive] FROM [tVAT] WHERE ([CompanyId] = @CompanyId)" UpdateCommand="UPDATE [tVAT] SET [VAT_Value] = @VAT_Value, [Remarks] = @Remarks, [CompanyId] = @CompanyId, [IsActive] = @IsActive WHERE [VAT_Id] = @VAT_Id">
                                    <DeleteParameters>
                                        <asp:Parameter Name="VAT_Id" Type="Int32" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="VAT_Value" Type="Decimal" />
                                        <asp:Parameter Name="Remarks" Type="String" />
                                        <asp:Parameter Name="CompanyId" Type="Int32" />
                                        <asp:Parameter Name="IsActive" Type="Boolean" />
                                    </InsertParameters>
                                    <SelectParameters>
                                        <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="VAT_Value" Type="Decimal" />
                                        <asp:Parameter Name="Remarks" Type="String" />
                                        <asp:Parameter Name="CompanyId" Type="Int32" />
                                        <asp:Parameter Name="IsActive" Type="Boolean" />
                                        <asp:Parameter Name="VAT_Id" Type="Int32" />
                                    </UpdateParameters>
                                    </asp:SqlDataSource>
                                </h3>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="GridView1" runat="server"  Width="100%"
                                    AllowPaging="True"
                                    CssClass="table table-bordered"
                                    AlternatingRowStyle-CssClass="alt"
                                    PagerStyle-CssClass="pgr" PageSize="5" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="VAT_Id" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
                                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                                 
                                    <Columns>
                                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
                                        <asp:BoundField DataField="VAT_Id" HeaderText="VAT_Id" InsertVisible="False" ReadOnly="True" SortExpression="VAT_Id" />
                                        <asp:BoundField DataField="VAT_Value" HeaderText="VAT_Value" SortExpression="VAT_Value" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                                        <asp:BoundField DataField="CompanyId" HeaderText="CompanyId" SortExpression="CompanyId" />
                                        <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
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






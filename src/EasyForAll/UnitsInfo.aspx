<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UnitsInfo.aspx.cs" Inherits="UnitsInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="Units Info" CssClass="Title"></asp:Label>
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
                            <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Unit Name"></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtUnitName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtUnitName" CssClass="LblMsg" ErrorMessage="Unit name is rquired">*</asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-6">
                        <div>
                            <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="Description"></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtDescription" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtDescription" CssClass="LblMessage" ErrorMessage="Description  is required">*</asp:RequiredFieldValidator>

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

                        <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Visible="False" OnClick="BtnUpdate_Click"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>



                    </div>


                </div>





                <div class="row">
                    <div class="col-md-12">

                        <div class="panel panel-grey margin-bottom-40">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-briefcase"></i>All Units</h3>
                            </div>
                            <div class="panel-body">

                                <asp:GridView ID="GvDriver" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True"
                                    CssClass="table table-bordered"
                                    AlternatingRowStyle-CssClass="alt"
                                    PagerStyle-CssClass="pgr" DataKeyNames="UnitId" PageSize="5" OnSelectedIndexChanged="GvDriver_SelectedIndexChanged" OnRowDeleting="GvDriver_RowDeleting">
                                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    CommandArgument='<%# Bind("UnitId") %>' CssClass="btn btn-danger btn-xs" OnClientClick="return confirm('Deleting the record can not be restored, confirm deleting?');"> Delete<i class="icon-trash" style="font-size:16px;"> </i> </asp:LinkButton>
                                                &nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:CommandField ShowSelectButton="True" />--%>
                                        <asp:TemplateField HeaderText="Driver Code" InsertVisible="False" SortExpression="UnitId" Visible="False">
                                            <EditItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("UnitId") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("UnitId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                        <asp:BoundField DataField="UnitName" HeaderText="UnitName" SortExpression="UnitName" />
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




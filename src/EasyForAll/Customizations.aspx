<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Customizations.aspx.cs" Inherits="Customizations" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">
                <div class="row">
                    <div class="col-md-7">



                        <div class="row">
                            <div class="col-md-12">
                                <div>
                                    <asp:Label ID="Label2" runat="server" CssClass="Lbl" Text="Customization"></asp:Label>
                                    <asp:Label ID="LabelErrorMessage1" runat="server" CssClass="LblMessage"></asp:Label>
                                    <hr />
                                </div>
                            </div>
                        </div>


                   

                        <div class="row">
                            <div class="col-md-3">
                                 <asp:TextBox ID="TxtAccountNameGroupAr" Placeholder="اسم الحساب بالعربي"  runat="server" CssClass="form-control"></asp:TextBox>
                               <%-- <asp:Label ID="LabelAccountGroup" runat="server" CssClass="Lbl" Text="Account Name"></asp:Label>--%>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtAccountNameGroupEn" Placeholder="Account English Name"  runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="LabelAccountNoGroup" runat="server" CssClass="Lbl" Text="Account No: "></asp:Label>
                            </div>
                             <div class="col-md-2">
                                <asp:Label ID="LabelAccountNumberGroup" runat="server" CssClass="Lbl" ForeColor="Red" Text=""></asp:Label>
                            </div>
                         <%--  <div class="col-md-3">
                                <asp:TextBox ID="TxtAccountEnglishGroup" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>--%>
                            <div class="col-md-1">
                                <asp:ImageButton ID="ImageButtonAccountGroup" ImageUrl="~/Images/searchs.png" runat="server" Height="25px" Width="25px" OnClick="ImageButtonAccountGroup_Click"  />

                            </div>

                        </div>
                    </div>

                    <div class="col-md-5">
                        <div class="col-md-7">
                            <asp:Label ID="LblNodeText" ForeColor="Red" runat="server"></asp:Label>
                            <asp:Label ID="LblNodeValue" ForeColor="Turquoise" runat="server"></asp:Label>

                            <br />
                            <asp:TreeView ID="TreeView1" Width="50%" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ShowCheckBoxes="All" ShowLines="True" ExpandDepth="6" OnTreeNodeCheckChanged="TreeView1_TreeNodeCheckChanged" OnTreeNodeExpanded="TreeView1_TreeNodeExpanded">
                                <HoverNodeStyle BackColor="#CCCC00" />
                                <LeafNodeStyle ForeColor="#00CC99" />
                                <NodeStyle ForeColor="#CC6600" />
                                <ParentNodeStyle ForeColor="#3333CC" />
                                <RootNodeStyle ForeColor="#CC9900" />
                                <SelectedNodeStyle BackColor="#996600" ForeColor="White" Width="100%" />
                            </asp:TreeView>

                        </div>

                    </div>

                </div>

                <div class="row">
                    <div class="col-md-4">
<asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" Visible="true" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>
</div>
                        <div class="col-md-4">
                        <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false" runat="server" Visible="true" OnClick="BtnClear_Click"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                        <asp:LinkButton ID="BtnChangeStatus" CssClass="btn btn-success" runat="server" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Change Status</asp:LinkButton>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>
                    </div>
                </div>
                  <div class="row">
                    <div class="col-md-12">
                       <asp:GridView ID="GvCompany"  runat="server" AutoGenerateColumns="False" Width="100%"
                      AllowPaging="True"
                      CssClass="table table-bordered"                    
                      AlternatingRowStyle-CssClass="alt"
                      PagerStyle-CssClass="pgr" DataKeyNames="CustomizationId" PageSize="50" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GvCompany_SelectedIndexChanged"  >
                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                    <Columns>
                        <%--<asp:CommandField ShowSelectButton="True" />--%>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="CustomizationId" SortExpression="CustomizationId" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CustomizationId") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("CustomizationId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CustomizationNameEn" HeaderText="CustomizationNameEn" SortExpression="CustomizationNameEn" />
                        <asp:BoundField DataField="CustomizationNameAr" HeaderText="CustomizationNameAr" SortExpression="CustomizationNameAr" />
                        <asp:BoundField DataField="COAChartCode" HeaderText="COAChartCode" SortExpression="COAChartCode" />
                        <asp:TemplateField HeaderText="CompanyId" SortExpression="CompanyId" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CompanyId") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("CompanyId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="CMPCompanyName" HeaderText="CMPCompanyName" SortExpression="CMPCompanyName" Visible="true" />
                  
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                    <RowStyle CssClass="Lbl" />
                </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM [View_Customization] WHERE ([CompanyId] = @CompanyId)">
                            <SelectParameters>
                                <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


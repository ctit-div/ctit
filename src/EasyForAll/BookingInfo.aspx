<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BookingInfo.aspx.cs" Inherits="BookingInfo" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
<div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">
     <asp:Label ID="LblMessage" runat="server" CssClass="LblMessage"></asp:Label>   
    <div class="row">

            <div class="col-md-12">
                <asp:DataList ID="DataList1" runat="server" DataKeyField="ItemId"
                           RepeatColumns="2" Width="100%">
                            <ItemTemplate>
                                <table style="width: 400px;">
                                    <tr class="auto-style1">
                                        <td class="auto-style2">Product Code:</td>
                                        <td class="style4">
                                            <asp:Label ID="ProductCodeLabel" runat="server" Text='<%# Eval("ItemCode") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </td>
                                    </tr>
                                    <tr class="auto-style1">
                                        <td class="auto-style2">اسم الصنف بالعربي:</td>
                                        <td class="style4">
                                            <asp:Label ID="ProductNameLabel" runat="server" Text='<%# Eval("ItemName") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </td>
                                    </tr>
                                    <tr class="auto-style1">
                                        <td class="auto-style2">Product Name English:</td>
                                        <td class="style4">
                                            <asp:Label ID="ProductNameEngLabel" runat="server" Text='<%# Eval("ProductNameEng") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">Price:
                                        </td>
                                        <td class="style4">
                                            <asp:Label ID="OutSidePriceLabel" runat="server" Text='<%# Eval("OutSidePrice") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">Avaiable Quantity:</td>
                                        <td class="style4">
                                            <asp:Label ID="AvaiableQuantityLabel" runat="server" Text='<%# Eval("FillQuantity") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="auto-style2">Order Quantity:</td>
                                        <td class="style4">
                                            <asp:Label ID="OrderQuantity" runat="server" Text='<%# Eval("OrderQuantity") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="auto-style2">Least Quantity:</td>
                                        <td class="style4">
                                            <asp:Label ID="LeastQuantity" runat="server" Text='<%# Eval("LeastQuantity") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td class="auto-style2">Description:</td>
                                        <td class="style4">
                                            <asp:TextBox ID="Details" runat="server" Text='<%# Eval("Details") %>' TextMode="MultiLine" Enabled="false" Width="100%" CssClass="auto-style1" Style="color: #0099CC"> </asp:TextBox>
                                            <%--<asp:Label ID="Details" runat="server" Text='<%# Eval("Details") %>' Width="100%" />--%>
                                        </td>
                                    </tr>






                                    <tr>
                                        <td class="auto-style2">Unit Name:</td>
                                        <td class="style4">
                                            <asp:Label ID="UnitNameLabel" runat="server" Text='<%# Eval("BasicUnit") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">Active:</td>
                                        <td class="style4">
                                            <asp:Label ID="ActiveLabel" runat="server" Text='<%# Eval("IsActive") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </td>
                                    </tr>






                                    <tr>
                                        <td class="auto-style2">Category Name:</td>
                                        <td class="style4">
                                            <asp:Label ID="CategoryNameLabel" runat="server" Text='<%# Eval("ItemGroupName") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">Agent Name:</td>
                                        <td class="style4">
                                            <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("UserName") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </td>
                                    </tr>






                                    <tr>
                                        <td>
                                            <a href='<%# DataBinder.Eval(Container.DataItem,"ImageURL") %>' target="_blank">
                                                <asp:Image ID="picture" runat="server" Height="100px" ImageUrl='<%# Eval("ImageURL") %>' Width="100px" />
                                            </a>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btn" runat="server" CausesValidation="False" CommandName="select" Height="25" ImageUrl="~/Images/edit-button-blue-hi.png" Width="45" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            <asp:Label ID="CategoryCodeLabel" runat="server" Text='<%# Eval("ItemGroupId") %>' Visible="False" />
                                            <asp:Label ID="AgentCodeLabel" runat="server" Text='<%# Eval("AgentCode") %>' Visible="False" />
                                        </td>
                                        <td class="style4">
                                            <asp:Label ID="UnitIdLabel" runat="server" Text='<%# Eval("BasicUnit") %>' Visible="False" />
                                            <asp:Label ID="ActiveLabel0" runat="server" Text='<%# Eval("IsActive") %>' Visible="False" />
                                            <asp:Label ID="ItemIdLabel" runat="server" Text='<%# Eval("ItemId") %>' Visible="False" />
                                            <asp:Label ID="BarCodeLabel" runat="server" Text='<%# Eval("BarCode") %>' Visible="False" />
                                            <asp:Label ID="LabelCostPrice" runat="server" Text='<%# Eval("CostPrice") %>' Visible="False" />
                                               <asp:Label ID="LabelAveragePrice" runat="server" Text='<%# Eval("AveragePrice") %>' Visible="False" />
                                            <asp:Label ID="LabelSalePrice" runat="server" Text='<%# Eval("SalePrice") %>' Visible="False" />
                                            <asp:Label ID="LabelMinSalePrice" runat="server" Text='<%# Eval("MinSalePrice") %>' Visible="False" />
                                            <asp:Label ID="LabelWholeSalePrice" runat="server" Text='<%# Eval("WholeSalePrice") %>' Visible="False" />
                                             <asp:Label ID="LabelOfferPrice" runat="server" Text='<%# Eval("OfferPrice") %>' Visible="False" />

                                        </td>
                                    </tr>
                                </table>

                                <br />
                            </ItemTemplate>
                        </asp:DataList>

            </div>
        </div>
        
    </div>
</asp:Content>


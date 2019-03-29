<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisplayItemByOffer.aspx.cs" Inherits="DisplayItemByOffer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

<div class="row">




            <div class="col-md-6">
                <asp:Label ID="Label1" runat="server" Text="Offers" CssClass="Title" Visible="true"></asp:Label>
                <%--<asp:Image ID="Image1" runat="server" Height="34px" ImageUrl="~/TitleLogo/2.PNG"  Width="100%"  />--%>
            </div>
            <div class="col-md-6">
                <asp:Label ID="LblMessage" runat="server" CssClass="LblMessage"></asp:Label>


            </div>

        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:DataList ID="DataList1" runat="server" DataKeyField="ItemId"
                    OnSelectedIndexChanged="DataList1_SelectedIndexChanged" Width="100%" RepeatColumns="4" RepeatDirection="Horizontal">
                    <ItemTemplate>

                       <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">
                            <div class="row">

                                <div class="col-md-6">




                                    <a href='<%# Eval("ItemId", "DisplayItemsByCategory.aspx?id={0}") %>'>
                                        <asp:Image ID="picture" runat="server" Height="500px" ImageUrl='<%# Eval("ImageURL") %>' Width="400px" /></a>



                                </div>

                                <div class="col-md-6">

                                    <a href='<%# Eval("ItemId", "DisplaySearch.aspx?id={0}") %>'>

                                         <asp:ImageButton ID="btn" runat="server" CommandName="select" Height="35px" 
                                                ImageUrl="~/Images/cart.jpg" onclick="btn_Click" Width="35px" />
                                          <asp:Label ID="ItemIdLabel" runat="server" Text='<%# Eval("ItemId") %>' Visible="False"></asp:Label>
                                        <asp:Label ID="ProductCodeLabel" runat="server" Text='<%# Eval("ItemCode") %>' Visible="false" CssClass="auto-style1" Style="color: #0099CC" /><br />
                     0                   <asp:Label ID="CategoryNameLabel" runat="server" Text='<%# Eval("ItemGroupName") %>' CssClass="auto-style1" Style="color: #0099CC" /><br />
                                        <asp:Label ID="ProductNameLabel" runat="server" Text='<%# Eval("ItemName") %>' CssClass="auto-style1" Style="color: #0099CC" /><br />
                                        SAR:
                                            <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("OutSidePrice") %>' Font-Strikeout="True" ForeColor="#FF3300" /><br />
                                        SAR:
                                            <asp:Label ID="PriceLabel0" runat="server" Font-Size="Large" Font-Strikeout="False" ForeColor="#339933" Style="color: #0099CC" Text='<%# Eval("OfferPrice") %>' /><br />
                                        <asp:Label ID="Details" runat="server" Text='<%# Eval("Details") %>' Enabled="false" Width="100%" CssClass="auto-style1" Style="color: #0099CC"> </asp:Label></a>
                                    <hr style="border-color: #FFFFFF; width: 100%; background-color: #FFFFFF;" />
                                </div>
                            </div>
                        </div>



                    </ItemTemplate>
                </asp:DataList>

            </div>
        </div>
        
    </div>



</asp:Content>


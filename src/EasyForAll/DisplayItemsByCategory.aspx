<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisplayItemsByCategory.aspx.cs" Inherits="DisplayItemsByCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UcCart.ascx" TagPrefix="uc1" TagName="UcCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <style>
        #rcorners {
            border-radius: 15px 100px;
            border: 1px solid #73AD21;
            padding: 10px;
            width: 100%;
            height: 100%;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

            <div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">
                <div id="n">

                    <div class="row">




                        <div class="col-md-12">
                            <asp:Label ID="Label1" runat="server" Text=" Add to your Cart()" Visible="true" CssClass="Title"></asp:Label>
                            <asp:Label ID="LblMessage" runat="server" CssClass="LblMessage"></asp:Label>
                        </div>

                        <hr />

                    </div>

                    <div class="row">
                        <div class="col-md-7">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                                    <asp:DataList ID="DataList1" runat="server" DataKeyField="ItemId"
                                        OnSelectedIndexChanged="DataList1_SelectedIndexChanged" RepeatColumns="2" Width="50%" RepeatDirection="Horizontal">
         
                                        <ItemTemplate>

                                                                     
                                            <table style="width: 400px;">
                                                <tr class="auto-style1">
                                                    <td class="auto-style2">كود المنتج:</td>
                                                    <td class="style4">
                                                        <asp:Label ID="ProductCodeLabel" runat="server" Text='<%# Eval("ItemCode") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                                    </td>
                                                </tr>
                                                <tr class="auto-style1">
                                                    <td class="auto-style2">اسم المنتج بالعربي:</td>
                                                    <td class="style4">
                                                        <asp:Label ID="ProductNameLabel" runat="server" Text='<%# Eval("ItemName") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                                    </td>
                                                </tr>
                                              
                                                <tr>
                                                    <td class="auto-style2">السعر (SAR):
                                                    </td>
                                                    <td class="style4">
                                                        <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("OutSidePrice") %>' />
                                                    </td>
                                                </tr>
                                                <% if (Session["Offer"] != null)
    { %>

                                                <tr>
                                                    <td class="auto-style2">
                                                        <p style="color: red">السعر الجديد(SAR)</p>
                                                    </td>
                                                    <td class="style4">
                                                        <p style="color: red">
                                                            <asp:Label ID="PriceLabel0" runat="server" Font-Size="Large" Font-Strikeout="False" ForeColor="#339933" Style="color: red" Text='<%# Eval("OfferPrice") %>' /></p>
                                                    </td>
                                                </tr>
                                                <%} %>
                                                <tr>
                                                    <td class="auto-style2">اسم التصنيف:</td>
                                                    <td class="style4">
                                                        <asp:Label ID="ItemIdLabel" runat="server" Text='<%# Eval("ItemId") %>' Visible="False"></asp:Label>
                                                        <asp:Label ID="CategoryNameLabel" runat="server" Text='<%# Eval("ItemGroupName") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style2">الوصف:</td>
                                                    <td class="style4">
                                                        <asp:Label ID="Details" runat="server" Text='<%# Eval("Details") %>' Enabled="false" Width="100%" CssClass="auto-style1" Style="color: #0099CC"> </asp:Label>

                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="auto-style2">الكمية:&nbsp;</td>
                                                    <td class="style4">

                                                        <asp:TextBox ID="txtQty" runat="server" Font-Size="Large" Width="50">0</asp:TextBox>


                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <a href='<%# DataBinder.Eval(Container.DataItem,"ImageURL") %>' target="_blank">
                                                            <asp:Image ID="picture" runat="server" Height="100px" ImageUrl='<%# Eval("ImageURL") %>' Width="100px" />
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <a href='<%# Eval("ItemId", "DisplaySearch.aspx?id={0}") %>'>
                                                            <br />
                                                            <asp:ImageButton ID="btn" runat="server" CommandName="select" Height="50px"
                                                                ImageUrl="~/Images/cart.jpg" OnClick="btn_Click" Width="50px" /></a>

                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        


                                
                                        </ItemTemplate>


                                    </asp:DataList>

                                </ContentTemplate>

                            </asp:UpdatePanel>


                        </div>

                        <div class="col-md-5">
                            <uc1:UcCart runat="server" ID="UcCart" />

                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>




</asp:Content>

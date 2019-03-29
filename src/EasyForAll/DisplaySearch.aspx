<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisplaySearch.aspx.cs" Inherits="DisplaySearch" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UcSearchItem.ascx" TagPrefix="uc1" TagName="UcSearchItem" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<%--<link href="assets/bootstrap/css/bootstrap.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




<asp:UpdatePanel ID="hhh222" runat="server">
    <ContentTemplate>
        <span class="border border-success">
           <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">




                    <div class="col-md-12">
                        <uc1:UcSearchItem runat="server" ID="UcSearchItem" />
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


                                           <a href='<%# Eval("ItemId", "DisplayItemsByCategory.aspx?Cid={0}") %>'><asp:Image ID="picture" runat="server"  Height="250px" ImageUrl='<%# Eval("ImageURL") %>' Width="200px" /></a>

                      
    

                                            </div>
                                        </div>
                                   
                                        <div class="col-md-6">
                                            
        <a href='<%# Eval("ItemId", "DisplayItemsByCategory.aspx?Cid={0}") %>'>

                
              <asp:Label ID="ItemIdLabel" runat="server" Text='<%# Eval("ItemId") %>' Visible="false" CssClass="auto-style1" Style="color: #0099CC" /><br />
                                            <asp:Label ID="ProductCodeLabel" runat="server" Text='<%# Eval("ItemCode") %>' Visible="false" CssClass="auto-style1" Style="color: #0099CC" /><br />
                                            <asp:Label ID="CategoryNameLabel" runat="server" Text='<%# Eval("ItemGroupName") %>' CssClass="auto-style1" Style="color: #0099CC" /><br />
                                            <asp:Label ID="ProductNameLabel" runat="server" Text='<%# Eval("ItemName") %>' CssClass="auto-style1" Style="color: #0099CC" /><br />
                                            SAR:
                                            <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("OutSidePrice") %>' Font-Strikeout="True" ForeColor="#FF3300" /><br />
                                            <%--SAR:
                                            <asp:Label ID="PriceLabel0" runat="server" Font-Size="Large" Font-Strikeout="False" ForeColor="#339933" Style="color: #0099CC" Text='<%# Eval("OfferPrice") %>' />--%>
            <br /><%--Quantity: 
             <asp:TextBox ID="txtQty" runat="server" Font-Size="Large" Width="50">0</asp:TextBox><br />--%>
              Read More</a>
                                     
                                                                        <hr style="border-color: #FFFFFF; width: 100%; background-color: #FFFFFF;" />
                                        </div>
                                    </div>
                            
                              

                            </ItemTemplate>
                        </asp:DataList>

                    </div>
                </div>
            </div>



        </span>
    </ContentTemplate>

</asp:UpdatePanel>

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="http://cdn.rawgit.com/elevateweb/elevatezoom/master/jquery.elevateZoom-3.0.8.min.js"></script>
<script type="text/javascript">
$(function () {
    $("[id*=DataList1] img").elevateZoom({
        cursor: 'pointer',
        imageCrossfade: true,
        loadingIcon: 'loading.gif'
    });
});
</script>


</asp:Content>


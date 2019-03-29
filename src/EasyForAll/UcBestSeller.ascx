<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcBestSeller.ascx.cs" Inherits="UcBestSeller" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<link href="assets/bootstrap/css/bootstrap.css" rel="stylesheet" />--%>



<asp:UpdatePanel ID="hhh222" runat="server">
    <ContentTemplate>
        <span class="border border-success">
           <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">




                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="Best Seller" CssClass="Title" Visible="true"></asp:Label>
                        <%--<asp:Image ID="Image1" runat="server" Height="34px" ImageUrl="~/TitleLogo/2.PNG"  Width="100%"  />--%>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="LblMessage" runat="server" CssClass="LblMessage"></asp:Label>
                        <div>
                        </div>

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


                                           <a href='<%# Eval("ItemId", "DisplayItemsByCategory.aspx?Cid={0}") %>'><asp:Image ID="picture" runat="server"  Height="150px" ImageUrl='<%# Eval("ImageURL") %>' Width="120px" /></a>

                      
   
      <%--  <a href='<%# Eval("ItemId", "DisplayItemByOffer.aspx?id={0}") %>'><asp:Image ID="picture" runat="server" data-zoom-image='<%# ResolveUrl(Eval("ZoomImageUrl").ToString()) %>' Height="250px" ImageUrl='<%# Eval("ImageURL") %>' Width="200px" /></a>

               --%> 

                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            
        <a href='<%# Eval("ItemId", "DisplayItemsByCategory.aspx?Cid={0}") %>'>

                

                                            <asp:Label ID="ProductCodeLabel" runat="server" Text='<%# Eval("ItemCode") %>' Visible="false" CssClass="auto-style1" Style="color: #0099CC" /><br />
                                            <asp:Label ID="CategoryNameLabel" runat="server" Text='<%# Eval("ItemGroupName") %>' CssClass="auto-style1" Style="color: #0099CC" /><br />
                                            <asp:Label ID="ProductNameLabel" runat="server" Text='<%# Eval("ItemName") %>' CssClass="auto-style1" Style="color: #0099CC" /><br />
                                            SAR:
                                            <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("OutSidePrice") %>' Font-Strikeout="false" ForeColor="#FF3300" /><br />
                                      
            <br />
            Read More</a>
                                                                        <hr style="border-color: #FFFFFF; width: 100%; background-color: #FFFFFF;" />
                                        </div>
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


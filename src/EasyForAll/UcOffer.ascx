<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcOffer.ascx.cs" Inherits="UcOffer" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<link href="assets/bootstrap/css/bootstrap.css" rel="stylesheet" />--%>




<asp:UpdatePanel ID="hhh222" runat="server">
    <ContentTemplate>
        <span class="border border-success">
           <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">




                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="Offers" CssClass="Title" Visible="true"></asp:Label>
                        <%--<asp:Image ID="Image1" runat="server" Height="34px" ImageUrl="~/TitleLogo/2.PNG"  Width="100%"  />--%>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="LblMessage" runat="server" CssClass="LblMessage"></asp:Label>
                        <div>
                        </div>

                    </div>
                    <hr />

                </div>

                <div class="row">
                    <div class="col-md-12">
                        <asp:DataList ID="DataList1" runat="server" DataKeyField="ItemId"
                            OnSelectedIndexChanged="DataList1_SelectedIndexChanged" Width="100%" RepeatColumns="1" RepeatDirection="Horizontal">
                            <ItemTemplate>

                               <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">


                                    <div class="row">
                                        <div class="col-md-2">
                                            Product Code:
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="ProductCodeLabel" runat="server" Text='<%# Eval("ItemCode") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </div>
                                        <div class="col-md-2">
                                            Category Name:
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="CategoryNameLabel" runat="server" Text='<%# Eval("ItemGroupName") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </div>



                                    </div>




                                    <div class="row">
                                        <div class="col-md-2">
                                            اسم الصنف بالعربي:
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="ProductNameLabel" runat="server" Text='<%# Eval("ItemName") %>' CssClass="auto-style1" Style="color: #0099CC" />
                                        </div>
                                        <div class="col-md-2">
                                            Product Name English:
                                        </div>
                                        <div class="col-md-4">
                                           <%-- <asp:Label ID="ProductNameEngLabel" runat="server" Text='<%# Eval("ProductNameEng") %>' CssClass="auto-style1" Style="color: #0099CC" />--%>
                                        </div>




                                    </div>

                                    <div class="row">
                                        <div class="col-md-2">
                                            Price (SAR):
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("OutSidePrice") %>' Font-Strikeout="True" ForeColor="#FF3300" />
                                        </div>
                                        <div class="col-md-2">
                                            New Price(SAR)
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="PriceLabel0" runat="server" Font-Size="Large" Font-Strikeout="False" ForeColor="#339933" Style="color: #0099CC" Text='<%# Eval("OfferPrice") %>' />
                                        </div>




                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            Description:
                                        </div>
                                        <div class="col-md-4">
                                            <%--<asp:Label ID="Details" runat="server" Text='<%# Eval("Details") %>' Enabled="false" Width="100%" CssClass="auto-style1" Style="color: #0099CC"> </asp:Label>--%>
                                        </div>


                                        <div class="col-xs-6 col-sm-6 col-md-6">
                                            <a href='<%# DataBinder.Eval(Container.DataItem,"ImageURL") %>' target="_blank">
                                                <asp:Image ID="picture" runat="server" Height="100px" ImageUrl='<%# Eval("ImageURL") %>' Width="100px" />
                                            </a>
                                        </div>


                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server" Width="50%" Visible="true" CommandName="Select"><i class="glyphicon glyphicon-shopping-cart"></i> Cart</asp:LinkButton>
                                        </div>


                                        <div class="col-md-9">
                                            <hr style="width:80%; background-color: #808080;"  />
                                       
                                          
                                            <asp:Label ID="ItemIdLabel" runat="server" Text='<%# Eval("ItemId") %>' Visible="False" />
                                            
                                        </div>


                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
      
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




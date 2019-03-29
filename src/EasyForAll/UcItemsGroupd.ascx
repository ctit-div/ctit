<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcItemsGroupd.ascx.cs" Inherits="UcItemsGroupd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<link href="assets/bootstrap/css/bootstrap.css" rel="stylesheet" />
--%>


<asp:UpdatePanel ID="hhh222" runat="server">
    <ContentTemplate>
        <span class="border border-success">
            <div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">

                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="Categories" CssClass="Title" Visible="true"></asp:Label>
                        <%--<asp:Image ID="Image1" runat="server" Height="34px" ImageUrl="~/TitleLogo/2.PNG"  Width="100%"  />--%>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="LblMessage" runat="server" CssClass="LblMessage"></asp:Label>
                        <div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <asp:DataList ID="DataList1" runat="server" DataKeyField="ItemGroupId" DataSourceID="SqlDataSource1"  RepeatLayout="Flow"  RepeatDirection="Horizontal"  Font-Names="Noor">
                        <ItemTemplate>
                            <div class="col-md-1">
                                    <div style="border: solid 1px #e8e6e6; text-align: center; Width:85px; height: 100px;">
                                        <asp:Image ID="Image1" runat="server" Height="50px" ImageUrl='<%# Eval("CategoryImage") %>' Width="50px" />

                                        <asp:Label ID="ItemGroupIdLabel" runat="server" Text='<%# Eval("ItemGroupId") %>' Visible="False" />

                                        <div style="background-color: #e4ddd3; height: 25px; font-size: 12px;">

                                            <a href='<%# Eval("ItemGroupId", "DisplayItemsByCategory.aspx?CidAll={0}") %>'>
                                                <asp:Image ID="picture" runat="server" Height="50px" Visible="False" ImageUrl='<%# Eval("CategoryImage") %>' Width="50px" />
                                                <asp:Label ID="ItemGroupNameLabel" runat="server" Text='<%# Eval("ItemGroupName") %>' />
                                            </a>
                                        </div>
                                    </div> 

                                </div>
                            <%--<table>
                                <tr>
                                    <td class="text-center">
                                        <a href='<%# Eval("ItemGroupId", "DisplayItemsByCategory.aspx?CidAll={0}") %>'>
                                            <asp:Image ID="picture" runat="server" Height="250px" Visible="False" ImageUrl='<%# Eval("CategoryImage") %>' Width="200px" />
                                        <asp:Label ID="ItemGroupNameLabel" runat="server" Text='<%# Eval("ItemGroupName") %>' />
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">&nbsp;&nbsp;
          <asp:Image ID="Image1" runat="server"  Height="100px" ImageUrl='<%# Eval("CategoryImage") %>' Width="100px" />
                                        &nbsp; 
                                <asp:Label ID="ItemGroupIdLabel" runat="server" Text='<%# Eval("ItemGroupId") %>' Visible="False" />

                                    </td>
                                </tr>
                            </table>--%>

                        </ItemTemplate>
                    </asp:DataList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT [ItemGroupId], [ItemGroupName], [CategoryImage] FROM [tItemGroups]"></asp:SqlDataSource>




                </div>
            </div>



        </span>
    </ContentTemplate>

</asp:UpdatePanel>

<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="http://cdn.rawgit.com/elevateweb/elevatezoom/master/jquery.elevateZoom-3.0.8.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=DataList1] img").elevateZoom({
            cursor: 'pointer',
            imageCrossfade: true,
            loadingIcon: 'loading.gif'
        });
    });
</script>--%>

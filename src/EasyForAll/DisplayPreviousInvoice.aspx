<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisplayPreviousInvoice.aspx.cs" Inherits="DisplayPreviousInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UcPreviousInvoice.ascx" TagPrefix="uc1" TagName="UcPreviousInvoice" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="assets/bootstrap/css/bootstrap.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>
            <span class="border border-success">
                <div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">

                    <div class="row">




                        <div class="col-md-12">
                            <uc1:UcPreviousInvoice runat="server" ID="UcPreviousInvoice" />
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





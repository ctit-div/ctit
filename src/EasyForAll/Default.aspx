<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="UcOffer.ascx" TagName="UcOffer" TagPrefix="uc1" %>
<%@ Register Src="~/UcItemsGroupd.ascx" TagPrefix="uc1" TagName="UcItemsGroupd" %>
<%@ Register Src="~/UcOffer.ascx" TagPrefix="uc2" TagName="UcOffer" %>
<%@ Register Src="~/UcMakeOffer.ascx" TagPrefix="uc1" TagName="UcMakeOffer" %>
<%@ Register Src="~/UcTopTen.ascx" TagPrefix="uc1" TagName="UcTopTen" %>
<%@ Register Src="~/UcBestSeller.ascx" TagPrefix="uc1" TagName="UcBestSeller" %>
<%@ Register Src="~/UcCustomer.ascx" TagPrefix="uc1" TagName="UcCustomer" %>
<%@ Register Src="~/UcOfferMain.ascx" TagPrefix="uc1" TagName="UcOfferMain" %>
<%@ Register Src="~/UcSearchItem.ascx" TagPrefix="uc1" TagName="UcSearchItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <uc1:UcSearchItem runat="server" ID="UcSearchItem" />

            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12">

                <div class="bs-example">
                    <div class="panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Category</a>
                                </h4>
                            </div>
                            <div id="collapseOne" class="panel-collapse collapse in">
                                <div class="panel-body"> 
                                  
                                        <uc1:UcItemsGroupd runat="server" ID="UcItemsGroupd" />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                           <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Offers</a>
                                </h4>
                            </div>
                            <div id="collapseTwo" class="panel-collapse collapse">
                                <div class="panel-body">
                                  
                                        <uc1:UcOfferMain runat="server" ID="UcOfferMain" />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                          <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree">Top Ten </a>
                                </h4>
                            </div>
                            <div id="collapseThree" class="panel-collapse collapse">
                                <div class="panel-body">
                                    
                                        <uc1:UcTopTen runat="server" ID="UcTopTen" />
                                </div>
                            </div>
                        </div>

                         <div class="panel panel-default">
                           <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive">Suggested Products</a>
                                </h4>
                            </div>
                            <div id="collapseFive" class="panel-collapse collapse">
                                <div class="panel-body">
                                    
                                        <uc1:UcCustomer runat="server" ID="UcCustomer" />
                                </div>
                            </div>
                        </div>


                    </div>
                    <p><strong>Note:</strong> Click on the linked heading text to expand or collapse accordion panels.</p>
                </div>






            </div>
        </div>


    </div>


</asp:Content>


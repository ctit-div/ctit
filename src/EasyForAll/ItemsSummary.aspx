<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItemsSummary.aspx.cs" Inherits="ItemsSummary" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--   <link href="Content/app/css/ModalCSS.css" rel="stylesheet" />--%>
    <style type="text/css">
        .auto-style2 {
            color: #0099FF;
        }

        .modalpopup {
            background-color: #ADADAD;
            filter: Alpha(Opacity=70);
            opacity: 0.70;
            /*-moz-opacity: 0.70;*/
        }
    </style>
    <script>
        // Get the modal
        var modal = document.getElementById('myModal');

        // Get the button that opens the modal
        var btn = document.getElementById("myBtn");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks the button, open the modal 
        btn.onclick = function () {
            modal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

            <div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">
                <asp:Label ID="LblMessage" runat="server" CssClass="LblMessage"></asp:Label>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="LabelItemsInfo" runat="server" Text="Items Info" CssClass="Title"></asp:Label>
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>)

                    </div>
                    <div class="col-md-4">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                            CssClass="LblMessage" DisplayMode="List" Font-Size="Small" />

                    </div>
                </div>
                <div class="row">







                    <div class="col-md-3">
                        <div>
                            <asp:TextBox ID="TxtSearchItem" CssClass="form-control" placeholder="Search by Item" runat="server"></asp:TextBox>

                        </div>

                    </div>
                    <div class="col-md-3">
                        <div>
                            <asp:Button ID="BtnSearchItem" runat="server" class="btn btn-info btn-sm" Text="Search Product" CausesValidation="False" OnClick="BtnSearchItem_Click" Width="100%" />
                        </div>

                    </div>

                    <div class="col-md-3">
                        <div>
                               <asp:Button ID="BtnNext" runat="server" class="btn btn-next btn-xing" Text="Display Next   >>" CausesValidation="False"  Width="100%" OnClick="BtnNext_Click" />
                        </div>

                    </div>
                    <div class="col-md-3">
                        <div>
                            <asp:Button ID="ButtonPrevious" runat="server" class="btn btn-previous  btn-xing" Text="<<   Display Previous" CausesValidation="False"  Width="100%" OnClick="ButtonPrevious_Click" />
                        </div>

                    </div>

                </div>

                <div class="row">


                    <div class="col-md-3">
                        <div>

                            <asp:Label ID="LabelProductCode" runat="server" CssClass="Lbl" Text="Product Code"></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtProductCode" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ControlToValidate="TxtProductCode" CssClass="LblMessage"
                            ErrorMessage="Product code is required">*</asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-3">
                        <div>
                            <asp:Label ID="LabelProductName" runat="server" CssClass="Lbl" Text="اسم الصنف بالعربي"></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtProductName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="TxtProductName" CssClass="LblMessage"
                            ErrorMessage="Product Name is required">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <div>
                            <asp:Label ID="Label14" runat="server" CssClass="Lbl" Text="English Product Name"></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtProductNameEng" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                            ControlToValidate="TxtProductName" CssClass="LblMessage"
                            ErrorMessage="English Product Name is required">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <div>
                        </div>
                    </div>
                    <div class="row">



                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="LabelAvaiableQuantity" runat="server" CssClass="Lbl" Text="Avaiable Quantity"></asp:Label>
                            </div>
                            <asp:TextBox ID="TxtAvaiableQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="TxtAvaiableQuantity" CssClass="LblMessage"
                                ErrorMessage="Avaiable Quantity is required">*</asp:RequiredFieldValidator>

                            <asp:RangeValidator ID="RangeValidator2" runat="server"
                                ControlToValidate="TxtAvaiableQuantity" CssClass="LblMessage" ErrorMessage="must be number"
                                MaximumValue="1000000" MinimumValue="0" Type="Currency">*</asp:RangeValidator>
                        </div>

                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="LabelLeastQuantity" runat="server" CssClass="Lbl" Text="Least Quantity"></asp:Label>
                            </div>
                            <asp:TextBox ID="TxtLeastQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                ControlToValidate="TxtLeastQuantity" CssClass="LblMessage"
                                ErrorMessage="Least Quantity is required">*</asp:RequiredFieldValidator>

                            <asp:RangeValidator ID="RangeValidator3" runat="server"
                                ControlToValidate="TxtLeastQuantity" CssClass="LblMessage" ErrorMessage="must be number"
                                MaximumValue="1000000" MinimumValue="0" Type="Currency">*</asp:RangeValidator>
                        </div>
                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="LabelOrderQuantity" runat="server" CssClass="Lbl" Text="Order Quantity"></asp:Label>
                            </div>
                            <asp:TextBox ID="TxtOrderQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                ControlToValidate="TxtOrderQuantity" CssClass="LblMessage"
                                ErrorMessage="Order Quantity is required">*</asp:RequiredFieldValidator>

                            <asp:RangeValidator ID="RangeValidator4" runat="server"
                                ControlToValidate="TxtOrderQuantity" CssClass="LblMessage" ErrorMessage="must be number"
                                MaximumValue="1000000" MinimumValue="0" Type="Currency">*</asp:RangeValidator>
                        </div>

                        <div class="col-md-3">

                            <div>
                                <asp:Label ID="LabelCostPrice" runat="server" CssClass="Lbl" Text="Cost Price"></asp:Label>
                            </div>
                            <asp:TextBox ID="TxtPrice" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="TxtPrice" CssClass="LblMessage"
                                ErrorMessage="Price is required">*</asp:RequiredFieldValidator>

                            <asp:RangeValidator ID="RangeValidator1" runat="server"
                                ControlToValidate="TxtPrice" CssClass="LblMessage" ErrorMessage="must be number"
                                MaximumValue="1000000" MinimumValue="0" Type="Currency">*</asp:RangeValidator>

                        </div>
                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="LabelSalePrice" runat="server" CssClass="Lbl" Text="Sale Price"></asp:Label>
                            </div>
                            <asp:TextBox ID="TxtSalePrice" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                ControlToValidate="TxtSalePrice" CssClass="LblMessage"
                                ErrorMessage="Least Quantity is required">*</asp:RequiredFieldValidator>

                            <asp:RangeValidator ID="RangeValidator6" runat="server"
                                ControlToValidate="TxtSalePrice" CssClass="LblMessage" ErrorMessage="must be number"
                                MaximumValue="1000000" MinimumValue="0" Type="Currency">*</asp:RangeValidator>
                        </div>
                    </div>


                    <div class="row">




                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="LabelMinSalePrice" runat="server" CssClass="Lbl" Text="Min Sale Price"></asp:Label>
                            </div>
                            <asp:TextBox ID="TxtMinSalePrice" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                ControlToValidate="TxtMinSalePrice" CssClass="LblMessage"
                                ErrorMessage="Min Sal Price is required">*</asp:RequiredFieldValidator>

                            <asp:RangeValidator ID="RangeValidator7" runat="server"
                                ControlToValidate="TxtMinSalePrice" CssClass="LblMessage" ErrorMessage="must be number"
                                MaximumValue="1000000" MinimumValue="0" Type="Currency">*</asp:RangeValidator>
                        </div>

                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="LabelWholeSalePrice" runat="server" CssClass="Lbl" Text="Whole Sale Price"></asp:Label>
                            </div>
                            <asp:TextBox ID="TxtWholeSalePrice" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                ControlToValidate="TxtWholeSalePrice" CssClass="LblMessage"
                                ErrorMessage="Whole Sale Price is required">*</asp:RequiredFieldValidator>

                            <asp:RangeValidator ID="RangeValidator8" runat="server"
                                ControlToValidate="TxtWholeSalePrice" CssClass="LblMessage" ErrorMessage="must be number"
                                MaximumValue="1000000" MinimumValue="0" Type="Currency">*</asp:RangeValidator>
                        </div>
                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="LabelOutSidePrice" runat="server" CssClass="Lbl" Text="Out Side Price"></asp:Label>
                            </div>
                            <asp:TextBox ID="TxtOutSidePrice" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator150" runat="server"
                                ControlToValidate="TxtOutSidePrice" CssClass="LblMessage"
                                ErrorMessage="Whole Sale Price is required">*</asp:RequiredFieldValidator>

                            <asp:RangeValidator ID="RangeValidator108" runat="server"
                                ControlToValidate="TxtOutSidePrice" CssClass="LblMessage" ErrorMessage="must be number"
                                MaximumValue="1000000" MinimumValue="0" Type="Currency">*</asp:RangeValidator>
                        </div>

                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="LabelDescription" runat="server" CssClass="Lbl" Text="Description"></asp:Label>
                            </div>
                            <asp:TextBox ID="TxtDetails" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                ControlToValidate="TxtDetails" CssClass="LblMessage"
                                ErrorMessage="Details is required">*</asp:RequiredFieldValidator>

                        </div>

                    </div>
                    <div class="row">

                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="LabelOfferPrice" runat="server" CssClass="Lbl" Text="Offer Price"></asp:Label>
                            </div>
                            <asp:TextBox ID="TxtOfferPrice" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                ControlToValidate="TxtOfferPrice" CssClass="LblMessage"
                                ErrorMessage="Offer Price is required">*</asp:RequiredFieldValidator>

                            <asp:RangeValidator ID="RangeValidator10" runat="server"
                                ControlToValidate="TxtOfferPrice" CssClass="LblMessage" ErrorMessage="must be number"
                                MaximumValue="1000000" MinimumValue="0" Type="Currency">*</asp:RangeValidator>
                        </div>
                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="LabelUnitName" runat="server" CssClass="Lbl" Text="Unit Name"></asp:Label>
                            </div>
                            <asp:DropDownList ID="DropDownListUnitName" runat="server" AutoPostBack="True" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="DropDownListUnitName" CssClass="LblMessage" ErrorMessage="Unit Name is required">*</asp:RequiredFieldValidator>

                        </div>


                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="LabelVAT" runat="server" CssClass="Lbl" Text="VAT"></asp:Label>
                            </div>
                            &nbsp;<asp:RadioButtonList ID="RadioButtonListIsVAT" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonListIsVAT_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:RadioButtonList>

                        </div>

                        <div class="col-md-3">
                            <div>
                                <asp:Label ID="Label11" runat="server" CssClass="Lbl" Text=""></asp:Label>
                            </div>
                            <asp:DropDownList ID="DropDownListVATvalue" Visible="false" runat="server" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="VAT_Value" DataValueField="VAT_Id">
                            </asp:DropDownList>


                        </div>


                    </div>

                </div>
                <div class="row">

                    <div class="col-md-3">
                        <div>
                            <asp:Label ID="LabelActive" runat="server" CssClass="Lbl" Text="Active"></asp:Label>
                        </div>
                        &nbsp;<asp:RadioButtonList ID="RadioButtonListActive" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-3">
                        <div>
                            <asp:Label ID="LabelDisplayforCustomers" runat="server" CssClass="Lbl" Text="Display for Customers"></asp:Label>
                        </div>
                        &nbsp;<asp:RadioButtonList ID="RadioButtonListIsDisplayed" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">No</asp:ListItem>
                            <asp:ListItem Value="0">Yes</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="col-md-3">
                        <div>
                            <asp:Label ID="LabelUploadImage" runat="server" CssClass="Lbl" Text="Upload Image"></asp:Label>
                        </div>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                        <asp:HyperLink ID="H1" runat="server" Target="_blank">View Image</asp:HyperLink>



                    </div>

                </div>




                <div class="row">
                    <div class="col-md-3">


                        <%--   <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>--%>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton ID="BtnReset" CssClass="btn btn-success" CausesValidation="false" runat="server" OnClick="BtnReset_Click"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton ID="BtnEdit" CssClass="btn btn-primary" runat="server" OnClick="BtnEdit_Click" Visible="true"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <%-- <asp:LinkButton ID="BtnDelete" CssClass="btn btn-success" runat="server" OnClick="BtnDelete_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Delete</asp:LinkButton>--%>
                    </div>


                </div>

                <div class="row">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>


                                <asp:Panel ID="pnlTextBoxes" runat="server" Visible="false">
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="BtnEdit" />

                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:DataList ID="DataList1" runat="server" DataKeyField="ItemId"
                            OnSelectedIndexChanged="DataList1_SelectedIndexChanged" RepeatColumns="2" Width="100%">
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

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT [VAT_Id], [VAT_Value] FROM [tVAT] WHERE ([StatusId] = @Status)">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="1" Name="Status" QueryStringField="1" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                    </div>
                </div>


            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

    <div id="myModal" class="modal" role="dialog" data-backdrop="false">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Search Categories</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div>
                            </div>
                        </div>

                    </div>
                    <p></p>
                </div>
                <div class="modal-footer">
                </div>
            </div>

        </div>
    </div>

</asp:Content>




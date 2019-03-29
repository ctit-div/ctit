<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItemsGroup.aspx.cs" Inherits="ItemsGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        //************************** Treeview Parent-Child check behaviour ****************************//


        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }


        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }


        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;


            if (parentNodeTable) {
                var checkUncheckSwitch;


                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        return; //do not need to check parent if any child is not checked
                }
                else //checkbox unchecked
                {
                    checkUncheckSwitch = false;
                }


                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }


        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

            <div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="LabelCategoryTitle" runat="server" Text="Category Info" CssClass="Title"></asp:Label>
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                        )
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">

                        <hr />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="LabelCategoryItem" runat="server" CssClass="Lbl" Text="Items Category"></asp:Label>
                        </div>

                    </div>



                </div>

                <div class="row">

                    <div class="col-md-4">
                        <div>
                            <asp:TextBox ID="TxtSearch" CssClass="form-control" placeholder="Search by Category" runat="server"></asp:TextBox>

                        </div>

                    </div>
                    <div class="col-md-1">
                        <div>
                            <asp:Button ID="BtnSearch" runat="server" CssClass="fs fa-search" Text="Search" OnClick="BtnSearch_Click" CausesValidation="False" />
                        </div>

                    </div>


                </div>


                <div class="row">
                    <div class="col-md-12">
                        <div>

                            <asp:TreeView ID="TreeView1" Width="50%" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ShowCheckBoxes="All" ShowLines="True" ExpandDepth="0" OnTreeNodeExpanded="TreeView1_TreeNodeExpanded">
                                <SelectedNodeStyle BackColor="#996600" ForeColor="White" Width="100%" />
                            </asp:TreeView>

                        </div>
                    </div>

                </div>


                <div class="row">
                    <div class="col-md-2">


                        <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="BtnSave_Click" CausesValidation="False"><i class="glyphicon glyphicon-floppy-save"></i> Add New</asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false" runat="server" OnClick="BtnClear_Click" Visible="False"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>
                    </div>


                </div>


                <div class="row">
                    <div class="col-md-12">

                        <div class="panel panel-grey margin-bottom-40">
                            <div class="panel-heading">
                                <%if (Session["dir"].ToString() == "ltr")
                                    { %>
                                <h3 class="panel-title">New&nbsp; Category</h3>
                                <%}
                                    else
                                    { %>
                                <h3 class="panel-title">تصنيف جديد</h3>
                                <%} %>
                            </div>
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>


                                        <asp:Panel ID="pnlTextBoxes" runat="server" Visible="False">
                                            <table>
                                                <tr>
                                                    <td>

                                                        <asp:Label ID="LblAlias" runat="server" CssClass="Lbl" Text="Category Code"></asp:Label>
                                                    </td>
                                                    <td>


                                                        <asp:TextBox ID="_ItemGroupCodeText" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="_ItemGroupCodeText" CssClass="LblMessage" ErrorMessage="Item Category  Code is required"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>

                                                        <asp:Label ID="LblGroup" runat="server" CssClass="Lbl" Text="Category Name : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="_ItemGroupNameText" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="_ItemGroupNameText" CssClass="LblMessage" ErrorMessage="Item Category  Name is required"></asp:RequiredFieldValidator>


                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblNodeValue" Visible="false" runat="server"></asp:Label>

                                                        <asp:Label ID="LabelUpload" runat="server" CssClass="Lbl" Text="Upload Image"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="UploadCategory" runat="server" CssClass="form-control" />
                                                        <asp:HyperLink ID="H1" runat="server" Target="_blank">View Image</asp:HyperLink>

                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td colspan="2">

                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <asp:LinkButton ID="BtnAddGroup" runat="server" CssClass="btn btn-primary" OnClick="BtnAddGroup_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:LinkButton ID="BtnCancel" runat="server" CausesValidation="false" CssClass="btn btn-success" OnClick="BtnCancel_Click"><i class="glyphicon glyphicon-refresh"></i> Cancel</asp:LinkButton>


                                                            </div>
                                                            <div class="col-md-3">

                                                                <asp:LinkButton ID="BtnDelete" runat="server" CssClass="btn btn-success" OnClick="BtnDelete_Click" Visible="False" CausesValidation="False"><i class="glyphicon glyphicon-floppy-save"></i> Delete</asp:LinkButton>

                                                            </div>
                                                            <div class="col-md-3">

                                                                <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary" runat="server" OnClick="BtnUpdate_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>

                                                            </div>
                                                        </div>

                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="BtnAddGroup" />
                                        <asp:PostBackTrigger ControlID="BtnUpdate" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AccountantChart.aspx.cs" Inherits="AccountantChart" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function uncheckAll() {
            inputs = document.getElementsByTagName("input");
            for (i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    inputs[i].checked = false;
                }
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        //************************** Treeview Parent-Child check behaviour ****************************//

        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            var t = GetParentByTagName("table", src);
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1) {
                    if (nxtSibling.tagName.toLowerCase() == "div") {
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
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
                var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                if (isAllSiblingsChecked) {
                    checkUncheckSwitch = true;
                }
                else {
                    checkUncheckSwitch = false;
                }
                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;

                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            var k = 0;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (prevChkBox.checked) {
                            //add each selected node one value
                            k = k + 1;
                        }
                    }
                }
            }

            //Finally check any one of child node is select if selected yes then return ture parent node check

            if (k > 0) {
                return true;
            }
            else {
                return false;
            }
        }
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }


    </script>


    <style type="text/css">
        .auto-style1 {
            color: #20B5E4;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

            <div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label ID="Label2" runat="server" CssClass="Lbl" Text="Chart of Account"></asp:Label>
                            <asp:Label ID="LabelErrorMessage1" runat="server" CssClass="LblMessage"></asp:Label>
                            <hr />
                        </div>

                    </div>


                </div>



                <div class="row">

                    <div class="col-md-2">

                        <asp:Button ID="BtnSearch" runat="server" CausesValidation="false" CssClass="fs fa-search" Text="Search" OnClick="BtnSearch_Click" />
                        <asp:Button ID="BtnClearSearch" runat="server" Width="100px" CausesValidation="false" CssClass="fs fa-search" Text="Clear Search" OnClick="BtnClearSearch_Click" />


                    </div>
                    <div class="col-md-4">

                        <asp:TextBox ID="TxtSearch" CssClass="form-control" placeholder="Search by Account Text" runat="server"></asp:TextBox>



                    </div>
                    <div class="col-md-4">

                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="Search by Account Text" runat="server"></asp:TextBox>--%>
                        <asp:RadioButtonList ID="R1" runat="server" DataSourceID="SqlDataSource1" DataTextField="COAChartCode" DataValueField="COAChartCode" RepeatDirection="Horizontal"></asp:RadioButtonList>



                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM [tChartOfAccounts] WHERE (([CompanyID] = @CompanyID) AND ([ParentChartID] = @ParentChartID))">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="7" Name="CompanyID" QueryStringField="7" Type="Int32" />
                                <asp:QueryStringParameter DefaultValue="0" Name="ParentChartID" QueryStringField="0" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>



                    </div>

                    <div class="col-md-2">

                        <asp:Label ID="Label4" runat="server" CssClass="Lbl" Text="Chart of Account"></asp:Label>


                    </div>



                </div>


                <div class="row">


                    <div class="col-md-2">
                        <div>
                            <asp:Button ID="BtnSddMain" runat="server" CssClass="fs fa-search" Width="150px" Text="Add Main Account" OnClick="BtnSddMain_Click" />
                        </div>

                    </div>

                </div>


                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:TreeView ID="TreeView1" Width="50%" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ShowCheckBoxes="All" ShowLines="True" ExpandDepth="6" OnTreeNodeCheckChanged="TreeView1_TreeNodeCheckChanged" OnTreeNodeExpanded="TreeView1_TreeNodeExpanded">
                                <HoverNodeStyle BackColor="#CCCC00" />
                                <LeafNodeStyle ForeColor="#00CC99" />
                                <NodeStyle ForeColor="#CC6600" />
                                <ParentNodeStyle ForeColor="#3333CC" />
                                <RootNodeStyle ForeColor="#CC9900" />
                                <SelectedNodeStyle BackColor="#996600" ForeColor="White" Width="100%" />
                            </asp:TreeView>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12">

                        <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false" runat="server" Visible="False"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
      <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>

                        &nbsp;&nbsp;
      <asp:LinkButton ID="BtnChangeStatus" CssClass="btn btn-success" runat="server" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Change Status</asp:LinkButton>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">


                        <h3>New Chart Account</h3>

                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>


                                    <asp:Panel ID="pnlTextBoxes" runat="server" Visible="False">

                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="Type"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="_Typelist" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="_Typelist_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1" Selected="True">Please select Type</asp:ListItem>
                                                    <asp:ListItem Value="0">Group</asp:ListItem>
                                                    <asp:ListItem Value="1">Account</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>


                                            <div class="col-md-2">
                                                <asp:Label ID="Label1" runat="server" CssClass="Lbl" Text="Account Number:"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtNewAccountNo" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>



                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-md-2">
                                                <asp:Label ID="LblGroup" runat="server" CssClass="Lbl" Text="Group Name:"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="_LedgerNameText" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="_LedgerNameText" CssClass="LblMessage" ErrorMessage="Ledger  Name is required"></asp:RequiredFieldValidator>



                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="LblAlias" runat="server" CssClass="Lbl" Text="Ledger Alias Name : " Visible="False"></asp:Label>
                                            </div>
                                            <div class="col-md-10">
                                                <asp:TextBox ID="_LedgerAliasText" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAlais" runat="server" ControlToValidate="_LedgerAliasText" CssClass="LblMessage" ErrorMessage="Ledger Alias  Name is required" Visible="False"></asp:RequiredFieldValidator>

                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="LblNodeValue" ForeColor="Blue" runat="server"></asp:Label>
                                                <asp:Label ID="LblNodeText" ForeColor="Blue" runat="server"></asp:Label>
                                            </div>

                                            <div class="col-md-5">
                                                <asp:LinkButton ID="BtnAddGroup" runat="server" CssClass="btn btn-primary" OnClick="BtnAddGroup_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:LinkButton ID="BtnCancel" runat="server" CausesValidation="false" CssClass="btn btn-success" OnClick="BtnCancel_Click"><i class="glyphicon glyphicon-refresh"></i> Cancel</asp:LinkButton>
                                            </div>

                                        </div>

                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <%-- </div>--%>
                    </div>
                </div>


            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

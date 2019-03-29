<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchAccount.aspx.cs" Inherits="SearchAccount" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script language="javascript" type="text/javascript">
    function uncheckAll()
    {
        inputs = document.getElementsByTagName("input");
        for(i = 0 ; i<inputs.length; i++)
        {
            if(inputs[i].type=="checkbox")
            {
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT [ChartId], [COAChartCode], [COAChartName], [ParentChartID], [ChartLevel], [AccountType], [ChartCat] FROM [tChartOfAccounts]"></asp:SqlDataSource>
    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

           <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

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

                    <div class="col-md-10">
                        <div>
                            <asp:TextBox ID="TxtSearch" CssClass="form-control" placeholder="Search by Account Text" runat="server"></asp:TextBox>

                        </div>

                    </div>
                    <div class="col-md-2">
                        <div>
                            <asp:Button ID="BtnSearch" runat="server" CssClass="fs fa-search" Text="Search" OnClick="BtnSearch_Click" OnClientClick="uncheckAll();" />
                        </div>

                    </div>


                </div>


                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ChartId" Width="100%">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField DataField="ChartId" HeaderText="ChartId" InsertVisible="False" ReadOnly="True" SortExpression="ChartId" />
                                    <asp:BoundField DataField="COAChartCode" HeaderText="COAChartCode" SortExpression="COAChartCode" />
                                    <asp:BoundField DataField="COAChartName" HeaderText="COAChartName" SortExpression="COAChartName" />
                                    <asp:BoundField DataField="ParentChartID" HeaderText="ParentChartID" SortExpression="ParentChartID" />
                                    <asp:BoundField DataField="ChartLevel" HeaderText="ChartLevel" SortExpression="ChartLevel" />
                                    <asp:BoundField DataField="AccountType" HeaderText="AccountType" SortExpression="AccountType" />
                                    <asp:BoundField DataField="ChartCat" HeaderText="ChartCat" SortExpression="ChartCat" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12">
                        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
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
                         
                       <%-- </div>--%>
                    </div>
                </div>


            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

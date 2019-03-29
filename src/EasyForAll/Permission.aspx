<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Permission.aspx.cs" Inherits="Permission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function CheckOtherIsCheckedByGVID(spanChk) {

            var IsChecked = spanChk.checked;
            if (IsChecked) {
                spanChk.parentElement.parentElement.style.backgroundColor = '#228b22';
                spanChk.parentElement.parentElement.style.color = 'white';
            }
            var CurrentRdbID = spanChk.id;
            var Chk = spanChk;
            Parent = document.getElementById("<%=GvCompany.ClientID%>");
            var items = Parent.getElementsByTagName('input');
            for (i = 0; i < items.length; i++) {
                if (items[i].id != CurrentRdbID && items[i].type == "radio") {
                    if (items[i].checked) {
                        items[i].checked = false;
                        items[i].parentElement.parentElement.style.backgroundColor = 'white'

                        items[i].parentElement.parentElement.style.color = 'black';
                    }
                }
            }
        }
</script>

    <script type="text/javascript">

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
                        <asp:Label ID="Label1" runat="server" Text="Assign Permission Pundles To Menus" CssClass="Title"></asp:Label>
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                        )
    <hr />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">

                        <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>

                    </div>
                </div>

                <div class="row">

                    <div class="col-md-2">

                        <asp:Button ID="BtnSearch" runat="server" Visible="false" CssClass="fs fa-search" Text="Search" OnClick="BtnSearch_Click" />


                        <asp:SqlDataSource ID="SqlDataSourceUsersType" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT UserRoleId, UserRoleName, Description, CompanyId, IsActive, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate FROM tUserRoles ORDER BY UserRoleId" InsertCommand="INSERT INTO tUserRoleDetails(UserRoleId, ApplicationId, MenuId) VALUES (@UserRoleId, 40,10000);
INSERT INTO tUserRoleDetails(UserRoleId, ApplicationId, MenuId) VALUES (@UserRoleId, 40,19000);">
                            <InsertParameters>
                                <asp:SessionParameter Name="UserRoleId" SessionField="UserRoleId" />
                            </InsertParameters>
                        </asp:SqlDataSource>


                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT UserRoleDetailId, UserRoleId, ApplicationId, Permissions, MenuId FROM tUserRoleDetails " InsertCommand="INSERT INTO tUserRoleDetails(UserRoleId, ApplicationId, MenuId) VALUES (@UserRoleId, @ApplicationId, @MenuId)" DeleteCommand="DELETE FROM tUserRoleDetails WHERE (UserRoleId = @UserRoleId )">
                            <DeleteParameters>
                                <asp:SessionParameter Name="UserRoleId " SessionField="UserRoleId " />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:SessionParameter Name="UserRoleId" SessionField="UserRoleId " />
                                <asp:QueryStringParameter DefaultValue="40" Name="ApplicationId" QueryStringField="40" />
                                <asp:SessionParameter Name="MenuId" SessionField="MenuIdd" />
                            </InsertParameters>
                        </asp:SqlDataSource>


                    </div>
                    <div class="col-md-5">

                        <asp:TextBox ID="TxtSearch" CssClass="form-control" Visible="false" placeholder="Search by User Name" runat="server"></asp:TextBox>



                    </div>


                    <div class="col-md-5">

                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceUsersType" DataTextField="UserRoleName" DataValueField="UserRoleId" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" Visible="False"></asp:RadioButtonList>


                    </div>



                </div>


                <div class="row">
                    <div class="col-md-4">
                        <asp:TreeView ID="TreeView1"  runat="server" ShowCheckBoxes="All" ShowLines="True" ExpandDepth="0" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                            <SelectedNodeStyle BackColor="#996600" ForeColor="White" Width="100%" />
                        </asp:TreeView>
                    </div>
                    <div class="col-md-2">
                         <asp:Button ID="ButtonLink" runat="server"  Text="<<---ربط--->>" Width="100%" OnClick="ButtonLink_Click" BackColor="#CCCCCC" Font-Bold="True" Font-Size="X-Large" ForeColor="#339966" Height="40px"  />

                         </div>
                    <div class="col-md-5">
                <asp:GridView ID="GvCompany"  runat="server" AutoGenerateColumns="False" Width="100%"
                      AllowPaging="True"
                      CssClass="table table-bordered"                    
                      AlternatingRowStyle-CssClass="alt"
                      PagerStyle-CssClass="pgr" DataKeyNames="UserRoleId" PageSize="5" OnPageIndexChanging="GvCompany_PageIndexChanging" OnSelectedIndexChanged="GvCompany_SelectedIndexChanged"  >
                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="UserRoleId" HeaderText="UserRole Id"    SortExpression="UserRoleId" InsertVisible="False" ReadOnly="True" />
                       
                        <asp:BoundField DataField="UserRoleName" HeaderText="User Role Name"    SortExpression="UserRoleName" />
                       
                        <asp:BoundField DataField="Description" HeaderText="Description"    SortExpression="Description" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton  ID="C1" runat="server"  onclick="javascript:CheckOtherIsCheckedByGVID(this)"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                    <RowStyle CssClass="Lbl" />
                </asp:GridView>

                    </div>

                </div>


                <div class="row">
                    <div class="col-md-6">


                        <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" CausesValidation="False" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Add New</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;  
 <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false" runat="server" Visible="False"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
      <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>

                        &nbsp;&nbsp;
      <asp:LinkButton ID="BtnChangeStatus" CssClass="btn btn-success" runat="server" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Change Status</asp:LinkButton>


                    </div>


                </div>

            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

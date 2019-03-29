<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserPermission.aspx.cs" Inherits="UserPermission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function CheckOtherIsCheckedByGVIDPundle(spanChk) {

            var IsChecked = spanChk.checked;
            if (IsChecked) {
                spanChk.parentElement.parentElement.style.backgroundColor = '#228b22';
                spanChk.parentElement.parentElement.style.color = 'white';
            }
            var CurrentRdbID = spanChk.id;
            var Chk = spanChk;
            Parent = document.getElementById("<%=GvPundles.ClientID%>");
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
        function CheckOtherIsCheckedByGVIDUsers(spanChk) {

            var IsChecked = spanChk.checked;
            if (IsChecked) {
                spanChk.parentElement.parentElement.style.backgroundColor = '#228b22';
                spanChk.parentElement.parentElement.style.color = 'white';
            }
            var CurrentRdbID = spanChk.id;
            var Chk = spanChk;
            Parent = document.getElementById("<%=GridViewUsers.ClientID%>");
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
                        <asp:Label ID="Label1" runat="server" Text="Assign Permission Pundles To Users" CssClass="Title"></asp:Label>
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

                        <asp:Button ID="BtnSearch" runat="server" CssClass="fs fa-search" Text="Search" />


                        <asp:SqlDataSource ID="SqlDataSourceUsersType" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM [tUserType] ORDER BY [OrderNo]"></asp:SqlDataSource>


                        <asp:SqlDataSource ID="SqlDataSourcePundleMenu" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM [tUserRoleDetails] WHERE ([UserRoleId] = @UserRoleId)">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="UserRoleId" QueryStringField="UserRoleId" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>


                        <asp:SqlDataSource ID="SqlDataSourcePundles" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT tUserRoles.* FROM tUserRoles"></asp:SqlDataSource>


                        <asp:SqlDataSource ID="SqlDataSourceUsers" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT UserId, UserName, EmployeeID, UserRoleId, UserTypeCode, LedgerId, CompanyId FROM tUsers" InsertCommand="INSERT INTO tUserRoleDetails(UserRoleId, ApplicationId, MenuId) VALUES (@UserRoleId, @ApplicationId, @MenuId)" DeleteCommand="DELETE FROM tUserRoleDetails WHERE (UserRoleId = @UserRoleId)" UpdateCommand="UPDATE tUsers SET UserRoleId = @UserRoleId WHERE (UserId = @UserId)">
                            <DeleteParameters>
                                <asp:SessionParameter Name="UserRoleId" SessionField="UserRoleId" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:SessionParameter Name="UserRoleId" SessionField="UserIdd" />
                                <asp:QueryStringParameter DefaultValue="40" Name="ApplicationId" QueryStringField="40" />
                                <asp:SessionParameter Name="MenuId" SessionField="MenuIdd" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:SessionParameter Name="UserRoleId" SessionField="UserRoleId" />
                                <asp:SessionParameter Name="UserId" SessionField="UserId" />
                            </UpdateParameters>
                        </asp:SqlDataSource>


                    </div>
                    <div class="col-md-5">

                        <asp:TextBox ID="TxtSearch" CssClass="form-control" placeholder="Search by User Name" runat="server"></asp:TextBox>



                    </div>


                    <div class="col-md-5">

                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceUsersType" DataTextField="UserTypeText_Ar" DataValueField="UserTypeCode" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"></asp:RadioButtonList>


                    </div>

                     <div class="col-md-3">

                        <asp:Button ID="ButtonSellectAll" runat="server" CssClass="fs fa-search" Text="Select All" OnClick="ButtonSellectAll_Click" width="150px"/>
                         <asp:Button ID="ButtonUnsellectAll" runat="server" CssClass="fs fa-search" Text="Unselect All" OnClick="ButtonUnsellectAll_Click" Width="150px" />

                    </div>

                </div>


                <div class="row">
                    <div class="col-md-4">
                             <asp:GridView ID="GvPundles"  runat="server" AutoGenerateColumns="False" Width="100%"
                      AllowPaging="True"
                      CssClass="table table-bordered"                    
                      AlternatingRowStyle-CssClass="alt"
                      PagerStyle-CssClass="pgr" PageSize="10" OnPageIndexChanging="GvPundles_PageIndexChanging" DataKeyNames="UserRoleId" DataSourceID="SqlDataSourcePundles"  >
                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="UserRoleId" HeaderText="UserRoleId" InsertVisible="False" ReadOnly="True" SortExpression="UserRoleId" />
                        <asp:BoundField DataField="UserRoleName" HeaderText="UserRoleName" SortExpression="UserRoleName" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="CompanyId" HeaderText="CompanyId" SortExpression="CompanyId" Visible="False" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" SortExpression="CreatedDate" Visible="False" />
                        <asp:BoundField DataField="ModifiedBy" HeaderText="ModifiedBy" SortExpression="ModifiedBy"  Visible="False" />
                        <asp:BoundField DataField="ModifiedDate" HeaderText="ModifiedDate" SortExpression="ModifiedDate" Visible="False" />
                        <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" Visible="False" />
                         <asp:TemplateField>
                            <ItemTemplate>
                               <asp:RadioButton  ID="C1" runat="server"  onclick="javascript:CheckOtherIsCheckedByGVIDPundle(this)"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                    <RowStyle CssClass="Lbl" />
                                 <SelectedRowStyle BackColor="#CCFFFF" />
                </asp:GridView>         

                    </div>
                    <div class="col-md-2">
                         <asp:Button ID="ButtonLink" runat="server"  Text="<<---ربط--->>" Width="100%" OnClick="ButtonLink_Click" BackColor="#CCCCCC" Font-Bold="True" Font-Size="X-Large" ForeColor="#339966" Height="40px"  />

                         </div>
                    <div class="col-md-5">
                
<asp:GridView ID="GridViewUsers"  runat="server" AutoGenerateColumns="False" Width="100%"
                      AllowPaging="false"
                      CssClass="table table-bordered"                    
                      AlternatingRowStyle-CssClass="alt"
                      PagerStyle-CssClass="pgr" DataKeyNames="UserId"  OnPageIndexChanging="GvPundles_PageIndexChanging" OnRowCommand="GridViewUsers_RowCommand" OnSelectedIndexChanged="GridViewUsers_SelectedIndexChanged"  >
                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                                          <Columns>
                                              <asp:BoundField DataField="UserId" HeaderText="UserId" InsertVisible="False" ReadOnly="True" SortExpression="UserId" />
                                              <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                                              <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" SortExpression="EmployeeID" Visible="False" />
                                              <asp:BoundField DataField="UserRoleId" HeaderText="UserRoleId" SortExpression="UserRoleId" />
                                              <asp:BoundField DataField="UserTypeCode" HeaderText="UserTypeCode" SortExpression="UserTypeCode" />
                                              <asp:BoundField DataField="LedgerId" HeaderText="LedgerId" SortExpression="LedgerId" Visible="False" />
                                              <asp:BoundField DataField="CompanyId" HeaderText="CompanyId" SortExpression="CompanyId" Visible="False" />
                                               <asp:TemplateField>
                            <ItemTemplate>
                               <asp:RadioButton  ID="C1" runat="server"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                                              <asp:CommandField ShowSelectButton="True" />
                                          </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                    <RowStyle CssClass="Lbl" BackColor="#D9EFFD" />
                    <SelectedRowStyle BackColor="#FFFFCC" />
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


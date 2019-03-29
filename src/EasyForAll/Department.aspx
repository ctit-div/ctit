<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Department.aspx.cs" Inherits="Department" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

   <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

           <div class="row">
      <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Department Info" CssClass="Title"></asp:Label>
          &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                )</div>
               </div>
                   <div class="row"> 
                       <div class="col-md-12">
    
                           <asp:SqlDataSource ID="SqlDataSourceCompany" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT [CompanyID], [CMPCompanyName], [CMPShortName] FROM [tCompanys]"></asp:SqlDataSource>
    
                           <asp:SqlDataSource ID="SqlDataSourceBranch" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT BranchId, CompanyId, BRNBranchName, BRNBranchAddress, BRNEmail, BRNPhone, BRNFax, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsActive FROM tBranches WHERE (CompanyId = @CompanyId)">
                               <SelectParameters>
                                   <asp:ControlParameter ControlID="_CMPlist" Name="CompanyId" PropertyName="SelectedValue" />
                               </SelectParameters>
                           </asp:SqlDataSource>
    
                           <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM View_Department"></asp:SqlDataSource>
    
          <hr />
                                     </div>
                       </div>
        
           <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label2" runat="server" CssClass="Lbl" Text="Company Name"></asp:Label>
           </div>
                 <asp:DropDownList ID="_CMPlist" runat="server" AutoPostBack="True" CssClass="form-control" DataSourceID="SqlDataSourceCompany" DataTextField="CMPCompanyName" DataValueField="CompanyID" OnSelectedIndexChanged="_CMPlist_SelectedIndexChanged">
                 </asp:DropDownList>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>
        <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label7" runat="server" CssClass="Lbl" Text="Branch Name"></asp:Label>
           </div>
                 <asp:DropDownList ID="_BranchList" runat="server" AutoPostBack="True" CssClass="form-control" DataSourceID="SqlDataSourceBranch" DataTextField="BRNBranchName" DataValueField="BranchId" OnSelectedIndexChanged="_BranchList_SelectedIndexChanged" >
                 </asp:DropDownList>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>
<div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label13" runat="server" CssClass="Lbl" Text="Department Name"></asp:Label>
           </div>
                <asp:TextBox ID="_DepartmentNameText" runat="server" CssClass="form-control"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="_DepartmentNameText" CssClass="LblMessage" ErrorMessage="Department Name is required"></asp:RequiredFieldValidator>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>
        <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="Description"></asp:Label>
           </div>
                <asp:TextBox ID="_DIVDescriptionText" runat="server" CssClass="form-control"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_DIVDescriptionText" CssClass="LblMessage" ErrorMessage="Description is required"></asp:RequiredFieldValidator>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>


        <div class="row">

               <div class="col-md-2">
              
            </div>
               </div>

        
                  <div class="row">
             <div class="col-md-6">

               
 <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary"  runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>  
 &nbsp;&nbsp;&nbsp;  
 <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false"  runat="server" OnClick="BtnClear_Click"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
      &nbsp;&nbsp;&nbsp;
      <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary"  runat="server" OnClick="BtnUpdate_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>            

                 &nbsp;&nbsp;
      <asp:LinkButton ID="BtnChangeStatus" CssClass="btn btn-success"  runat="server" OnClick="BtnChangeStatus_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Change Status</asp:LinkButton>            

                 <br />

             </div>

            
                 </div>

                    <div class="row">
             <div class="col-md-3">

                 <br />

                <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>
                 <br />
               </div></div>
                      <div class="row">
             <div class="col-md-12">
                 
                 </div></div>
                    <div class="row">
             <div class="col-md-12">
                <asp:Label ID="LabelId" runat="server" CssClass="Lbl" Visible="False"></asp:Label>
            </div>

                    </div>
                   
        
                 <div class="row">
             <div class="col-md-12">

             	<div class="panel panel-grey margin-bottom-40">
				<div class="panel-heading" >
					<h3 class="panel-title"><i class="icon-globe"></i> All Department</h3>
				</div>
				<div class="panel-body">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


             <asp:Panel ID="pnlTextBoxes" runat="server" Visible="false">  

             </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
                <asp:GridView ID="GvCompany"  runat="server" AutoGenerateColumns="False" Width="100%"
                      AllowPaging="True"
                      CssClass="table table-bordered"                    
                      AlternatingRowStyle-CssClass="alt"
                      PagerStyle-CssClass="pgr" DataKeyNames="DepartmentId" PageSize="5" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GvCompany_SelectedIndexChanged"  >
                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                    <Columns>
                        <%--<asp:CommandField ShowSelectButton="True" />--%>
                        <asp:CommandField ShowSelectButton="True" />
                       
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName" />
                        <asp:BoundField DataField="DepartmentDescription" HeaderText="Department Description" SortExpression="DepartmentDescription" />
                        <asp:BoundField DataField="CMPCompanyName" HeaderText="Company Name" SortExpression="CMPCompanyName" />
                       
                        <asp:BoundField DataField="BRNBranchName" HeaderText="Branch Name" SortExpression="BRNBranchName" />
                        <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" Visible="False" />
                       
                        <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" SortExpression="CreatedDate" Visible="False" />
                        <asp:BoundField DataField="ModifiedBy" HeaderText="ModifiedBy" SortExpression="ModifiedBy" Visible="False" />
                        <asp:BoundField DataField="ModifiedDate" HeaderText="ModifiedDate" SortExpression="ModifiedDate" Visible="False" />
                       
                        <asp:TemplateField HeaderText="DepartmentId" SortExpression="DepartmentId" Visible="False">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("DepartmentId") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("DepartmentId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BranchId" SortExpression="BranchId" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("BranchId") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("BranchId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CompanyId" SortExpression="CompanyId" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CompanyId") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("CompanyId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                    <RowStyle CssClass="Lbl" />
                </asp:GridView>
                           	</div>	
               </div>	
                 </div>
         </div>				
			
  
          </div>
       </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>


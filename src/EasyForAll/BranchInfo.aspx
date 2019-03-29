<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BranchInfo.aspx.cs" Inherits="BranchInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .modalpopup
        {
            background-color: #ADADAD;
            filter: Alpha(Opacity=70);
            opacity: 0.70;
            -moz-opacity: 0.70;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

   <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

           <div class="row">
      <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Branch Info" CssClass="Title"></asp:Label>
          &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                )</div>
               </div>
                   <div class="row"> 
                       <div class="col-md-12">
    
                           <asp:SqlDataSource ID="SqlDataSourceCompany" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT [CompanyID], [CMPCompanyName], [CMPShortName] FROM [tCompanys]"></asp:SqlDataSource>
    
                           <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM View_Branch"></asp:SqlDataSource>
    
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
                <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="Branch Name"></asp:Label>
           </div>
                <asp:TextBox ID="_BRNBranchNameText" runat="server" CssClass="form-control"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_BRNBranchNameText" CssClass="LblMessage" ErrorMessage="Branch Name is required"></asp:RequiredFieldValidator>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>

        <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label4" runat="server" CssClass="Lbl" Text="Branch Address "></asp:Label>
           </div>
                <asp:TextBox ID="_CMPAddressText" runat="server" CssClass="form-control"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="_CMPAddressText" CssClass="LblMessage" ErrorMessage="Branch Address is required"></asp:RequiredFieldValidator>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>


        <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label5" runat="server" CssClass="Lbl" Text="Branch Email"></asp:Label>
           </div>
                <asp:TextBox ID="_EmailText" runat="server" CssClass="form-control"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="_EmailText" CssClass="LblMessage" ErrorMessage="Branch Email is required"></asp:RequiredFieldValidator>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>


        <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label6" runat="server" CssClass="Lbl" Text="Branch Mobile"></asp:Label>
           </div>
                <asp:TextBox ID="_MobileText" runat="server" CssClass="form-control"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="_MobileText" CssClass="LblMessage" ErrorMessage="Branch is required"></asp:RequiredFieldValidator>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

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
					<h3 class="panel-title"><i class="icon-globe"></i> All Branches</h3>
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
                      PagerStyle-CssClass="pgr" DataKeyNames="BranchId" PageSize="5" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GvCompany_SelectedIndexChanged"  >
                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                    <Columns>
                        <%--<asp:CommandField ShowSelectButton="True" />--%>
                        <asp:CommandField ShowSelectButton="True" />
                       
                        <asp:BoundField DataField="BRNBranchName" HeaderText="Branch Name" SortExpression="BRNBranchName" />
                        <asp:BoundField DataField="BRNBranchAddress" HeaderText="Address" SortExpression="BRNBranchAddress" />
                        <asp:BoundField DataField="BRNEmail" HeaderText="Email" SortExpression="BRNEmail" />
                       
                        <asp:BoundField DataField="CMPShortName" HeaderText="Company Name" SortExpression="CMPShortName" />
                        <asp:TemplateField HeaderText="CompanyId" SortExpression="CompanyId" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CompanyId") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("CompanyId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BranchId" SortExpression="BranchId" Visible="False">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("BranchId") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("BranchId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CMPCompanyName" HeaderText="CMPCompanyName" SortExpression="CMPCompanyName" Visible="False" />
                        <asp:BoundField DataField="BRNPhone" HeaderText="BRNPhone" SortExpression="BRNPhone" Visible="False" />
                        <asp:BoundField DataField="BRNFax" HeaderText="BRNFax" SortExpression="BRNFax" Visible="False" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
                       
                        <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" SortExpression="CreatedDate" Visible="False" />
                        <asp:BoundField DataField="ModifiedBy" HeaderText="ModifiedBy" SortExpression="ModifiedBy" Visible="False" />
                        <asp:BoundField DataField="ModifiedDate" HeaderText="ModifiedDate" SortExpression="ModifiedDate" Visible="False" />
                        <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" Visible="False" />
                       
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

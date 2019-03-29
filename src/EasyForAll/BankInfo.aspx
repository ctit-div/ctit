<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BankInfo.aspx.cs" Inherits="BankInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UcSearchAccount.ascx" TagPrefix="uc1" TagName="UcSearchAccount" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

   <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

           <div class="row">
      <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Bank/Cash Info" CssClass="Title"></asp:Label>
          &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                )</div>
               </div>
                   <div class="row"> 
                       <div class="col-md-12">
    
                          <%-- <asp:SqlDataSource ID="SqlDataSourceCompany" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT [CompanyID], [CMPCompanyName], [CMPShortName] FROM [tCompanys]"></asp:SqlDataSource>--%>
    
                           <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM tBanks"></asp:SqlDataSource>
    
          <hr />
                                     </div>
                       </div>
        

               <div class="row">

  <div class="col-md-12">

       <asp:LinkButton ID="LinkButtonAccount" CausesValidation="false" CssClass="btn btn-primary" data-toggle="modal" data-target="#myModal"  runat="server" ><i class="glyphicon glyphicon-floppy-save"></i> Get Account No</asp:LinkButton> 
  <!-- Button trigger modal -->


                    </div>
                       </div>
        <div class="row">
             <div class="col-md-12">
                 <asp:Panel ID="Panel1" Visible="false" runat="server">
                     <uc1:UcSearchAccount runat="server" ID="UcSearchAccount" />
                 </asp:Panel>
                 </div>
               </div>
        <div class="row">

 
             <div class="col-md-6">
                 <asp:Label ID="Label50" runat="server" CssClass="Lbl" Text="Account No"></asp:Label>
                 <div>
                
           </div>
                <asp:TextBox ID="_LedgerIdText" runat="server" Placeholder="Account No will be generated" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="_LedgerIdText" CssClass="LblMessage" ErrorMessage="Account No is required"></asp:RequiredFieldValidator>--%>

                </div>
           
                  <div class="col-md-6">
                 <asp:Label ID="Label5" runat="server" CssClass="Lbl" Text="Account Name"></asp:Label>
                 <div>
                
           </div>
                <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                </div>

              



               </div>
           
        
           <div class="row">
             <div class="col-md-12">
                                <asp:RadioButtonList ID="RadioButtonListAccountType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonListAccountType_SelectedIndexChanged">
 <asp:ListItem Value="0" Selected="True">Bank</asp:ListItem>
                     <asp:ListItem Value="1">Cash</asp:ListItem>
                 </asp:RadioButtonList>
           </div>
                 

                </div>
           

        <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="Bank Name Arabic"></asp:Label>
           </div>
                <asp:TextBox ID="_BNKBankNameArabicText" runat="server" CssClass="form-control"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_BNKBankNameArabicText" CssClass="LblMessage" ErrorMessage="Bank/Cash Name is required"></asp:RequiredFieldValidator>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>

        <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label4" runat="server" CssClass="Lbl" Text="Bank Name English "></asp:Label>
           </div>
                <asp:TextBox ID="_BNKBankNameEnglishText" runat="server" CssClass="form-control"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="_BNKBankNameEnglishText" CssClass="LblMessage" ErrorMessage="Bank/Cash Name English is required"></asp:RequiredFieldValidator>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>


        <%--<div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label5" runat="server" CssClass="Lbl" Text="Bank/Cash Email"></asp:Label>
           </div>
                <asp:TextBox ID="_EmailText" runat="server" CssClass="form-control"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="_EmailText" CssClass="LblMessage" ErrorMessage="Bank/Cash Email is required"></asp:RequiredFieldValidator>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>--%>


                  <div class="row">
             <div class="col-md-3">

 <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary"  runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>  
                 </div>
                <div class="col-md-3">
 <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false"  runat="server" OnClick="BtnClear_Click"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
                      </div>
                <div class="col-md-3">
 <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary"  runat="server" OnClick="BtnUpdate_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>            

                      </div>
                <div class="col-md-3">
      <asp:LinkButton ID="BtnChangeStatus" CssClass="btn btn-success"  runat="server" OnClick="BtnChangeStatus_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Change Status</asp:LinkButton>            

            

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
                      PagerStyle-CssClass="pgr" DataKeyNames="BankId" PageSize="5" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GvCompany_SelectedIndexChanged"  >
                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                    <Columns>
                        <%--<asp:CommandField ShowSelectButton="True" />--%>
                        <asp:CommandField ShowSelectButton="True" />
                       <asp:BoundField DataField="BankId" HeaderText="BankId" SortExpression="BankId" Visible="false" />
                        <asp:BoundField DataField="AccountType" HeaderText="Bank/Cash " SortExpression="AccountType" />
                        <asp:BoundField DataField="BNKBankNameArabic" HeaderText="Bank Name Arabic" SortExpression="BNKBankNameArabic" />
                        <asp:BoundField DataField="BNKBankNameEnglish" HeaderText="Bank Name English" SortExpression="BNKBankNameEnglish" />
                       
                        <%--<asp:BoundField DataField="CMPShortName" HeaderText="Company Name" SortExpression="CMPShortName" />--%>
                        <asp:TemplateField HeaderText="CompanyId" SortExpression="CompanyId" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CompanyId") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("CompanyId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BankId" SortExpression="BankId" Visible="false">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("BankId") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("BankId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LedgerId" HeaderText="Account No" SortExpression="LedgerId" Visible="true" />
                       <%-- <asp:BoundField DataField="BRNPhone" HeaderText="BRNPhone" SortExpression="BRNPhone" Visible="False" />
                        <asp:BoundField DataField="BRNFax" HeaderText="BRNFax" SortExpression="BRNFax" Visible="False" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
                       
                        <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" SortExpression="CreatedDate" Visible="False" />
                        <asp:BoundField DataField="ModifiedBy" HeaderText="ModifiedBy" SortExpression="ModifiedBy" Visible="False" />
                        <asp:BoundField DataField="ModifiedDate" HeaderText="ModifiedDate" SortExpression="ModifiedDate" Visible="False" />
                        <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" Visible="False" />
                       --%>
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

    
  <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Modal Header</h4>
        </div>
        <div class="modal-body">
          <p><div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

          

                <div class="row">

                    <div class="col-md-10">
                        <div>
                            <asp:TextBox ID="TxtSearch" CssClass="form-control" placeholder="Search by Account Text" runat="server"></asp:TextBox>

                        </div>

                    </div>
                    <div class="col-md-2">
                        <div>
                            <asp:Button ID="BtnSearch" runat="server" CausesValidation="false" CssClass="fs fa-search" Text="Search" OnClick="BtnSearch_Click" OnClientClick="uncheckAll();" />
                        </div>

                    </div>


                </div>


                <div class="row">
                    <div class="col-md-3-offset">
                        <div style="text-align: center">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ChartId" Width="50%" style="text-align: center" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField DataField="COAChartCode" HeaderText="COAChartCode" SortExpression="COAChartCode" />
                                    <asp:BoundField DataField="COAChartName" HeaderText="COAChartName" SortExpression="COAChartName" />
                                    <asp:BoundField DataField="ChartId" HeaderText="ChartId" InsertVisible="False" ReadOnly="True" SortExpression="ChartId" Visible="False" />
                                    <asp:BoundField DataField="ParentChartID" HeaderText="ParentChartID" SortExpression="ParentChartID" Visible="False" />
                                    <asp:BoundField DataField="ChartLevel" HeaderText="ChartLevel" SortExpression="ChartLevel" Visible="False" />
                                    <asp:BoundField DataField="AccountType" HeaderText="AccountType" SortExpression="AccountType" Visible="False" />
                                    <asp:BoundField DataField="ChartCat" HeaderText="ChartCat" SortExpression="ChartCat" Visible="False" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
                

       
            </div></p>
        </div>
        <div class="modal-footer">
         <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
           <asp:Button ID="ButtonSavechanges" runat="server" CausesValidation="false" CssClass="fs fa-search" Text="Save changes" OnClick="ButtonSavechanges_Click" />
       
        </div>
      </div>
      
    </div>
  </div>
  



    <!-- Modal -->

</asp:Content>

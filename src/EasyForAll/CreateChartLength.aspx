<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateChartLength.aspx.cs" Inherits="CreateChartLength" %>


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


   <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

           <div class="row">
      <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Create Chart Length" CssClass="Title"></asp:Label>
           &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>
                )</div>
          </div>
              
                   <div class="row"> 
                       <div class="col-md-12">
    
                           <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM View_Company"></asp:SqlDataSource>
    
          <hr />
                                     </div>
                       </div>
        
           <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label2" runat="server" CssClass="Lbl" Text="Company Name"></asp:Label>
           </div>
                <asp:TextBox ID="_CMPCompanyNameText" runat="server" CssClass="form-control"></asp:TextBox>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>

        <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label3" runat="server" CssClass="Lbl" Text="Number of Levels"></asp:Label>
           </div>
               
                 <asp:TextBox ID="_TextBoxCount" runat="server" CssClass="form-control">0</asp:TextBox>
                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>


        <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label4" runat="server" CssClass="Lbl" Text="Chart Account Structure"></asp:Label>
           </div>
                              
        <asp:TextBox ID="TextBoxLength" runat="server" CssClass="form-control">0</asp:TextBox>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>
        
      <div class="row">
             <div class="col-md-12"><div>
                <asp:Label ID="Label8" runat="server" CssClass="Lbl" Text="Company Name"></asp:Label>
           </div>
                              
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


          <%--  <asp:PlaceHolder ID="PlaceHolder1" runat="server">--%>

             <asp:Panel ID="pnlTextBoxes" runat="server" Visible="true">  

             </asp:Panel>

           <%-- </asp:PlaceHolder>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

                </div>
             <div class="col-md-3"><div>
             
          </div> 
</div>

               <div class="col-md-2">
              
            </div>
               </div>
        
                  <div class="row">
             <div class="col-md-6">

               
 <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false"  runat="server" OnClick="BtnClear_Click"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
      <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary"  runat="server" OnClick="BtnUpdate_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>            

               
 <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary"  runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Create Length</asp:LinkButton>  

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
					<h3 class="panel-title"><i class="icon-globe"></i> All Companys</h3>
				</div>
				<div class="panel-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                       <ContentTemplate>
                       </ContentTemplate>
                   </asp:UpdatePanel>


<asp:Panel ID="pnlLongDescription" Visible="false" runat="server" Style="background-color: White; ">
                   <hr />
              
            
  
              
               
            </asp:Panel>
                <asp:GridView ID="GvCompany"  runat="server" AutoGenerateColumns="False" Width="100%"
                      AllowPaging="True"
                      CssClass="table table-bordered"                    
                      AlternatingRowStyle-CssClass="alt"
                      PagerStyle-CssClass="pgr" DataKeyNames="CompanyId" PageSize="5" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GvCompany_SelectedIndexChanged"  >
                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                    <Columns>
                        <%--<asp:CommandField ShowSelectButton="True" />--%>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="CompanyID" HeaderText="Company ID" InsertVisible="False" ReadOnly="True" SortExpression="CompanyID" />
                        <asp:BoundField DataField="CMPCompanyName" HeaderText="Company Name" SortExpression="CMPCompanyName" />
                       
                        <asp:BoundField DataField="CMPShortName" HeaderText="Short Name" />
                        <asp:BoundField DataField="NoOfChartLevels" HeaderText="No Levels" />
                       
                        <asp:BoundField DataField="ChartLastLevelLength" HeaderText="Chart Structure" />
                       
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                    <RowStyle CssClass="Lbl" />
                </asp:GridView>
                           	</div>	
               </div>	
                 </div>
         </div>				
			
  
          </div>
  
</asp:Content>




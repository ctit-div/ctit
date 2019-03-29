<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateChart.aspx.cs" Inherits="CreateChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    

      <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta name="description" content="Easy for All"/>
    <meta name="author" content="Mohammad"/>
    <meta name="keyword" content="Easy for All"/>
    <!-- <link rel="shortcut icon" href="assets/ico/favicon.png"> -->
    <title>Easy for All</title>
    <!-- Icons -->
    
    <!-- Main styles for this application -->
      <% if ( Session["dir"].ToString()=="rtl"){ %>
    <link href="dest/style.css" rel="stylesheet"/>
      <%}else{ %>

    <link href="css/style.css" rel="stylesheet" />
      <%} %>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

           <div class="row">
      <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Create Chart Length" CssClass="Title"></asp:Label>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
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
      &nbsp;&nbsp;
      <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary"  runat="server" OnClick="BtnUpdate_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>            

               
 &nbsp;            

               
 <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary"  runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Create Length</asp:LinkButton>  

                 &nbsp;

               
 <asp:LinkButton ID="BtnBack" CssClass="btn btn-success" CausesValidation="false"  runat="server" OnClick="BtnBack_Click" ><i class="glyphicon glyphicon-refresh"></i> Back</asp:LinkButton>

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
    </div>
    </form>
</body>
</html>

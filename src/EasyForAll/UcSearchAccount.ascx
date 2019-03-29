<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcSearchAccount.ascx.cs" Inherits="UcSearchAccount" %>
    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

           <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

          

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
                

       
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

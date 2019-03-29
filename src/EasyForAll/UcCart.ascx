<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcCart.ascx.cs" Inherits="UcCart" %>
<asp:GridView ID="GridView1" runat="server" AllowPaging="True"  CssClass="table table-bordered"
                                        AlternatingRowStyle-CssClass="alt"
                                        PagerStyle-CssClass="pgr"
                            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ItemId" 
                            EmptyDataText="No Items List"  Width="100%">
                            <Columns>
                                <asp:TemplateField Visible="true">
                                    <HeaderTemplate>
                                        <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" 
              runat="server" type="checkbox" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" SortExpression="ItemId">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductCode" runat="server" SkinID="lbl" 
                                            Text='<%# Eval("ItemId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="ItemName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductName" runat="server" SkinID="txt" 
                                            Text='<%# Eval("ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" SortExpression="ItemQty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty0" runat="server"  
                                            SkinID="txt" Text='<%# Eval("Quantity") %>' Width="50"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price" SortExpression="Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server" ReadOnly="true" SkinID="txt" 
                                            Text='<%# Eval("Price") %>' Width="50">
                    </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                            </Columns>
     <PagerStyle CssClass="pgr"></PagerStyle>
                                        <RowStyle CssClass="Lbl" />
                        </asp:GridView>




<%--                                                                           <% if (Session["dir"].ToString() == "ltr")
                                                                              { %> <br />--%>
 <asp:Label ID="lblTotal" runat="server" Text="Total Amount"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtTotal" runat="server"  Width="125px"></asp:TextBox>
<br />
   <asp:Button ID="btnContinue" runat="server" CssClass="Button" 
                    onclick="btnContinue_Click" SkinID="btn" Text="Continue Shoping" 
                    Width="150px" />
                <asp:Button ID="btnCheckOut" runat="server" CssClass="Button" 
                    onclick="btnCheckOut_Click" SkinID="btn" Text="Checkout" Width="130px" />
                <asp:Button ID="btnUpdate" runat="server" CssClass="Button" 
                    onclick="btnUpdate_Click" SkinID="btn" Text="Update" Width="125px" />
                <asp:Button ID="btnDelete" runat="server" CssClass="Button" 
                    onclick="btnDelete_Click" 
                    OnClientClick=" return confirm('Do u want to Delete the selected items');" 
                    SkinID="btn" Text="Delete" Width="120px" />
<%--<%} %>



                                                                           <% else if (Session["dir"].ToString() == "rtl")
                                                                              { %> <br />
 <asp:Label ID="Label1" runat="server" Text="اجمالي المبلغ"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TextBox1" runat="server" SkinID="txt" Width="125px"></asp:TextBox>
<br />
   <asp:Button ID="Button1" runat="server" CssClass="Button" 
                    onclick="btnContinue_Click" SkinID="btn" Text="الاستمرار بالتسوق" 
                    Width="150px" />
                <asp:Button ID="Button2" runat="server" CssClass="Button" 
                    onclick="btnCheckOut_Click" SkinID="btn" Text="تأكيد الطلب" Width="130px" />
                <asp:Button ID="Button3" runat="server" CssClass="Button" 
                    onclick="btnUpdate_Click" SkinID="btn" Text="تعديل الطلبية" Width="125px" />
                <asp:Button ID="Button4" runat="server" CssClass="Button" 
                    onclick="btnDelete_Click" 
                    OnClientClick=" return confirm('هل انت متأكد من حذف المنتج المحدد؟');" 
                    SkinID="btn" Text="حذف" Width="120px" />
<%} %>--%>

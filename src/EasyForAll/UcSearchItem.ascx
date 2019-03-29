<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcSearchItem.ascx.cs" Inherits="UcSearchItem" %>


<div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">
                           <div class="row">
                                   
                    <div class="col-md-9">
                        <div>
                            <asp:TextBox ID="TxtSearchItem" CssClass="form-control" placeholder="Search by Item" runat="server"></asp:TextBox>

                        </div>

                    </div>
                    <div class="col-md-3">
                        <div>
                            <asp:Button ID="BtnSearchItem" runat="server" class="btn btn-info btn-sm" Text="Search" CausesValidation="False" OnClick="BtnSearchItem_Click" Width="100%" />
                        </div>

                    </div>

                           </div>
                            
                        </div>
        
  
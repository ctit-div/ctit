<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
        .style2
        {
            color: #000099;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <p>
        <span class="style1">Thanks for purchasing from our warehouse, the order will be delivered to you as soon as 
        payment is done</p>
    <p>
        <span class="style2"><strong>Our Bank Account&nbsp; is 4002140001241</strong></p>
    <p>
        <strong>Alrajhi Bank</strong></span></p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Amount"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label3" runat="server" Text="Order Number"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        Please provide us with transaction number.</span></p>
</asp:Content>


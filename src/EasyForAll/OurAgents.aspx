<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OurAgents.aspx.cs" Inherits="OurAgents" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>






<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript">
    $(document).ready(function()
  {
var tdImg = $("#<%=DataList1.ClientID%>.tdContainer");
tdImg.attr("style",'width:150px;float:left;');
)
  }
</script>

    <style type="text/css">
        .hidebr br {
            display: none;
        }


        .auto-style1 {
            color: #0099FF;
        }

        .auto-style2 {
            color: #333333;
            text-align: left;
        }

        .auto-style3 {
            text-align: left;
        }
    </style>
    <style type="text/css">
        .Lbl {
            font-size: 44pt;
            color: #0099FF;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
    </style>
    <style type="text/css">
        .vertical-align {
            text-align: center;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

           <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">
                    <div class="col-md-2">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div runat="server" style="align-content: center; font-size: x-large; text-align: center;">
                            <asp:Label ID="Label2" runat="server" CssClass="Lbl" Text="Our Agents"></asp:Label>
                        </div>

                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                    </div>
                </div>




                <div class="row">
                    <div class="col-md-12">

                        <div runat="server" style="align-content: center; font-size: x-large; text-align: center;">
                            <asp:DataList ID="DataList1" runat="server" DataKeyField="AgentCode" CssClass="hidebr" RepeatColumns="1" Width="100%">
                                <ItemStyle VerticalAlign="Top"
                                    CssClass="tdContainer" />
                                <ItemTemplate>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="UserNameLabel" runat="server" CssClass="auto-style1" Style="color: #0099CC; font-size: x-large;" Text='<%# Eval("UserName") %>' />

                                        </div>
                                    </div>

<div class="row">
                                        <div dir="rtl" class="col-md-8">
                                            <asp:TextBox ID="TextBox1" runat="server" Height="400px" Text='<%# Eval("Address") %>' TextMode="MultiLine" Width="100%" Enabled="False" Font-Size="Medium" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>

                                        </div>
<div class="col-md-4">

                                            <a href='<%# DataBinder.Eval(Container.DataItem,"LogoImage") %>' target="_blank" style="clip: rect(1px, auto, auto, auto)">
                                                <asp:Image ID="picture" runat="server" Height="400px" ImageUrl='<%# Eval("LogoImage") %>' Width="100%" />
                                            </a>
                                        </div>
                                    </div>

                                   



                                    <div class="row">
                                        <div class="col-md-12">
                                            <%--<asp:Image ID="Image1" runat="server" Style="vertical-align: top; text-align: left;" ImageUrl="~/img/Divider.png" />--%>
                                            <asp:Label ID="AgentCodeLabel" runat="server" Text='<%# Eval("AgentCode") %>' Visible="False" />
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:DataList>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>





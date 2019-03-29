<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TrailBalance.aspx.cs" Inherits="TrailBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <link type="text/css" rel="Stylesheet" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />

<script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.5.js"></script> 

     <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/jquery-ui.js"></script>
    <style>

       .ui-button { margin-left: -1px; }

       .ui-button-icon-only .ui-button-text { padding: 0.35em; }

       .ui-autocomplete-input { margin: 0; padding: 0.48em 0 0.47em 0.45em; }

       </style>
       <script>
           function optionSelected(selectedValue) {
              document.title = selectedValue;
           }
           (function ($) {
               $.widget("ui.combobox", {
                   _create: function () {
                       var self = this,
                                  select = this.element.hide(),
                                  selected = select.children(":selected"),
                                  value = selected.val() ? selected.text() : "";
                       var input = this.input = $("<input>")
                                  .insertAfter(select)
                                  .val(value)
                                  .autocomplete({
                                      delay: 0,
                                      minLength: 0,
                                      source: function (request, response) {
                                          var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                                          response(select.children("option").map(function () {
                                              var text = $(this).text();
                                              if (this.value && (!request.term || matcher.test(text)))
                                                  return {
                                                      label: text.replace(
                                                                           new RegExp(
                                                                                  "(?![^&;]+;)(?!<[^<>]*)(" +
                                                                                  $.ui.autocomplete.escapeRegex(request.term) +
                                                                                  ")(?![^<>]*>)(?![^&;]+;)", "gi"
                                                                           ), "<strong>$1</strong>"),
                                                      value: text,
                                                      option: this
                                                  };
                                          }));
                                      },
                                      select: function (event, ui) {
                                          ui.item.option.selected = true;
                                          self._trigger("selected", event, {
                                              item: ui.item.option
                                          });
                            optionSelected(ui.item.option.value);
                                      },
                                      change: function (event, ui) {
                                          if (!ui.item) {
                                              var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i"),
                                                              valid = false;
                                              select.children("option").each(function () {
                                                  if ($(this).text().match(matcher)) {
                                                      this.selected = valid = true;
                                                      return false;
                                                  }
                                              });
                                              if (!valid) {
                                                  // remove invalid value, as it didn't match anything
                                                  $(this).val("");
                                                  select.val("");
                                                  input.data("autocomplete").term = "";
                                                  return false;
                                              }
                                          }
                                      }
                                  })
                                  .addClass("ui-widget ui-widget-content ui-corner-right");
                       input.data("autocomplete")._renderItem = function (ul, item) {
                           return $("<li></li>")
                                         .data("item.autocomplete", item)
                                         .append("<a>" + item.label + "</a>")
                                          .appendTo(ul);
                       };
                       this.button = $("<button type='button'>&nbsp;</button>")
                                  .attr("tabIndex", -1)
                                  .attr("title", "Show All Items")
                                  .insertAfter(input)
                                  .button({
                                      icons: {
                                          primary: "ui-icon-triangle-1-s"
                                      },
                                      text: false
                                  })
                                  .removeClass("ui-corner-all")
                                  .addClass("ui-corner-right ui-button-icon")
                                  .click(function () {
                                      // close if already visible
                                      if (input.autocomplete("widget").is(":visible")) {
                                          input.autocomplete("close");
                                          return;
                                      }
                                      // pass empty string as value to search for, displaying all results
                                      input.autocomplete("search", "");
                                      input.focus();
                                  });
                   },
                   destroy: function () {
                       this.input.remove();
                       this.button.remove();
                       this.element.show();
                       $.Widget.prototype.destroy.call(this);
                   }
               });
           })(jQuery);
         

    </script>

 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>
           <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="Label1" runat="server" Text="Opening Accounts Info" CssClass="Title"></asp:Label>
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>)
                    </div>

                    <div class="col-md-9">

                

                    </div>
                </div>
           <div class="row">
                    <div class="col-md-12">

                        <div class="panel panel-grey margin-bottom-40">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="icon-globe"></i>Opening Balance</h3>
                            </div>
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>


                                        <asp:Panel ID="pnlTextBoxes" runat="server" Visible="false">
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <%--<asp:GridView ID="GvCompany" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True"
                                    CssClass="table table-bordered"
                                    AlternatingRowStyle-CssClass="alt"
                                    PagerStyle-CssClass="pgr" DataKeyNames="VoucherID" PageSize="5" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GvCompany_SelectedIndexChanged">
                                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:TemplateField ShowHeader="False" Visible="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Status"
                                                    CommandArgument='<%# Bind("VoucherID") %>' CssClass="btn btn-danger btn-xs" OnClientClick="return confirm('Are you sure to change status, confirm Change Status?');"> Change Status<i class="icon-trash" style="font-size:16px;"> </i> &nbsp;&nbsp;&nbsp;&nbsp;</asp:LinkButton>
                                                &nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="VoucherID" HeaderText="VoucherID" InsertVisible="False" ReadOnly="True" SortExpression="VoucherID" />
                                        <asp:BoundField DataField="VOMManualVoucherNo" HeaderText="VOMVoucherNo" SortExpression="VOMManualVoucherNo" />

                                        <asp:BoundField DataField="VOMVoucherNo" HeaderText="VOMVoucherNo" />
                                        <asp:BoundField DataField="VOMVoucherDate" HeaderText="Date" />
                                        <asp:BoundField DataField="VOMVoucherType" HeaderText="VOMVoucherType" />

                                    </Columns>
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <RowStyle CssClass="Lbl" />
                                </asp:GridView>--%>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <%--<div class="row">
                    <div class="col-md-3">
                        <asp:LinkButton ID="BtnSave" CssClass="btn btn-primary" runat="server" OnClick="BtnSave_Click"><i class="glyphicon glyphicon-floppy-save"></i> Save</asp:LinkButton>
                        </div>
                      <div class="col-md-3">
 <asp:LinkButton ID="BtnClear" CssClass="btn btn-success" CausesValidation="false" runat="server" OnClick="BtnClear_Click"><i class="glyphicon glyphicon-refresh"></i> Clear</asp:LinkButton>
                        </div>
                      <div class="col-md-3">
      <asp:LinkButton ID="BtnUpdate" CssClass="btn btn-primary" runat="server" OnClick="BtnUpdate_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Update</asp:LinkButton>

                    </div>
                      <div class="col-md-3">
      <asp:LinkButton ID="BtnChangeStatus" CssClass="btn btn-success" runat="server" OnClick="BtnChangeStatus_Click" Visible="False"><i class="glyphicon glyphicon-floppy-save"></i> Change Status</asp:LinkButton>



                    </div>


                </div>--%>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>
                        <br />
                    </div>

                </div>
                
               
                


            </div>
        </ContentTemplate>

    </asp:UpdatePanel>


</asp:Content>






<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
    <link href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet">   
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
<link rel="stylesheet" href="http://cdn.datatables.net/1.10.2/css/jquery.dataTables.min.css" />
<script type="text/javascript" src="http://cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br /><br /><br />
   <div class="container">
<div class="row header" style="text-align:center;color:green">
<h3>Bootstrap Table With sorting,searching and paging using dataTable.js (Responsive)</h3>
</div>
  <asp:Repeater ID="rptCustomers" runat="server">
        <HeaderTemplate>
           <table id="myTable" class="table table-striped" >  
                <thead>
                    <tr>
                        <th>
                            Chart Code 
                        </th>
                        <th>
                            Chart Name
                        </th>
                        <th>
                            Chart Level
                        </th>
                    </tr>
                </thead>
                </HeaderTemplate>
        <ItemTemplate>
                <tbody>
    
            <tr>
                <td>
                    <asp:Label ID="lblCOAChartCode" runat="server" Text='<%# Eval("COAChartCode") %>' />
                </td>
                <td>
                    <asp:Label ID="lblCOAChartName" runat="server" Text='<%# Eval("COAChartName") %>' />
                </td>
                <td>
                    <asp:Label ID="lblChartLevel" runat="server" Text='<%# Eval("ChartLevel") %>' />
                </td>
            </tr>
      

                      </ItemTemplate>
        <FooterTemplate>
            </tbody> </table>
        </FooterTemplate>
    </asp:Repeater>

   </div>     
      <script>
$(document).ready(function(){
    $('#myTable').dataTable();
});
</script>

</asp:Content>

--%>




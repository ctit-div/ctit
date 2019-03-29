<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JournalVoucher.aspx.cs" Inherits="JournalVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="Stylesheet" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />

    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.5.js"></script>

    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/jquery-ui.js"></script>
    <style>
        .ui-button {
            margin-left: -1px;
        }

        .ui-button-icon-only .ui-button-text {
            padding: 0.35em;
        }

        .ui-autocomplete-input {
            margin: 0;
            padding: 0.48em 0 0.47em 0.45em;
        }
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
        $(function () {

            $("#<%= _ddPayedTo.ClientID%>").combobox();

            $("#toggle").click(function () {

                $("#<%= _ddPayedTo.ClientID%>").toggle();

            });

        });

    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>
            <div class="main" style="font-family: Noor, Tahoma, Geneva, Verdana, sans-serif">

                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="Label1" runat="server" Text="Journal Info" CssClass="Title"></asp:Label>
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>)
                    </div>

                    <div class="col-md-6">

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT VoucherID, CompanyID, FinYearID, VOMManualVoucherNo, VOMVoucherNo, VOMVoucherDate, VOMVoucherType, VOMPayedReceivedBy, VOMPayRepType, VOMBankType, VOMBankId, VOMChequeNo, VOMChequePayeeName, VOMChequeDate, VOMBankTransferDate, VOMPlaceOfIssue, VOMDescription, CreatedBy, CreatedDate, VOMStatus, IsSingle, Posted, Deleted FROM tVoucherMasters WHERE (VOMVoucherType = 'J')"></asp:SqlDataSource>

                    </div>
                </div>


                <div class="row">


                    <div class="col-md-2">
                        <div>
                            <asp:Label ID="LabelVoucherNo" runat="server" CssClass="Lbl" Text="Voucher No"></asp:Label>
                        </div>
                        <asp:TextBox ID="_VoucherNoText" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_VoucherNoText" CssClass="LblMessage" ErrorMessage="Voucher No is required"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-2">
                        <div>
                            <div>
                                <asp:Label ID="Label2" runat="server" Text="Voucher Date"></asp:Label>
                            </div>

                            <asp:TextBox ID="_VoucherDateText" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/Calendar.gif" OnClick="ImageButton1_Click" CausesValidation="False" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server" ControlToValidate="_VoucherDateText" CssClass="LblMessage" ErrorMessage="Voucher Date is required"></asp:RequiredFieldValidator>

                            <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" Visible="False" Height="16px"></asp:Calendar>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <div>
                            <asp:Label ID="Label4" runat="server" CssClass="Lbl" Text="Transaction Type "></asp:Label>
                        </div>
                        <asp:DropDownList ID="_ddReceiptType" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="_ddReceiptType_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                            <asp:ListItem Value="1">Debit</asp:ListItem>
                            <asp:ListItem Value="2">Credit</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-3">
                        <div>


                            <asp:Label ID="Label6" runat="server" CssClass="Lbl" Text="Ledger"></asp:Label>
                        </div>
                        <div class="demo">
                            <div class="ui-widget">
                                <asp:DropDownList ID="_ddPayedTo" runat="server" AutoPostBack="false" CssClass="form-control">
                                    <asp:ListItem Value="0">--- Please Select ---</asp:ListItem>
                                </asp:DropDownList>
                                <%-- <button id="toggle">Show underlying select</button>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div>

                            <asp:Label ID="Label116" runat="server" CssClass="Lbl" Text="Branch Name"></asp:Label>
                        </div>

                        <asp:DropDownList ID="_ddBranchName" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceBranch" DataTextField="BRNBranchName" DataValueField="BranchId" AutoPostBack="True" OnSelectedIndexChanged="_ddBranchName_SelectedIndexChanged">
                            <asp:ListItem Value="0">--- Please Select ---</asp:ListItem>

                        </asp:DropDownList>
                    </div>
                </div>



                <div class="row">

                    <div class="col-md-3">
                        <div>
                            <br />
                            <asp:Label ID="Label9" runat="server" CssClass="Lbl" Text="Department Name"></asp:Label>
                        </div>

                        <asp:DropDownList ID="_ddDepartment" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceDepartment" DataTextField="DepartmentName" DataValueField="DepartmentId" AutoPostBack="True" OnSelectedIndexChanged="_ddDepartment_SelectedIndexChanged">
                            <asp:ListItem Value="0">--- Please Select ---</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                    <div class="col-md-3">
                        <div>
                            <br />
                            <asp:Label ID="Label17" runat="server" CssClass="Lbl" Text="Devision Name"></asp:Label>
                        </div>

                        <asp:DropDownList ID="_ddUnitName" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceDivision" DataTextField="DIVDivisionName" DataValueField="DivisionId">
                            <asp:ListItem Value="0">--- Please Select ---</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                    <div class="col-md-3">
                        <div>


                            <br />

                            <asp:Label ID="Label18" runat="server" CssClass="Lbl" Text="Currency"></asp:Label>
                        </div>

                        <asp:DropDownList ID="_ddCurrency" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="_ddCurrency_SelectedIndexChanged">
                            <asp:ListItem Value="0">--- Please Select ---</asp:ListItem>

                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <div>

                            <br />
                            <asp:Label ID="Label19" runat="server" CssClass="Lbl" Text="Foriegn Currency Amount"></asp:Label>
                        </div>
                        <asp:TextBox ID="_TxtCurrency" runat="server" CssClass="form-control" Enabled="False" AutoPostBack="True" OnTextChanged="_TxtCurrency_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="_TxtCurrency" CssClass="LblMessage" ErrorMessage="Foriegn Currency Amount is required"></asp:RequiredFieldValidator>


                    </div>


                </div>
                <div class="row">


                    <div class="col-md-3">
                        <div>



                            <br />

                            <asp:Label ID="Label20" runat="server" CssClass="Lbl" Text="Exchange Rate"></asp:Label>
                        </div>
                        <asp:TextBox ID="_TxtValue" runat="server" CssClass="form-control" Enabled="False" AutoPostBack="True" OnTextChanged="_TxtValue_TextChanged">0</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="_TxtValue" CssClass="LblMessage" ErrorMessage="Currency value is required"></asp:RequiredFieldValidator>


                    </div>
                    <div class="col-md-3">
                        <div>
                            <br />
                            <asp:Label ID="Label8" runat="server" CssClass="Lbl" Text="Amount in Local Currency"></asp:Label>
                        </div>
                        <asp:TextBox ID="_TxtAmount" runat="server" CssClass="form-control">0</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="_TxtAmount" CssClass="LblMessage" ErrorMessage="Password is required"></asp:RequiredFieldValidator>

                    </div>

                    <div class="col-md-6">

                        <div>
                            <div>
                                <asp:Label ID="Label7" runat="server" CssClass="Lbl" Text="Description"></asp:Label>
                            </div>
                            <asp:TextBox ID="_TxtDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="_TxtDescription" CssClass="LblMessage" ErrorMessage="Description is required"></asp:RequiredFieldValidator>

                        </div>


                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3">
                        <asp:LinkButton ID="BtnAdd" CssClass="btn btn-success" runat="server" Visible="True" OnClick="BtnAdd_Click"><i class="glyphicon glyphicon-floppy-save"></i> Add</asp:LinkButton>



                    </div>
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


                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LabelErrorMessage" runat="server" CssClass="LblMessage"></asp:Label>

                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:SqlDataSource ID="SqlDataSourceDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT DepartmentId, BranchId, DepartmentName, DepartmentDescription, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsActive, BRNBranchName, CMPCompanyName, CompanyId FROM View_Department WHERE (BranchId = @DepartmentId)  and (IsActive = 1)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="_ddBranchName" Name="DepartmentId" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSourceCompany" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT CompanyID, CMPCompanyName, CMPShortName, IsActive FROM tCompanys WHERE (IsActive = 1)"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSourceBranch" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT BranchId, CompanyId, BRNBranchName, BRNBranchAddress FROM tBranches WHERE (CompanyId = @CompanyId) and (IsActive = 1)">
                            <SelectParameters>
                                <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSourceDivision" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT DivisionId, DepartmentId, DIVDivisionName, DIVDescription, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsActive, CMPCompanyName, BRNBranchName, DepartmentName, CompanyId, BranchId FROM View_Division WHERE (DepartmentId = @DepartmentId) and (IsActive = 1)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="_ddDepartment" Name="DepartmentId" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSourceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT * FROM [tVoucherDetails]"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSourceCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:FinanceConnStr %>" SelectCommand="SELECT CurrencyId, CurrencyName + '(' + CurrencySymbol + ')'  as CurrencyName, Status, IsLocal FROM tCurrency WHERE (Status = 1)"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LabelId" runat="server" CssClass="Lbl" Visible="False"></asp:Label>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12">

                        <div class="panel panel-grey margin-bottom-40">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="icon-globe"></i>All Receipt Voucher</h3>
                            </div>
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>


                                        <asp:Panel ID="pnlTextBoxes" runat="server" Visible="false">
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:GridView ID="GvCompany" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True"
                                    CssClass="table table-bordered"
                                    AlternatingRowStyle-CssClass="alt"
                                    PagerStyle-CssClass="pgr" DataKeyNames="ManualVoucherNo" PageSize="5"  OnSelectedIndexChanged="GvCompany_SelectedIndexChanged">
                                    <AlternatingRowStyle CssClass="Lbl"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        
                                        <asp:BoundField DataField="ManualVoucherNo" HeaderText="ManualVoucherNo"  SortExpression="ManualVoucherNo" />
                                        <asp:BoundField DataField="VoucherDate" HeaderText="VoucherDate" SortExpression="VoucherDate" />

                                        <asp:BoundField DataField="Debit" HeaderText="Debit" />
                                        <asp:BoundField DataField="Credit" HeaderText="Credit" />
                                        <asp:BoundField DataField="ForeignAmount" HeaderText="ForeignAmount" />
                                         <asp:BoundField DataField="ExchangeRate" HeaderText="ExchangeRate" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="Ledger" HeaderText="Ledger" />
                                    </Columns>
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <RowStyle CssClass="Lbl" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                 <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="Label3" runat="server" Text="Debt" CssClass="Lbl" Visible="true"></asp:Label>
                    </div>
                      <div class="col-md-3">
                        <asp:Label ID="LblDebt" runat="server" Text="0" CssClass="Lbl" Visible="true"></asp:Label>
                    </div>
                      <div class="col-md-3">
                        <asp:Label ID="Label10" runat="server" Text="Credit" CssClass="Lbl" Visible="true"></asp:Label>
                    </div>
                      <div class="col-md-3">
                        <asp:Label ID="LblCredit" runat="server" Text="0" CssClass="Lbl" Visible="true"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>


</asp:Content>





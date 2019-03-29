$.chart = {};
var ledgers = [];
var charts = [];
$("#errGroupName").hide();
$("#errLedgerName").hide();
$('[data-showTooltip="yes"]').tooltip();
$("#divGroupDetails").dialog({
    autoOpen: false,
    closeOnEscape: true,
    resizable: false,
    width: "600px",
    title: "Add Group",
    modal: true,
    open: function (event, ui) {
        //hide close button.
        $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
    }
});

$("#divLedgerDetails").dialog({
    autoOpen: false,
    closeOnEscape: true,
    resizable: false,
    width: "600px",
    title: "Add Group/Ledger",
    modal: true,
    open: function (event, ui) {
        //hide close button.
        $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
    }
});

$("#cmbChartType").on("change", function () {
    if ($(this).val() == "1") {
        $("#divChartType").show();
        $(".chartGroup").show();
        $(".chartAccount").hide();
    }
    else {
        $(".chartGroup").hide();
        $(".chartAccount").show();
    }
});

$.chart.AddGroupOrLedger = function (e) {
    var obj = $(this);
    var companyChartLevel = parseFloat($("#hidCompanyChartLevel").val());
    var currentLevel = 0
    try { currentLevel = parseFloat($(".treegrid-" + obj.attr("dataId")).treegrid('getDepth')) + 1; }
    catch (e) { }
    $("#txtGroupName").val("");
    $("#txtLedgerName").val("");
    $("#txtLedgerAlias").val("");
    if (currentLevel < (companyChartLevel - 2)) {
        $("#hidChartId").val("0");
        $("#hidParentId").val(obj.attr("dataId"));
        
        $("#cmbChartType").val("1").trigger("change");
        $("#divChartType").show();
        $(".chartGroup").show();
        $(".chartAccount").hide();
        $("#divLedgerDetails").dialog("open");
    }
    else {
        $("#hidLedgerId").val("0");
        $("#hidParentId").val(obj.attr("dataId"));
        $("#chkDebit").attr("checked", "checked");

        $("#cmbChartType").val("2").trigger("change");
        $("#divChartType").hide();
        $(".chartGroup").hide();
        $(".chartAccount").show();
        $("#divLedgerDetails").dialog("open");
        //alert("Add Ledger Under Construction");
    }
}

$.chart.EditGroupOrLedger = function (e) {
    var obj=$(this);
    var companyChartLevel = parseFloat($("#hidCompanyChartLevel").val());
    var currentLevel = 0
    try{currentLevel=parseFloat($(".treegrid-" + obj.attr("dataId")).treegrid('getDepth')) + 1;}
    catch (e) { }
    if (currentLevel < (companyChartLevel - 1) && $(this).attr("chartOrLedger")=="chart") {

        $("#hidChartId").val(obj.attr("dataId"));
        $("#hidParentId").val(obj.attr("parentId"));
        var chartsId = obj.attr("dataId");
        $.each(charts, function (index, data) {
            if (data.Id == chartsId) {
                $("#txtGroupName").val(data.Name);
            }
        });

        $("#cmbChartType").val("1").trigger("change");
        $("#divChartType").hide();
        $(".chartGroup").show();
        $(".chartAccount").hide();
        $("#divLedgerDetails").dialog("open");
    }
    else {
        
        $("#hidParentId").val(obj.attr("parentId"));
        var ledgerId = parseFloat(obj.attr("dataId")) - parseFloat(contantNumber);
        $("#hidLedgerId").val(ledgerId);
        $.each(ledgers, function (index, data) {
            if (data.Id == ledgerId) {
                $("#txtLedgerName").val(data.Name);
                $("#txtLedgerAlias").val(data.AliasName);
            }
        });
        $("#cmbChartType").val("2").trigger("change");
        $("#divChartType").hide();
        $(".chartGroup").hide();
        $(".chartAccount").show();
        $("#divLedgerDetails").dialog("open");
    }
}

$.chart.DeleteGroupOrLedger = function (e) {
    var obj = $(this);
    var obj = $(this);
    var companyChartLevel = parseFloat($("#hidCompanyChartLevel").val());
    var currentLevel = 0
    try { currentLevel = parseFloat($(".treegrid-" + obj.attr("dataId")).treegrid('getDepth')) + 1; }
    catch (e) { }
    if (currentLevel < (companyChartLevel - 1) && $(this).attr("chartOrLedger") == "chart") {
        $.ajax({
            type: "POST",
            url: "/Chart/DeleteGroup",
            data: { 'id': obj.attr("dataId") },
            cache: false,
            beforeSend: function () {
                $(".bgLoading").show();
            },
            success: function (result) {
                var queryResult = $.parseJSON(result);
                if (queryResult.ErrorNo == 0) {
                    $(".treegrid-" + obj.attr("dataId")).remove();
                }
                else {
                    $.common.alert(alertMessage, queryResult.ErrorDesc);
                }
            },
            error: function (request, status, error) {
            },
            complete: function () {
                $(".bgLoading").hide();
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Chart/DeleteLedger",
            data: { 'id': obj.attr("dataId") },
            cache: false,
            beforeSend: function () {
                $(".bgLoading").show();
            },
            success: function (result) {
                var queryResult = $.parseJSON(result);
                if (queryResult.ErrorNo == 0) {
                    $(".treegrid-" + obj.attr("dataId")).remove();
                }
                else {
                    $.common.alert(alertMessage, queryResult.ErrorDesc);
                }
            },
            error: function (request, status, error) {
            },
            complete: function () {
                $(".bgLoading").hide();
            }
        });
    }
}

$("#btnCancelChart").on("click", function (e) {
    $("#divGroupDetails").dialog("close");
});

function saveGroup(companyId, parentId, chartId, sChartName)
{
    $.ajax({
        type: "POST",
        url: "/Chart/SaveGroup",
        data: { 'companyId': companyId, 'parentId': parentId, 'chartId': chartId, 'sChartName': sChartName },
        cache: false,
        beforeSend: function () {
            $(".bgLoading").show();
        },
        success: function (result) {
            var queryResult = $.parseJSON(result);
            if (queryResult.ErrorNo == 0) {
                if (chartId == 0) {
                    addNewNodetoTree(parentId, queryResult.RecordId, sChartName + ' - ' + queryResult.OtherData, 'chart');
                    charts.push({ Id: queryResult.RecordId, Code: queryResult.OtherData, Name: sChartName });
                }
                else {
                    $.each(charts, function (index, data) {
                        if (data.Id == chartId) {
                            data.Name = sChartName
                        }
                    });
                    var editHtml = '';
                    $(".treegrid-" + chartId).find('td:first').find("span").each(function () {
                        editHtml += $(this).prop('outerHTML');
                    });
                    $(".treegrid-" + chartId).find('td:first').html(editHtml + sChartName + ' - ' + queryResult.OtherData);

                    $('.tree-' + parentId).treegrid({
                        'initialState': 'collapsed',
                        expanderExpandedClass: 'glyphicon glyphicon-minus',
                        expanderCollapsedClass: 'glyphicon glyphicon-plus'
                    });
                }
            }
            else {
                $.common.alert(alertMessage, queryResult.ErrorDesc);
            }
        },
        error: function (request, status, error) {
        },
        complete: function () {
            $(".bgLoading").hide();
        }
    });
}

function addNewNodetoTree(parentId, nodeId, nodeName,chartOrLedger)
{
    var sNodeButtonTemplate = '';
    if (chartOrLedger == "chart") {
        sNodeButtonTemplate = '<button dataId="' + nodeId + '" type="button" class="btn btn-primary btn-xs btnAdd"> <span class="glyphicon glyphicon-plus"></span> <span>' + labelAdd + '</span> </button> <button chartOrLedger="' + chartOrLedger + '" parentId="' + parentId + '" dataId="' + nodeId + '" type="button" class="btn btn-primary btn-xs btnEdit"> <span class="glyphicon glyphicon-edit"></span> <span>' + labelEdit + '</span> </button> <button dataId="' + nodeId + '" type="button" class="btn btn-primary btn-xs btnDelete"> <span class="glyphicon glyphicon-trash"></span> <span>' + labelDelete + '</span> </button>';
    }
    else {
        sNodeButtonTemplate = '<button chartOrLedger="' + chartOrLedger + '" parentId="' + parentId + '" dataId="' + nodeId + '" type="button" class="btn btn-primary btn-xs btnEdit"> <span class="glyphicon glyphicon-edit"></span> <span>' + labelEdit + '</span> </button> <button dataId="' + nodeId + '" type="button" class="btn btn-primary btn-xs btnDelete"> <span class="glyphicon glyphicon-trash"></span> <span>' + labelDelete + '</span> </button>';
    }
    var sNodeTemplate = "";
    if ($(".treegrid-" + parentId).length == 0) {
        if (chartOrLedger == "chart") {
            sNodeTemplate = '<tr class="treegrid-' + nodeId + ' treegrid-expanded"><td><span class="treegrid-expander glyphicon glyphicon-minus"></span>' + nodeName + '</td><td>' + sNodeButtonTemplate + '</td></tr>';
        }
        else {
            sNodeTemplate = '<tr class="treegrid-' + nodeId + ' treegrid-expanded" style="background-color:#cbd2d6"><td><span class="treegrid-expander glyphicon glyphicon-minus"></span>' + nodeName + '</td><td>' + sNodeButtonTemplate + '</td></tr>';
        }
    }
    else {
        if (chartOrLedger == "chart") {
            sNodeTemplate = '<tr class="treegrid-' + nodeId + ' treegrid-parent-' + parentId + ' treegrid-expanded" style="display: table-row;"><td><span class="treegrid-expander glyphicon glyphicon-minus"></span>' + nodeName + '</td><td>' + sNodeButtonTemplate + '</td></tr>';
        }
        else {
            sNodeTemplate = '<tr class="treegrid-' + nodeId + ' treegrid-parent-' + parentId + ' treegrid-expanded" style="display: table-row;background-color:#cbd2d6"><td><span class="treegrid-expander glyphicon glyphicon-minus"></span>' + nodeName + '</td><td>' + sNodeButtonTemplate + '</td></tr>';
        }
    }
    if ($(".treegrid-" + parentId).length == 0) {
        $(".tree-" + parentId + " tbody").append(sNodeTemplate);
        $('.tree-' + parentId).treegrid({
            'initialState': 'collapsed',
            expanderExpandedClass: 'glyphicon glyphicon-minus',
            expanderCollapsedClass: 'glyphicon glyphicon-plus'
        });
        //$("#btnAddFirst").hide();
    }
    else {
        if ($('.treegrid-' + parentId).treegrid('getChildNodes').length > 0) {
            $('.treegrid-' + parentId).treegrid('getChildNodes').each(function () {
                if ($(this).treegrid("isLast")) {
                    if ($(this).treegrid('getChildNodes').length > 0) {
                        $(this).treegrid('getChildNodes').each(function () {
                            if ($(this).treegrid("isLast")) {
                                $(this).after(sNodeTemplate);
                                $('.treegrid-' + parentId).treegrid("initAddedNode");
                                //$('.treegrid-' + parentId).treegrid("expand");
                            }
                        });
                    }
                    else {
                        $(this).after(sNodeTemplate);
                        $('.treegrid-' + parentId).treegrid("initAddedNode");
                        //$('.treegrid-' + parentId).treegrid("expand");
                    }
                }
                $(this).treegrid('collapse');
            });
        }
        else {
            $('.treegrid-' + parentId).after(sNodeTemplate);
            $('.treegrid-' + parentId).treegrid("initAddedNode");
            $('.treegrid-' + parentId).treegrid("expand");
        }
    }

    $(".btnAdd").unbind("click", $.chart.AddGroupOrLedger);
    $(".btnAdd").bind("click", $.chart.AddGroupOrLedger);

    $(".btnEdit").unbind("click", $.chart.EditGroupOrLedger);
    $(".btnEdit").bind("click", $.chart.EditGroupOrLedger);

    $(".btnDelete").unbind("click", $.chart.DeleteGroupOrLedger);
    $(".btnDelete").bind("click", $.chart.DeleteGroupOrLedger);
}

$('.nav-tabs a[href="#divLiabilities"]').on("click", function () {
    if ($.trim($("#divLiabilities").html()) != "") return;
    $.ajax({
        type: "POST",
        url: "/Chart/ChartTree",
        data: { 'type': '2' },
        cache: false,
        beforeSend: function () {
            $(".bgLoading").show();
        },
        success: function (result) {
            $("#divLiabilities").html(result);
        },
        error: function (request, status, error) {
        },
        complete: function () {
            $(".bgLoading").hide();
        }
    });
});

$('.nav-tabs a[href="#divEquity"]').on("click", function () {
    if ($.trim($("#divEquity").html()) != "") return;
    $.ajax({
        type: "POST",
        url: "/Chart/ChartTree",
        data: { 'type': '5' },
        cache: false,
        beforeSend: function () {
            $(".bgLoading").show();
        },
        success: function (result) {
            $("#divEquity").html(result);
        },
        error: function (request, status, error) {
        },
        complete: function () {
            $(".bgLoading").hide();
        }
    });
});

$('.nav-tabs a[href="#divIncome"]').on("click", function () {
    if ($.trim($("#divIncome").html()) != "") return;
    $.ajax({
        type: "POST",
        url: "/Chart/ChartTree",
        data: { 'type': '3' },
        cache: false,
        beforeSend: function () {
            $(".bgLoading").show();
        },
        success: function (result) {
            $("#divIncome").html(result);
        },
        error: function (request, status, error) {
        },
        complete: function () {
            $(".bgLoading").hide();
        }
    });
});

$('.nav-tabs a[href="#divExpenses"]').on("click", function () {
    if ($.trim($("#divExpenses").html()) != "") return;
    $.ajax({
        type: "POST",
        url: "/Chart/ChartTree",
        data: { 'type': '4' },
        cache: false,
        beforeSend: function () {
            $(".bgLoading").show();
        },
        success: function (result) {
            $("#divExpenses").html(result);
        },
        error: function (request, status, error) {
        },
        complete: function () {
            $(".bgLoading").hide();
        }
    });
});

$("#btnSaveLedger").on("click", function (e) {
    if ($("#cmbChartType").val() == "1") {
        if ($.trim($("#txtGroupName").val()) == "") {
            $("#errGroupName").show();
            return;
        }
        saveGroup($("#hidCompanyId").val(), $("#hidParentId").val(), $("#hidChartId").val(), $("#txtGroupName").val())
        $("#divLedgerDetails").dialog("close");
    }
    else {
        if ($.trim($("#txtLedgerName").val()) == "") {
            $("#errLedgerName").show();
            return;
        }

        var accountSide = 0;
        if ($("#chkDebit").is(":checked")) accountSide = 1;
        saveLedger($("#hidParentId").val(), $("#hidLedgerId").val(), $("#txtLedgerName").val(), $("#txtLedgerAlias").val(), 0, accountSide)
        $("#divLedgerDetails").dialog("close");
    }
});

$("#btnCancelLedger").on("click", function (e) {
    $("#divLedgerDetails").dialog("close");
});

$('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
    checkboxClass: 'icheckbox_minimal-blue',
    radioClass: 'iradio_minimal-blue'

});

function saveLedger(parentId, LedgerId, sLedgerName, sLedgerAlias,isCostCenter,accountSide) {
    $.ajax({
        type: "POST",
        url: "/Chart/SaveLedger",
        data: { 'parentId': parentId, 'LedgerId': LedgerId, 'sLedgerName': sLedgerName, 'sLedgerAlias': sLedgerAlias, 'isCostCenter': isCostCenter, 'accountSide': accountSide },
        cache: false,
        beforeSend: function () {
            $(".bgLoading").show();
        },
        success: function (result) {
            var queryResult = $.parseJSON(result);
            if (queryResult.ErrorNo == 0) {
                if (LedgerId == 0) {
                    addNewNodetoTree(parentId, queryResult.RecordId, sLedgerName + ' - ' + queryResult.OtherData, 'ledger');
                    ledgers.push({ Id: queryResult.RecordId, Code: '', Name: sLedgerName, ParentId: parentId, AliasName: sLedgerAlias });
                }
                else {
                    var editHtml = "";
                    $.each(ledgers, function (index, data) {
                        if (data.Id == LedgerId) {
                            data.Name = sLedgerName
                            data.AliasName = sLedgerAlias
                        }
                    });
                    LedgerId = parseFloat(LedgerId) + parseFloat(contantNumber);
                    $(".treegrid-" + LedgerId).find('td:first').find("span").each(function () {
                        editHtml += '<span class="treegrid-indent"></span>';
                    });
                    $(".treegrid-" + LedgerId).find('td:first').html(editHtml + sLedgerName + ' - ' + queryResult.OtherData);
                }
            }
            else {
                $.common.alert(alertMessage, queryResult.ErrorDesc);
            }
        },
        error: function (request, status, error) {
        },
        complete: function () {
            $(".bgLoading").hide();
        }
    });
}

$("#txtGroupName").on('keyup', function (e) {
    if ($.trim($(this).val()) != "") {
        $("#errGroupName").hide();
    }
    else {
        $("#errGroupName").show();
    }
});

$("#txtLedgerName").on('keyup', function (e) {
    if ($.trim($(this).val()) != "") {
        $("#errLedgerName").hide();
    }
    else {
        $("#errLedgerName").show();
    }
});

$.item = {};
var ledgers = [];
var charts = [];
$("#errGroupName").hide();
$("#errItemName").hide();
$('[data-showTooltip="yes"]').tooltip();
var itemPricings = [];
$("#divItemDetails").dialog({
    autoOpen: false,
    closeOnEscape: true,
    resizable: false,
    width: "980px",
    title: "Add Item Groups/Items",
    modal: true,
    open: function (event, ui) {
        //hide close button.
        $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
        $(this).css({ 'max-height': 450, 'overflow-y': 'auto' });
    }
});

$("#cmbItemType").on("change", function () {
    if ($(this).val() == "1") {
        $("#divItemType").show();
        $(".itemGroup").show();
        $(".itemAccount").hide();
    }
    else {
        $(".itemGroup").hide();
        $(".itemAccount").show();
    }
});

$("#btnAdd").on("click", function (e) {
    var isValid = true;
    if ($("#cmbUnits").val() == null) {
        $("#cmbUnits").next().find('.select2-selection').css({
            backgroundColor: "#eddddd"
        });
        isValid = false;
    }
    
    if ($("#txtQuantity").val() == "") {
        $("#txtQuantity").css("backgroundColor", "#eddddd");
        isValid = false;
    }
    if ($("#txtCostPrice").val() == "") {
        $("#txtCostPrice").css("backgroundColor", "#eddddd");
        isValid = false;
    }
    if ($("#txtAvgPrice").val() == "") {
        $("#txtAvgPrice").css("backgroundColor", "#eddddd");
        isValid = false;
    }
    if ($("#txtSalePrice").val() == "") {
        $("#txtSalePrice").css("backgroundColor", "#eddddd");
        isValid = false;
    }
    if ($("#txtMinSalePrice").val() == "") {
        $("#txtMinSalePrice").css("backgroundColor", "#eddddd");
        isValid = false;
    }
    if ($("#txtWholesalePrice").val() == "") {
        $("#txtWholesalePrice").css("backgroundColor", "#eddddd");
        isValid = false;
    }
    //if ($("#txtBarcode").val() == "") {
    //    $("#txtBarcode").css("backgroundColor", "#eddddd");
    //    isValid = false;
    //}
    if (!isValid) return;
    $.each(itemPricings, function (index, data) {
        if (data.UnitId == $("#cmbUnits").val()){
            isValid = false;
            return false;
        }
    });
    if (!isValid) {
        $("#errItemPricing").html("Unit already added.");
        return;
    }
    var newitemPricing = { ItemPricingId: $.common.uniqueNumber(), ItemId: 0, UnitId: $("#cmbUnits").val(), FillQuantity: $("#txtQuantity").val(), CostPrice: parseFloat($("#txtCostPrice").val()).toFixed(2), AveragePrice: $("#txtAvgPrice").val(), SalePrice: $("#txtSalePrice").val(), MinSalePrice: $("#txtMinSalePrice").val(), WholeSalePrice: $("#txtWholesalePrice").val(), BarCode: $("#txtBarcode").val() };
    itemPricings.push(newitemPricing);
    addItemPriceToTable(newitemPricing);
    $("#txtQuantity").val("");
    $("#txtCostPrice").val("");
    $("#txtAvgPrice").val("");
    $("#txtSalePrice").val("");
    $("#txtMinSalePrice").val("");
    $("#txtWholesalePrice").val("");
    $("#txtBarcode").val("");
});

function addItemPriceToTable(newItemPricing) {
    var sNodeTemplate = "";
    //main row
    sNodeTemplate = '<tr rowId="' + newItemPricing.ItemPricingId + '" ondblclick="editVoucherRow(' + newItemPricing.ItemPricingId + ')" class="treegrid-' + newItemPricing.ItemPricingId + ' treegrid-expanded"><td><span class="treegrid-expander glyphicon glyphicon-minus"></span>' + getSelectedText("cmbUnits") + '</td><td align="center">' + newItemPricing.FillQuantity + '</td><td align="right">' + parseFloat(newItemPricing.CostPrice).toFixed(2) + '</td><td align="right">' + parseFloat(newItemPricing.AveragePrice).toFixed(2) + '</td><td align="right">' + parseFloat(newItemPricing.SalePrice).toFixed(2) + '</td><td align="right">' + parseFloat(newItemPricing.MinSalePrice).toFixed(2) + '</td><td align="right">' + parseFloat(newItemPricing.WholeSalePrice).toFixed(2) + '</td><td>' + newItemPricing.BarCode + '</td><td align="right"><button dataId="' + newItemPricing.ItemPricingId + '" type="button" class="btn btn-primary btn-xs btnDelete"> <span class="glyphicon glyphicon-edit"></span> <span></span> </button>&nbsp;&nbsp;<button dataId="' + newItemPricing.ItemPricingId + '" type="button" class="btn btn-primary btn-xs btnDelete"> <span class="glyphicon glyphicon-trash"></span> <span></span> </button></td></tr>';
    $("#trTemplateItemPricing").before(sNodeTemplate);
    $('.tree-1').treegrid({
        'initialState': 'collapsed',
        expanderExpandedClass: 'glyphicon glyphicon-minus',
        expanderCollapsedClass: 'glyphicon glyphicon-plus'
    });

    $(".btnDelete").unbind("click", $.item.DeleteGroupOrItem);
    $(".btnDelete").bind("click", $.item.DeleteGroupOrItem);

    $("table.tree-1 tr").on("click", function () {
        //onRowClick($(this).attr("rowId"));
    });
}

function getSelectedText(cmbName) {
    var selectedValue = $("#" + cmbName + "").val();
    if (selectedValue == "0") return "";
    var selectedText = "";
    var dataArr = $("#" + cmbName + "").select2('data')
    $.each(dataArr, function (index, data) {
        if (data.id == selectedValue) {
            selectedText = data.text;
            return false;
        }
    });
    return selectedText;
}

$("input[type=text]").on("keyup", function () {
    if ($(this).val() != "") {
        $(this).css("backgroundColor", "#ffffff");
    }
});

$("#cmbUnits").on("change", function () {
    $(this).next().find('.select2-selection').css({
        backgroundColor: "#ffffff"
    });
});

$.item.AddGroupOrItem = function (e) {
    var obj = $(this);
    var companyChartLevel = parseFloat($("#hidCompanyChartLevel").val());
    var currentLevel = 0
    try { currentLevel = parseFloat($(".treegrid-" + obj.attr("dataId")).treegrid('getDepth')) + 1; }
    catch (e) { }
    $("#txtGroupName").val("");
    $("#txtItemName").val("");
    $("#txtItemLocation").val("");
    $('#cmbBasicUnit').val($('#cmbBasicUnit option:first-child').val()).trigger('change');
    $('#cmbUnits').val($('#cmbBasicUnit option:first-child').val()).trigger('change');
    itemPricings = [];
    $(".tree-1 tr").each(function () {
        if ($(this).attr("id") != "trTemplateItemPricing" && $(this).find("th").length<=0) {
            $(this).remove();
        }
    });
    $("#errItemPricing").html("");
    
    if (obj.attr("dataId") == "0") {
        $("#cmbItemType").val("1").trigger("change");
        $("#cmbItemType").prop("disabled", true).select2({ width: "100%", minimumResultsForSearch: Infinity});
    }
    else    {
        $("#cmbItemType").prop("disabled", false).select2({ width: "100%", minimumResultsForSearch: Infinity });
    }
    if (currentLevel < (companyChartLevel - 2) ) {
        $("#hidItemId").val("0");
        $("#hidParentId").val(obj.attr("dataId"));
        
        if (currentLevel > 1) {
            $("#cmbItemType").val("2").trigger("change");
            $("#divItemType").show();
            $(".itemGroup").hide();
            $(".itemAccount").show();
        }
        else {
            $("#cmbItemType").val("1").trigger("change");
            $("#divItemType").show();
            $(".itemGroup").show();
            $(".itemAccount").hide();
        }
        $("#divItemDetails").dialog("open");
    }
    else {
        $("#hidLedgerId").val("0");
        $("#hidParentId").val(obj.attr("dataId"));
        $("#chkDebit").attr("checked", "checked");

        $("#cmbItemType").val("2").trigger("change");
        $("#divItemType").hide();
        $(".itemGroup").hide();
        $(".itemAccount").show();
        $("#divItemDetails").dialog("open");
        //alert("Add Ledger Under Construction");
    }
}

$.item.EditGroupOrItem = function (e) {
    var obj=$(this);
    var companyChartLevel = parseFloat($("#hidCompanyChartLevel").val());
    var currentLevel = 0
    try{currentLevel=parseFloat($(".treegrid-" + obj.attr("dataId")).treegrid('getDepth')) + 1;}
    catch (e) { }
    if ($(this).attr("groupOrItem") == "group") {
        $("#hidItemId").val(obj.attr("dataId"));
        $("#hidParentId").val(obj.attr("parentId"));
        var groupId = obj.attr("dataId");
        $.ajax({
            type: "POST",
            url: "/Item/GetItemGroupById",
            data: { 'id': obj.attr("dataId") },
            cache: false,
            beforeSend: function () {
                $(".bgLoading").show();
            },
            success: function (result) {
                var itemGroup = $.parseJSON(result);
                if (itemGroup.length > 0) {
                    $("#txtGroupName").val(itemGroup[0].ItemGroupName);
                    $("#divItemDetails").dialog("open");
                }
            },
            error: function (request, status, error) {
            },
            complete: function () {
                $(".bgLoading").hide();
            }
        });
        $("#cmbItemType").val("1").trigger("change");
        $("#divItemType").hide();
        $(".itemGroup").show();
        $(".itemAccount").hide();
        
    }
    else {
        $("#hidParentId").val(obj.attr("parentId"));
        var itemId = parseFloat(obj.attr("dataId")) - parseFloat(contantNumber);
        $("#hidItemId").val(itemId);
        $(".tree-1 tr").each(function () {
            if ($(this).attr("id") != "trTemplateItemPricing" && $(this).find("th").length <= 0) {
                $(this).remove();
            }
        });
        itemPricings = [];
        $.ajax({
            type: "POST",
            url: "/Item/GetItemById",
            data: { 'id': obj.attr("dataId") },
            cache: false,
            beforeSend: function () {
                $(".bgLoading").show();
            },
            success: function (result) {
                var item = $.parseJSON(result);
                if (item.length > 0) {
                    $("#txtItemName").val(item[0].ItemName);
                    $("#cmbBasicUnit").val(item[0].BasicUnit).trigger("change");
                    $("#txtItemLocation").val(item[0].ItemLocation);
                    $.each(item[0].ItemPricing, function (index, data) {
                        $("#cmbUnits").val(data.UnitId).trigger("change");
                        itemPricings.push(data);
                        addItemPriceToTable(data);
                    });
                    $("#divItemDetails").dialog("open");
                }
            },
            error: function (request, status, error) {
            },
            complete: function () {
                $(".bgLoading").hide();
            }
        });
        $("#cmbItemType").val("2").trigger("change");
        $("#divItemType").hide();
        $(".itemGroup").hide();
        $(".itemAccount").show();
    }
}

$.item.DeleteGroupOrItem = function (e) {
    var obj = $(this);
    var obj = $(this);
    var companyChartLevel = parseFloat($("#hidCompanyChartLevel").val());
    if ($(this).attr("groupOrItem") == "group") {
        $.ajax({
            type: "POST",
            url: "/Item/DeleteGroup",
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
            url: "/Item/DeleteItem",
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

function saveItemGroup(parentId, itemId, sItemGroupName)
{
    $.ajax({
        type: "POST",
        url: "/Item/SaveItemGroup",
        data: { 'companyId': $("#hidCompanyId").val(), 'branchId': $("#hidBranchId").val(), 'parentId': parentId, 'itemId': itemId, 'itemGroupName': sItemGroupName },
        cache: false,
        beforeSend: function () {
            $(".bgLoading").show();
        },
        success: function (result) {
            var queryResult = $.parseJSON(result);
            if (queryResult.ErrorNo == 0) {
                if (itemId == 0) {
                    addNewNodetoTree(parentId, queryResult.RecordId, sItemGroupName + ' - ' + queryResult.OtherData, 'group');
                    //charts.push({ Id: queryResult.RecordId, Code: queryResult.OtherData, Name: sChartName });
                }
                else {
                    //$.each(charts, function (index, data) {
                    //    if (data.Id == chartId) {
                    //        data.Name = sChartName
                    //    }
                    //});
                    var editHtml = '';
                    $(".treegrid-" + itemId).find('td:first').find("span").each(function () {
                        editHtml += $(this).prop('outerHTML');
                    });
                    $(".treegrid-" + itemId).find('td:first').html(editHtml + sItemGroupName + ' - ' + queryResult.OtherData);

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
            $("#divItemDetails").dialog("close");
        },
        error: function (request, status, error) {
        },
        complete: function () {
            $(".bgLoading").hide();
        }
    });
}

function addNewNodetoTree(parentId, nodeId, nodeName,groupOrItem)
{
    var sNodeButtonTemplate = '';
    if (groupOrItem == "group") {
        sNodeButtonTemplate = '<button dataId="' + nodeId + '" type="button" class="btn btn-primary btn-xs btnAdd"> <span class="glyphicon glyphicon-plus"></span> <span>' + labelAdd + '</span> </button> <button groupOrItem="' + groupOrItem + '" parentId="' + parentId + '" dataId="' + nodeId + '" type="button" class="btn btn-primary btn-xs btnEdit"> <span class="glyphicon glyphicon-edit"></span> <span>' + labelEdit + '</span> </button> <button dataId="' + nodeId + '" groupOrItem="' + groupOrItem + '" type="button" class="btn btn-primary btn-xs btnDelete"> <span class="glyphicon glyphicon-trash"></span> <span>' + labelDelete + '</span> </button>';
    }
    else {
        sNodeButtonTemplate = '<button groupOrItem="' + groupOrItem + '" parentId="' + parentId + '" dataId="' + nodeId + '" type="button" class="btn btn-primary btn-xs btnEdit"> <span class="glyphicon glyphicon-edit"></span> <span>' + labelEdit + '</span> </button> <button dataId="' + nodeId + '" groupOrItem="' + groupOrItem + '" type="button" class="btn btn-primary btn-xs btnDelete"> <span class="glyphicon glyphicon-trash"></span> <span>' + labelDelete + '</span> </button>';
    }
    var sNodeTemplate = "";
    if ($(".treegrid-" + parentId).length == 0) {
        if (groupOrItem == "group") {
            sNodeTemplate = '<tr class="treegrid-' + nodeId + ' treegrid-expanded"><td><span class="treegrid-expander glyphicon glyphicon-minus"></span>' + nodeName + '</td><td>' + sNodeButtonTemplate + '</td></tr>';
        }
        else {
            sNodeTemplate = '<tr class="treegrid-' + nodeId + ' treegrid-expanded" style="background-color:#cbd2d6"><td><span class="treegrid-expander glyphicon glyphicon-minus"></span>' + nodeName + '</td><td>' + sNodeButtonTemplate + '</td></tr>';
        }
    }
    else {
        if (groupOrItem == "group") {
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

    $(".btnAdd").unbind("click", $.item.AddGroupOrItem);
    $(".btnAdd").bind("click", $.item.AddGroupOrItem);

    $(".btnEdit").unbind("click", $.item.EditGroupOrItem);
    $(".btnEdit").bind("click", $.item.EditGroupOrItem);

    $(".btnDelete").unbind("click", $.item.DeleteGroupOrItem);
    $(".btnDelete").bind("click", $.item.DeleteGroupOrItem);
}

$("#btnSaveItem").on("click", function (e) {
    if ($("#cmbItemType").val() == "1") {
        if ($.trim($("#txtGroupName").val()) == "") {
            $("#errGroupName").show();
            return;
        }
        saveItemGroup($("#hidParentId").val(), $("#hidItemId").val(), $("#txtGroupName").val())
        $("#divItemDetails").dialog("close");
    }
    else {
        if ($.trim($("#txtItemName").val()) == "") {
            $("#errItemName").show();
            return;
        }
        if (itemPricings.length<=0) {
            $("#errItemPricing").html("Please add atleast one Item Pricing");
            return;
        }
        $("#errItemPricing").html("");
        var accountSide = 0;
        if ($("#chkDebit").is(":checked")) accountSide = 1;
        saveItem()
        $("#divItemDetails").dialog("close");
    }
});

$("#btnCancelItem").on("click", function (e) {
    $("#divItemDetails").dialog("close");
});

$('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
    checkboxClass: 'icheckbox_minimal-blue',
    radioClass: 'iradio_minimal-blue'

});

function saveItem() {
    
    $.ajax({
        type: "POST",
        url: "/Item/SaveItem",
        data: { 'companyId': $("#hidCompanyId").val(), 'branchId': $("#hidBranchId").val(), 'parentId': $("#hidParentId").val(), 'itemId': $("#hidItemId").val(), 'itemName': $("#txtItemName").val(), 'basicUnit': $("#cmbBasicUnit").val(), 'location': $("#txtItemLocation").val(),'itemPricings': JSON.stringify(itemPricings) },
        cache: false,
        beforeSend: function () {
            $(".bgLoading").show();
        },
        success: function (result) {
            var queryResult = $.parseJSON(result);
            if (queryResult.ErrorNo == 0) {
                if ($("#hidItemId").val() == 0) {
                    addNewNodetoTree($("#hidParentId").val(), queryResult.RecordId, $("#txtItemName").val() + ' - ' + queryResult.OtherData, 'item');
                }
                else {
                    var itemId = $("#hidItemId").val();
                    itemId = parseFloat(itemId) + parseFloat(contantNumber);
                    var editHtml = '';
                    $(".treegrid-" + itemId).find('td:first').find("span").each(function () {
                        editHtml += '<span class="treegrid-indent"></span>';
                    });
                    $(".treegrid-" + itemId).find('td:first').html(editHtml + $("#txtItemName").val() + ' - ' + queryResult.OtherData);
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
            $("#divItemDetails").dialog("close");
        }
    });
}

$("#txtCostPrice, #txtAvgPrice, #txtSalePrice, #txtMinSalePrice, #txtWholesalePrice").on('keypress', function (e) {
    return $.common.inputDecimals($(this), e);
});

$("#txtCostPrice, #txtAvgPrice, #txtSalePrice, #txtMinSalePrice, #txtWholesalePrice").on('blur', function (e) {
    if ($(this).val() == ".") $(this).val("0");
});

$("#txtCostPrice, #txtAvgPrice, #txtSalePrice, #txtMinSalePrice, #txtWholesalePrice").on('paste', function (e) {
    e.preventDefault();
});

$("#txtCostPrice, #txtAvgPrice, #txtSalePrice, #txtMinSalePrice, #txtWholesalePrice").on('focus', function (e) {
    $(this).select();
});

$("#txtQuantity").on('keypress', function (e) {
    return $.common.inputNumbers(e);
});

$("#txtQuantity").on('blur', function (e) {
    if ($(this).val() == ".") $(this).val("0");
});

$("#txtQuantity").on('paste', function (e) {
    e.preventDefault();
});

$("#txtQuantity").on('focus', function (e) {
    $(this).select();
});
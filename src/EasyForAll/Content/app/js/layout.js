$(document).ready(function () {
    //Adjust the height of side Menu and content wrapper
    $(".sidebar-menu").height($(window).height() - $(".user-panel").outerHeight() - $(".sidebar-form").outerHeight() - parseFloat($(".main-sidebar").css("padding-top")));
    if ($(window).width() > 768) {
        $(".content-wrapper").height($(window).height() - parseFloat($(".navbar-custom-menu").outerHeight()) - parseFloat($(".content-subheader").outerHeight()) - parseFloat($(".main-footer").outerHeight()) - parseFloat($(".content-wrapper").css("padding-top")) - parseFloat($(".content-wrapper").css("padding-top")));
    }
    else {
        $(".content-wrapper").height($(".content-wrapper") - parseFloat($(".navbar-custom-menu").outerHeight()) - parseFloat($(".content-subheader").outerHeight()) - parseFloat($(".main-footer").outerHeight()) - 40);
    }

    //loading panel adjustment
    $(".bgLoading").height($(window).innerHeight());
    $(".bgLoading").css("padding-top", ($(window).innerHeight() / 2) - 100);

    //display the content after the height Adjustment;
    $(".content-wrapper").show();
    $(window).resize(function () {
        //loading panel adjustment
        $(".bgLoading").height($("body").innerHeight());
        $(".bgLoading").css("padding-top", ($("body").innerHeight() / 2) - 100);

        //Adjust the height of side Menu and content wrapper
        $(".sidebar-menu").height($(window).height() - $(".user-panel").outerHeight() - $(".sidebar-form").outerHeight() - parseFloat($(".main-sidebar").css("padding-top")));
        if ($(window).width() > 768) {
            $(".content-wrapper").height($(window).height() - parseFloat($(".navbar-custom-menu").outerHeight()) - parseFloat($(".content-subheader").outerHeight()) - parseFloat($(".main-footer").outerHeight()) - parseFloat($(".content-wrapper").css("padding-top")) - parseFloat($(".content-wrapper").css("padding-top")));
        }
        else {
            $(".content-wrapper").height($(".content-wrapper") - parseFloat($(".navbar-custom-menu").outerHeight()) - parseFloat($(".content-subheader").outerHeight()) - parseFloat($(".main-footer").outerHeight()) - 40);
        }
    });

    //side menu selection
    $(".sidebar-menu > li").on("click", function () {
        if (!$(this).hasClass("treeview")) {
            $(".sidebar-menu > li").removeClass("active");
            $(this).addClass("active");
            $(".treeview-menu > li").css("backgroundColor", "#0e1012");
            $(".treeviewSub-menu > li").css("backgroundColor", "#202a2e");
        }
    });

    $(".treeview-menu > li").on("click", function () {
        $(".treeview-menu > li").css("backgroundColor", "#0e1012");
        $(this).css("backgroundColor", "#37474f");
    });

    $(".treeviewSub-menu > li").on("click", function () {
        $(".treeviewSub-menu > li").css("backgroundColor", "#202a2e");
        $(this).css("backgroundColor", "#6d8087");
    });
});


//Ajax.BeginForm Events
function OnBegin() {
    $(".bgLoading").show();
    
    //if ($('[controlType="date"]').length > 0) $('[controlType="date"]').remove();
    if ($("#divChartingLevel").length > 0) $("#divChartingLevel").remove();
    if ($("#divGroupDetails").length > 0) $("#divGroupDetails").remove();
    if ($("#divLedgerDetails").length > 0) $("#divLedgerDetails").remove();
    if ($("#divBranchDetails").length > 0) $("#divBranchDetails").remove();
    if ($("#divItemDetails").length > 0) $("#divItemDetails").remove();
}
function OnComplete(result) {
    setTimeout('$(".bgLoading").hide();', 10);
    //if ($(".form-horizontal").length > 0) {
    //    $(".form-horizontal").height($(".form-horizontal").parent().height());
    //}
}
function OnSuccessMenu() {
    $(".tab-pane").css("min-height", $(".content-wrapper").height() - parseFloat($(".nav-tabs").outerHeight())- 22);
}
function OnFailure() {
}



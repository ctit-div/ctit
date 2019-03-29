// tripG base function
// dependency: jquery.1.5.1
(function ($) {

    $.common = $.common || {};

    $.common.lang = "en";

    $.common.getKeyNum = function (e) {
        var keynum;
        if (window.event)
            keynum = e.keyCode;

        else if (e.which)
            keynum = e.which;

        return keynum;
    };

    $.common.inputNumbers = function (e) {
        var keynum = $.common.getKeyNum(e);
        if (keynum) {
            if (keynum == 8) return true;
            if (keynum == 13) {
                $(this).change().blur();
                return false;
            }
            else {
                var keychar = String.fromCharCode(keynum);
                var numcheck = /\d/;
                return numcheck.test(keychar);
            }
        }
        return true;
    };

    $.common.inputDecimals = function (txtDec,e) {
        var keynum = $.common.getKeyNum(e);
        var txtId = txtDec.attr("id");
        if (keynum) {
            if (txtDec.val().indexOf('.') !== -1) {
                if (txtDec.val().split('.')[1].length == 2 && $.common.ShowSelection(txtId) != txtDec.val()) return false;
            }
            if (keynum == 8) return true;
            if (keynum == 13) return false;
            else {
                var newVal = txtDec.val();
                var keychar = String.fromCharCode(keynum);
                var numcheck = /\d/;
                if (keychar == '.' && $.common.ShowSelection(txtId) != "") return false;
                return (numcheck.test(keychar) || (keychar == '.' && newVal.indexOf('.') < 0 && newVal != ""));
            }
        }
        return true;
    };

    $.common.ShowSelection = function (txtId) {
        var textComponent = document.getElementById(txtId);
        var selectedText;
        // IE version
        if (document.selection != undefined) {
            textComponent.focus();
            var sel = document.selection.createRange();
            selectedText = sel.text;
        }
            // Mozilla version
        else if (textComponent.selectionStart != undefined) {
            var startPos = textComponent.selectionStart;
            var endPos = textComponent.selectionEnd;
            selectedText = textComponent.value.substring(startPos, endPos)
        }
        return selectedText;
    }

    $.common.alert = function (txt,message, ok) {
        $('#alert-text').html(txt);
        $('#alert-message').html(message);
        $('.bgShadow').show();
        $('#alert-box').center().show();
        $('#alert-btn-ok').focus();
        $('#alert-btn-ok').unbind('click').click(function () {
            $('.bgShadow').hide();
            $('#alert-box').hide();
            if (ok) ok();
            return false;
        });
        return false;
    };

    $.common.confirm = function (txt,message, ok, cancel, txtOk, txtCancel) {
        var txtOk = txtOk || 'OK';
        var txtCancel = txtCancel || 'Cancel';
        $('#confirm-text').html(txt);
        $('#confirm-message').html(message);
        $('.bgShadow').show();
        $('#confirm-box').center().show();
        $('#confirm-btn-ok').focus();
        $('#confirm-btn-ok').html(txtOk).unbind('click').click(function () {
            $('.bgShadow').hide();
            $('#confirm-box').hide();
            if (ok) ok();
            return false;
        });
        $('#confirm-btn-cancel').html(txtCancel).unbind('click').click(function () {
            $('.bgShadow').hide();
            $('#confirm-box').hide();
            if (cancel) cancel();
            return false;
        });
    };

    $.fn.center = function () {
        $(this).each(function () {
            var div = $(this);
            div.css("position", "absolute")
               .css("top", ($(window).height() - div.height()) / 2 + $(window).scrollTop() + "px")
               .css("left", ($(window).width() - div.width()) / 2 + $(window).scrollLeft() + "px");
        });
        return this;
    };

    $.common.selectTab = function (tab) {
        $('.nav-tabs a[href="#' + tab + '"]').tab('show');
    };

    $.common.uniqueNumber = function () {
        return (Math.floor(Math.random() * 60000) + 1);
    };

})($);
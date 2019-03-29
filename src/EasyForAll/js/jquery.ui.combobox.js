/*
 * Developer:	Eric Faust
 * Date:		November 25, 2011
 * Description:	Used by the Editable Drop Down List control
 */
// Start jquery combobox
(function ($) {
    $.widget("ui.combobox", {
        _create: function () {
            var self = this,
					select = this.element.hide(),
					selected = select.children(":selected"),
					value = select.val() ? select.val() : selected.val() ? selected.text() : "";
            var input = this.input = $("<input>")
					.insertAfter(select)
					.val(value)
					.autocomplete({
					    delay: 0,
					    minLength: 0,
					    autoFocus: this.options.autoselectFirstItem,
					    source: this.options.source,
					    change: function (event, ui) {
					        select.val($(this).val());
					    },
					    select: function (event, ui) {
					        if (ui.item) {
					            select.val(ui.item.label);
					            if (input.autopostback)
					                __doPostBack(select.context.id, "");
					        }
					    }
					})
                    .copyCSS(select);
            input.autopostback = this.options.autopostback;
            input.attr('name', this.options.name);
            input.attr('id', this.options.name);
            if (this.element[0].tabIndex > 0) input.attr('tabIndex', this.element[0].tabIndex);
            input.show();
            input.copyAttributes(select);
            input.data("autocomplete")._renderItem = function (ul, item) {
                return $("<li></li>")
						.data("item.autocomplete", item)
						.append("<a>" + item.label + "</a>")
						.appendTo(ul)
                        .css( "font-family" , select.css( "font-family" ))
                        .css( "white-space" , "pre-wrap" );
            };

            var button = this.button = $("<button type='button'>&nbsp;</button>")
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

					    // work around a bug (likely same cause as #5265)
					    $(this).blur();

					    // pass empty string as value to search for, displaying all results
					    input.autocomplete("search", "");
					    input.focus();
					});
            button.attr('name', this.options.name + '_button');
            button.attr('id', this.options.name + '_button');
        },

        destroy: function () {
            this.input.remove();
            this.button.remove();
            this.element.show();
            $.Widget.prototype.destroy.call(this);
        }
    });
})(jQuery);
// end jquery combobox

// Get the styles of the given element -- http://plugins.jquery.com/project/copyCSS
(function ($) {
    $.fn.copyCSS = function (source) {
        var dom = $(source).get(0);
        var style;
        var dest = {};
        if (window.getComputedStyle) {
            var camelize = function (a, b) {
                return b.toUpperCase();
            };
            style = window.getComputedStyle(dom, null);
            for (var i = 0, l = style.length; i < l; i++) {
                var prop = style[i];
                var camel = prop.replace(/\-([a-z])/g, camelize);
                var val = style.getPropertyValue(prop);
                dest[camel] = val;
            };
            return this.css(dest);
        };
        if (style = dom.currentStyle) {
            for (var prop in style) {
                dest[prop] = style[prop];
            };
            return this.css(dest);
        };
        if (style = dom.style) {
            for (var prop in style) {
                if (typeof style[prop] != 'function') {
                    dest[prop] = style[prop];
                };
            };
        };
        return this.css(dest);
    };
})(jQuery);
// end jquery copyCSS

// Copy any events
(function ($) {
    $.fn.copyAttributes = function (source) {

        var source = $(source).get(0);
        var destination = $(this).get(0);
        for (var i=0; i < source.attributes.length; i++) {
            var attr = source.attributes[i];
            var attrName = attr.name.toLowerCase ();

            if (attrName.substring(0, 2).toLowerCase() == "on") {
                destination.setAttribute (attr.name, attr.value);
            }
        }
        return $(this);
    };
})(jQuery);
// end jquery copy any events
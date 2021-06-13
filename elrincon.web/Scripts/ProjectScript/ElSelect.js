/**
 * constructor
 */
function ElSelect(id) {
    this.htmlId = id;
    this.events = [];
    this.wrapper = $("#" + this.htmlId);
    this.dropContainer = $("#" + this.htmlId + " .drop-container");
    this.buttonSelector = "#" + this.htmlId + "_btn";
    this.listSelector = "#" + this.htmlId + "_list";
    this.listItemsSelector = "#" + this.htmlId + " ul li";
    this.searchInputSelector = "#" + this.htmlId + "_srch";
    this.lineHeight = 0;

    this.updateList = function (listItems) {
        $(this.listSelector).html("");
        let that = this;
        let html = "";
        if (Array.isArray) {
            for (let i = 0; i < listItems.length; i++) {
                let lineHeightVal = "";
                if (this.lineHeight !== 0) {
                    lineHeightVal = ' style="height: ' + this.lineHeight + "px; line-height: " + this.lineHeight + 'px"';
                }

                const li = listItems[i];
                html += '<li title="' + li.value + '" data-code="' + li.code;
                html += '" data-value="' + li.value + '">';
                html += '<a' + lineHeightVal + '>' + li.value + '</a>';
                html += '</li>';
            }
        }
        $(this.listSelector).html(html);
        $(this.listSelector).scrollTop(0);

        $("#" + this.htmlId + " ul li").off("click").on("click", function () {
            that.setValueItem($(this));
            //let c = $(this).data().code;
            //let v = $(this).data().value;

            //$(that.buttonSelector).html(v);
            //$(that.buttonSelector).attr("data-code", c);
            //$(that.buttonSelector).attr("data-code", c);

            //that.fire("change", {
            //    code: c,
            //    value: v
            //});
            //that.closeDropDownMenu();
        });

        //<li title="@cv.Value" data-code="@cv.Code" data-value="@cv.Value">
        //    <a>@cv.Value</a>
        //</li>
    }

    this.setValue = function (code) {
        list = $("#" + this.htmlId + " ul");
        let item = list.find('[data-code="' + code + '"]');
        if (item.length > 0) {
            this.setValueItem(item);
        }
    }

    this.setValueItem = function (jqueryItem) {
        let c = jqueryItem.data().code;
        let v = jqueryItem.data().value;

        $(this.buttonSelector).html(v);
        $(this.buttonSelector).attr("data-code", c);
        $(this.buttonSelector).data("code", c);
        this.fire("change", {
            code: c,
            value: v
        });
        this.closeDropDownMenu();
    }

    this.getValue = function () {
        return $(this.buttonSelector).data().code;
    }

    this.closeDropDownMenu = function () {
        this.dropContainer.slideUp(50);
            //console.log("idss: " + this.listSelector);
        $(this.listSelector).hide();
        //console.log("idss: " + this.listSelector);
    }
    this.toggleDropDownMenu = function () {
        if (this.dropContainer.is(":visible")) {
            this.dropContainer.slideUp(50);
            //console.log("idss: " + this.listSelector);
            $(this.listSelector).hide();

            this.fire("close");
        } else {
            this.dropContainer.slideDown(50);
            //console.log("idss: " + this.listSelector);
            $(this.listSelector).show();
            //console.log("idss: " + this.listSelector);
            this.fire("open");
        }
    }
    this.destroy = function () {
        $(this.buttonSelector).off("click");
        $(this.listItemsSelector).off("click");
        $(document).off('mouseup.' + this.wrapper);
    }

    this.events = [];
    this.fire = function (eventName, params) {
        for (var i = 0; i < this.events.length; i++) {
            let e = this.events[i];
            if (e.name === eventName && isFunction(e.func)) {
                e.func(params);
            }
        }
    }
    this.search = function () {
        let input = document.getElementById(this.htmlId + "_srch");
        let filter = input.value.toLowerCase();
        let div = document.getElementById(this.htmlId);
        let a = div.getElementsByTagName("a");
        for (let i = 0; i < a.length; i++) {
            let txtValue = a[i].textContent || a[i].innerText;
            if (txtValue.toLowerCase().indexOf(filter) > -1) {
                a[i].style.display = "";
            } else {
                a[i].style.display = "none";
            }
        }
        this.fire("search");
    }

    this.on = function (a, b) {
        if (typeof a !== "string") {
            throw ('first parameter must be a string', a);
        }
        if (!isFunction(b)) {
            throw ('second parameter must be a function', b);
        }

        this.events.push(
            {
                name: a,
                func: b
            });
    }

    this.init = function () {
        let that = this;
        $(this.buttonSelector).on("click", function (e) {
            that.toggleDropDownMenu();
        });

        $(document).on('mouseup.' + that.wrapper, function (e) {
            let elem = $("#" + that.htmlId);
            if (!that.wrapper.is(e.target) && that.wrapper.has(e.target).length === 0 && $("#" + that.htmlId + " .drop-container").is(':visible')) {
                that.closeDropDownMenu();
                that.fire("close");
            }
        });

        //id="@(idS)_srch_clr
        $("#" + this.htmlId + "_srch_clr").on("click", function () {
            $("#" + that.htmlId + "_srch").val("");
            that.search();
        });

        $("#" + this.htmlId + "_srch").on("keyup", function () {
            that.search();
        });

        $("#" + this.htmlId + " ul li").on("click", function () {
            that.setValueItem($(this));
        });

        this.on("open", function () {
            let left = 1;//that.dropContainer.offset().left;
            let w = that.dropContainer.width();
            let docW = that.dropContainer.closest("body").width();
            if (left + w > docW) {
                let l = docW - (left + w);
                //that.dropContainer.css("left", l);
            }
        });

        this.on("close", function () {
            that.dropContainer.css("left", 0);
        });

        this.on("search", function () {
            that.dropContainer.css("left", 0);
            let left = that.dropContainer.offset().left;
            let w = that.dropContainer.width();
            let docW = that.dropContainer.closest("body").width();
            if (left + w > docW) {
                let l = docW - (left + w);
                //that.dropContainer.css("left", l);
            }
        });
    }
    this.init();
}

function isFunction(functionToCheck) {
    return functionToCheck && {}.toString.call(functionToCheck) === '[object Function]';
}
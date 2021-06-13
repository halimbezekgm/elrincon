

//listede sol sorgu pull iconuna tıklayınca
$(".navview-pane .pull-button").on("click", function () {
    if ($(".navview-pane").css("width") == '48px') {
        $(".navview-pane").attr('style', 'width: 450px !important');
    } else {
        $(".navview-pane").attr('style', 'width: 48px !important');
    }

    $(".query-inputs").toggle();
    $("#pull-icon-label").toggle();
    $(".query-button").toggle();
});

function ShowAlertMessage(title, message, type) {
    Metro.infobox.create("<h3>" + title + "</h3><p>" + message + "</p>", type);
}


$(document).on('change', '.outoNewRowOnChange .tr:last select', function () {
    console.log("change.");
    var searchselect = "";
    $.ajax({
        type: 'GET',
        url: '/personeleditor/GetYuzdelikOranMuhasebeKodu',
        success: function (result) {
            console.log(parenttr.getElementsByClassName("search-selectparent").hmtl(""));
            searchselect = result;
        }
    });

    var parentsearch = $(this).parent().parent().parent().find("search-selectparent").html();
    var parenttr = $(this).parent().parent().parent().html();

    $(".outoNewRowOnChange").append("<div class='tr'>" + parenttr + "</div>");
});  

$(document).on('change', '.outoNewRowOnChange_2 .tr:last select', function () {
    console.log("change.");
    var parenttr = $(this).parent().parent().parent().html();
    $(".outoNewRowOnChange_2").append("<div class='tr'>" + parenttr + "</div>");
});
$(document).on('click','.delete_yuzdelik_orani', function() {
        deleteYuzdelik(this);
    });

function deleteYuzdelik(tht) {

    if ($(tht).parent().parent().is(":last-child")) {
        ShowAlertMessage("Hata", "Boş satır silinemez! ", "alert");

        return;
    }

    Metro.dialog.create({
        title: "Yüzdelik değeri Sil",
        content: "<div>Kaydı silmek istediğinizden emin isiniz..</div>",
        actions: [
            {
                caption: "Evet",
                cls: "js-dialog-close alert",
                onclick: function () {
                    $(tht).parent().parent().remove();
                }
            },
            {
                caption: "Hayır",
                cls: "js-dialog-close",
                onclick: function () {
                }
            }
        ]
    });
}

function doubleIntegerLimit(input, event) {
    var sign = $(input).data().sign;
    var length = $(input).data().length;
    var isItDouble = $(input).data().isitdouble;
    var ch = String.fromCharCode(event.which);
    var regStr = "[0-9";
    if (isItDouble) {
        regStr += ",";
        if (ch === ".") {
            event.preventDefault();
            if (input.value.includes(",")) event.preventDefault();
            else input.value = input.value + ",";
        }

        if (input.value.includes(",") && (ch === ",")) event.preventDefault();
    }
    if (sign === "True") {
        regStr += "-";
        if (input.value.includes("-") && (ch === "-")) event.preventDefault();
    }
    regStr += "]";
    var regEx = new RegExp(regStr);
    if (!(regEx.test(ch))) {
        event.preventDefault();
    }

    var selectedText = input.value.substr(input.selectionStart, input.selectionEnd - input.selectionStart);
    var selectedTextLength = selectedText.length;

    var inputlength = input.value.length;
    if (input.value.includes("-")) inputlength--;
    if ((inputlength >= length) && (selectedTextLength === 0)) {
        event.preventDefault();
    }
}



function phoneNumberLimit(input, event) {
    //var inputlength = input.value.length;
    //if (inputlength >= 15) {
    //    var oldValue = input.value;
    //    input.value = oldValue.substr(0, oldValue.length - 1);
    //}
}

function phone_formatter(opts) {
    let _this = {}



    _this.inputId = opts.inputId;
    _this.isValid = function () {

    };
    _this.input = document.getElementById(opts.inputId);
    if (!_this.input) {
        console.error("Phone formatter | Input element wrong", _this);
        return;
    }

    _this.inputFormat = function (e) {
        let val = $(this).val();
        val = _this.phoneFormat(val);
        $(this).val(val);
    }


    _this.isNumericInput = function (event) {
        let key = event.keyCode;
        return ((key >= 48 && key <= 57) || // Allow number line
            (key >= 96 && key <= 105) // Allow number pad
        );
    };

    _this.isModifierKey = function (event) {
        let key = event.keyCode;
        return (event.shiftKey === true || key === 35 || key === 36) || // Allow Shift, Home, End
            (key === 9 || key === 13 || key === 46) || // Allow Tab, Enter, Delete
            (key > 36 && key < 41) || // Allow left, up, right, down
            (
                // Allow Ctrl/Command + A,C,V,X,Z
                (event.ctrlKey === true || event.metaKey === true) &&
                (key === 65 || key === 67 || key === 86 || key === 88 || key === 90)
            )
    };

    //var t = text.value.substr(text.selectionStart, text.selectionEnd - text.selectionStart);


    _this.isShortEnough = function (event) {
        return event.target.value.length < 14;
    };

    _this.isBackspace = function (event) {
        return (event.keyCode === 8);
    };

    _this.hasSelection = function (event) {
        let text = event.target.value;
        let t = text.substr(text.selectionStart, text.selectionEnd - text.selectionStart);
        t = t.replace(/\D/g, '');
        if (t.length > 0)
            return true;
        else
            return false;
    }

    _this.enforceFormat = function (event) {
        // Input must be of a valid number format or a modifier key, and not longer than ten digits
        if (_this.isShortEnough(event)) {
            if (!_this.isNumericInput(event) && !_this.isModifierKey(event) && !_this.isBackspace(event) && !_this.hasSelection(event)) {
                event.preventDefault();
            }
        }
        else if (!_this.isModifierKey(event) && !_this.isBackspace(event) && !_this.hasSelection(event)) {
            event.preventDefault();
        }



    };


    _this.formatToPhone = function (event) {
        if (_this.isModifierKey(event)) { return; }

        // I am lazy and don't like to type things more than once
        const target = event.target;
        const input = target.value.replace(/\D/g, '').substring(0, 10); // First ten digits of input only
        const zip = input.substring(0, 3);
        const middle = input.substring(3, 6);
        const last = input.substring(6, 10);

        if (input.length > 6) { target.value = `(${zip}) ${middle}-${last}`; }
        else if (input.length > 3) { target.value = `(${zip}) ${middle}`; }
        else if (input.length > 0) { target.value = `(${zip}`; }
    };

    _this.input.addEventListener('keydown', _this.enforceFormat);
    _this.input.addEventListener('keyup', _this.formatToPhone);
    let event = { target: _this.input }
    _this.formatToPhone(event);

    return _this;
}

$(document).ready(function () {
    //var input = $('.phoneNumber');
    //input.mobilePhoneNumber({ allowPhoneWithoutPrefix: '+1' });
    $(".phoneNumber").each(function (index) {
        //$(this).mobilePhoneNumber({ allowPhoneWithoutPrefix: '+1' });
        var pn = phone_formatter({
            inputId: $(this).attr("id")
        });
    }); 
});

$("input[data-type='currency']").on({
    keyup: function () {
        formatCurrency($(this));
    },
    blur: function () {
        formatCurrency($(this), "blur");
    }
});


function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}


function formatCurrency(input, blur) {
    // appends $ to value, validates decimal side
    // and puts cursor back in right position.

    // get input value
    var input_val = input.val();

    // don't validate empty input
    if (input_val === "") { return; }

    // original length
    var original_len = input_val.length;

    // initial caret position 
    var caret_pos = input.prop("selectionStart");

    // check for decimal
    if (input_val.indexOf(",") >= 0) {

        // get position of first decimal
        // this prevents multiple decimals from
        // being entered
        var decimal_pos = input_val.indexOf(",");

        // split number by decimal point
        var left_side = input_val.substring(0, decimal_pos);
        var right_side = input_val.substring(decimal_pos);

        // add commas to left side of number
        left_side = formatNumber(left_side);

        // validate right side
        right_side = formatNumber(right_side);

        // On blur make sure 2 numbers after decimal
        if (blur === "blur") {
            right_side += "00";
        }

        // Limit decimal to only 2 digits
        right_side = right_side.substring(0, 2);

        // join number by .
        input_val = left_side + "," + right_side;

    } else {
        // no decimal entered
        // add commas to number
        // remove all non-digits
        input_val = formatNumber(input_val);
        //input_val = "$" + input_val;

        // final formatting
        if (blur === "blur") {
            input_val += ",00";
        }
    }

    // send updated string to input
    input.val(input_val);

    // put caret back in the right position
    var updated_len = input_val.length;
    caret_pos = updated_len - original_len + caret_pos;
    input[0].setSelectionRange(caret_pos, caret_pos);
}



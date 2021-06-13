
var maxWindowZIndex = 0;
var draggedWindow;

var Desktop = {
    options: {
        windowArea: ".window-area",
        windowAreaClass: "",
        taskBar: ".task-bar > .tasks",
        taskBarClass: ""
    },

    wins: {},

    setup: function (options) {
        this.options = $.extend({}, this.options, options);
        return this;
    },

    addToTaskBar: function (wnd) {
        var icon = wnd.getIcon();
        var wID = wnd.win.attr("id");
        var item = $("<span>").addClass("task-bar-item started").html(icon);

        item.data("wID", wID);

        item.appendTo($(this.options.taskBar));
    },

    removeFromTaskBar: function (wnd) {
        var wID = wnd.attr("id");
        var items = $(".task-bar-item");
        var that = this;
        $.each(items, function () {
            var item = $(this);
            if (item.data("wID") === wID) {
                delete that.wins[wID];
                item.remove();
            }
        })
    },

    createWindow: function (o) {
        //var that = this;
        o.onDragStart = function (pos, el) {
            win = $(this);
            draggedWindow = el;
            $(".window").css("z-index", 1);

            if (!win.hasClass("modal")) {
                win.css("z-index", 3);
            }

            $("iframe").css("pointer-events", "none");
        };
        o.onDragStop = function () {

            draggedWindow = undefined;
            win = $(this);
            if (!win.hasClass("modal"))
                win.css("z-index", 2);

            $("iframe").css("pointer-events", "auto");

        };
        o.onWindowDestroy = function (win) {
            //that.removeFromTaskBar(win);
            //that.removeFromArray(win);
            Desktop.removeFromTaskBar($(win));
        };
        o.onWindowCreate = function (win) {
            //$("#loading").css("display", "block");

            $(".window").css("z-index", 1);
            $(win).css("z-index", 3);
            //$(win).find(".btn-min").attr("title", "Küçült");
            //$(win).find(".btn-max").attr("title", "Büyüt");
            //$(win).find(".btn-close").attr("title", "Kapat");
        };
        o.onResizeStart = function (e) {
            $("iframe").css("pointer-events", "none");
        }
        o.onResizeStop = function (e) {
            $("iframe").css("pointer-events", "auto");
        }

        var w = $("<div>").appendTo($(this.options.windowArea));
        var wnd = w.window(o).data("window");

        var win = wnd.win;
        var shift = Metro.utils.objectLength(this.wins) * 16;

        if (wnd.options.place === "auto" && wnd.options.top === "auto" && wnd.options.left === "auto") {
            win.css({
                top: shift,
                left: shift
            });
        }
        this.wins[win.attr("id")] = wnd;
        this.addToTaskBar(wnd);

        return wnd;
    }
};

Desktop.setup();

var w_icons = [
    'rocket', 'apps', 'cog', 'anchor'
];
var w_titles = [
    'rocket', 'apps', 'cog', 'anchor'
];

var createdWindowsArray = [];

function searchForValueInWindowArray(array, value) {
    if (array.constructor !== Array)
        return false;

    for (var i = 0; i < array.length; i++) {
        if (value === array[i])
            return true;
    }
    return false;
}

function createWindowManuel(url,id, width, height, title) {
    //var index = $.random(0, 3);
    //var content = "<iframe src='" + url + "' style='width:100%; height:100%' allowfullscreen></iframe>";
    //var content = '<iframe id="iframe-test" style="width: 100%; height:100%;" src="' +url+ '" onload=oniframeload(this)></iframe>';
    //console.log("dfgdf + " + $(ths).data('url'));
    //data - pagewidth="1316" data - pageheight="600" data - id="tezgahtar_listesi"
    //data - url="" onclick = "createWindow(this)" data - title="Listesi"
    //var id = $(ths).data('id');
    //var width = $(ths).data('pagewidth');
    //var height = $(ths).data('pageheight');
    //var title = $(ths).data('title');
    //var url = $(ths).data('url');

    var content = '';
    content += '<div class="holds-the-iframe">';
    content += '<iframe class="iframe" id="' + id + '"style="width: 100%; height:100%;" src="' + url + '" onload=oniframeload(this)></iframe> </div>';

    if (searchForValueInWindowArray(createdWindowsArray, id))
        return;

    if (id)
        createdWindowsArray.push(id);

    var w = Desktop.createWindow({
        resizeable: true,
        draggable: true,
        width: width,
        height: height,
        icon: "<span class='mif-apps'></span>",
        title: title,
        content: content
    }); 
}

function createWindow(ths) {
    //var index = $.random(0, 3);
    //var content = "<iframe src='" + url + "' style='width:100%; height:100%' allowfullscreen></iframe>";
    //var content = '<iframe id="iframe-test" style="width: 100%; height:100%;" src="' +url+ '" onload=oniframeload(this)></iframe>';
    console.log("dfgdf + " + $(ths).data('url'));
    //data - pagewidth="1316" data - pageheight="600" data - id="tezgahtar_listesi"
    //data - url="" onclick = "createWindow(this)" data - title="Listesi"
    var id = $(ths).data('id');
    var width = $(ths).data('pagewidth');
    var height = $(ths).data('pageheight');
    var title = $(ths).data('title');
    var url = $(ths).data('url');

    var content = '';
    content += '<div class="holds-the-iframe">';
    content += '<iframe class="iframe" id="' + id + '"style="width: 100%; height:100%;" src="' + url + '" onload=oniframeload(this)></iframe> </div>';

    if (searchForValueInWindowArray(createdWindowsArray, id))
        return;

    if (id)
        createdWindowsArray.push(id);

    var w = Desktop.createWindow({
        resizeable: true,
        draggable: true,
        width: width,
        height: height,
        icon: "<span class='mif-apps'></span>",
        title: title,
        content: content
    }); 
}

$(document).on("click", ".window-caption .btn-close", function () {
    console.log("close ");

    var id = $(this).parent().parent().parent().find("iframe").attr("id");

    createdWindowsArray = jQuery.grep(createdWindowsArray, function (value) {
        return value != id;
    });
});
 


function createWindowWithCustomButtons() {
    var index = $.random(0, 3);
    var customButtons = [
        {
            html: "<span class='mif-rocket'></span>",
            cls: "sys-button",
            onclick: "alert('You press rocket button')"
        },
        {
            html: "<span class='mif-user'></span>",
            cls: "alert",
            onclick: "alert('You press user button')"
        },
        {
            html: "<span class='mif-cog'></span>",
            cls: "warning",
            onclick: "alert('You press cog button')"
        }
    ];
    Desktop.createWindow({
        resizeable: true,
        draggable: true,
        customButtons: customButtons,
        width: 360,
        icon: "<span class='mif-" + w_icons[index] + "'></span>",
        title: w_titles[index],
        content: "<div class='p-2'>This is desktop demo created with Metro 4 Components Library.<br><br>This window has a custom buttons in caption.</div>"
    });
}

function createWindowModal() {
    Desktop.createWindow({
        resizeable: false,
        draggable: true,
        width: 300,
        icon: "<span class='mif-cogs'></span>",
        title: "Modal window",
        content: "<div class='p-2'>This is desktop demo created with Metro 4 Components Library</div>",
        overlay: true,
        //overlayColor: "transparent",
        modal: true,
        place: "center",
        onShow: function (win) {
            win = $(win);
            win.addClass("ani-swoopInTop");
            setTimeout(function () {
                $(win).removeClass("ani-swoopInTop");
            }, 1000);
        },
        onClose: function (win) {
            win = $(win);
            win.addClass("ani-swoopOutTop");
        }
    });
}

function createWindowYoutube() {
    Desktop.createWindow({
        resizeable: true,
        draggable: true,
        width: 500,
        icon: "<span class='mif-youtube'></span>",
        title: "Youtube video",
        content: "https://youtu.be/Qz6XNSB0F3E",
        clsContent: "bg-dark"
    });
}

function openCharm() {
    var charm = $("#charm").data("charms");
    charm.toggle();
}

//$(".window-area").on("click", function () {
//    Metro.charms.close("#charm");
//});

$(".charm-tile").on("click", function () {
    $(this).toggleClass("active");
});

function openModal(data, id) {

    var modal = $("#modal-show");
    if (id)
        modal = $("#" + id);
    modal.html(data);
    modal.css("display", "block");

}

function openModalChild(data, id) {

    var modal = $("#modal-show-child");
    if (id)
        modal = $("#" + id);
    modal.html(data);
    modal.css("display", "block");

}

function hideModal() {

    $(".modal-editor").css("display", "none");
}

function hideModalChild() {

    $("#modal-show-child").css("display", "none");
}

$(".open_menu").on("click", function() {
    $('.content-holder').css("display", "block");
    var src = ($('#menuimg').attr('src') === '/Content/images/slide-down-48.png')
        ? '/Content/images/slide-up-48.png'
        : '/Content/images/slide-up-48.png';
    $('#menuimg').attr('src', src);

});

$(".staticping").on("click", function() {
    $('.content-holder').toggle();
    var src = ($('#menuimg').attr('src') === '/Content/images/slide-up-48.png')
        ? '/Content/images/slide-down-48.png'
        : '/Content/images/slide-up-48.png';
    $('#menuimg').attr('src', src);

    if ($('.content-holder').hasClass("pinging")) {
        $('.content-holder').removeClass("pinging");
    } else $('.content-holder').addClass("pinging");

});

$(".content-holder").mouseleave(function () {

    if ($('.content-holder').hasClass("pinging"))
        return;

    if ($('.content-holder').is(':visible')) {
        $('.content-holder').hide();
    }
});

function showMenu() {

    if ($('.content-holder').hasClass("pinging"))
        return;

    $('.content-holder').toggle();
    var src = ($('#menuimg').attr('src') === '/Content/images/slide-up-48.png')
        ? '/Content/images/slide-down-48.png'
        : '/Content/images/slide-up-48.png';
    $('#menuimg').attr('src', src);
}


$(document).on('change', '#home_index_sube_id', function () {
    console.log("changeas.");
    $.ajax({
        url: "/home/index",
        type: "POST",
        data: {
            subeId: $("#home_index_sube_id").val()
        },
        success: function (data) {
            $(".desktop").html(data);
        },
        error: function (data) {

        }
    });
});  

$(document).ready(function () {
    $(".holds-the-iframe").css("display", "none");
    $('.iframe').on('load', function () {
        $(".holds-the-iframe").css("display", "none");
       /* $('.iframe-loading').hide();*/
    });
});

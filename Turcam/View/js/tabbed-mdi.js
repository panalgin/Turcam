// name should be connections-content alike
function selectPage(name) {
    $("div.tab-content").hide();
    $("div.tab").removeClass("active");
    $("div#" + name).show();

    setTimeout(function () {
        $("div#" + name).attr("tabindex", "-1").focus();
        $("div.tab").removeClass("active");
    }, 5);
}

function removePage(id) {
    $("div#" + id).remove(); //delete content

    var tabid = id.replace("content", "tab");
    var closest = $("div#" + tabid).prev();

    if (!(closest.length > 0))
        closest = $("div#" + tabid).next();

    $("div#" + tabid).remove(); //delete tab

    if (closest.length > 0) {
        var cid = closest.attr("id");
        cid = cid.replace("tab", "content");
        
        selectPage(cid);
    }
}

function getPage(name) {
    if ($("div#" + name + "-content").length > 0) {
        selectPage(name + "-content");
    }
    else {
        switch (name) {
            case "connections": $("div.tabs").append("<div class=\"tab\" id=\"" + name + "-tab\">Bağlantı <img src=\"img/close.png\" /></div>"); break;
            case "start": $("div.tabs").append("<div class=\"tab\" id=\"" + name + "-tab\">Başlangıç <img src=\"img/close.png\" /></div>"); break;
            case "drillbits": $("div.tabs").append("<div class=\"tab\" id=\"" + name + "-tab\">Matkap Uçları <img src=\"img/close.png\" /></div>"); break;
        }

        $.get("inc/" + name + ".html", function (data) {
            $("div#content").prepend(data);
        });

        selectPage(name + "-content");
    }
}

function refreshPage(name) {
    if ($("div#" + name + "-content").length > 0) {
        $.get("inc/" + name + ".html", function (data) {
            $("div#" + name + "-content").html($(data).html());
        });

        selectPage(name + "-content");
    }
}

$(document).on("ready", function (e) {
    $("div.tabs").on("mousedown", "div.tab", function (e) {
        $("div.tab-content").off("focusout"); // disable focusout

        var id = $(this).attr("id").replace("tab", "content");

        selectPage(id);

        $("div.tab").removeClass("active");
        e.preventDefault();
    });

    $("div#content").on("focusin", "div.tab-content", function (e) {
        var id = $(this).attr("id").replace("content", "tab");

        $("div#" + id).removeClass("active").addClass("active-focused");
        var hold = $(this).parent();
        $(hold).removeClass("active").addClass("active-focused");

        $(hold).on("focusout", "#" + id.replace("tab", "content"), OnFocusOutContent);
    });

    function OnFocusOutContent() {
        $("div.tab").removeClass("active"); //remove all for the gods sake

        var id = $(this).attr("id").replace("content", "tab");

        $("div#" + id).removeClass("active-focused").addClass("active");
        var hold = $(this).parent();

        if ($(this).is(":visible")) { // if content is visible set tab just active
            
            $(hold).removeClass("active-focused").add("active");
        }
    }

    $("div.tabs").on("click", "img", function (e) {
        var id = $(this).parent().attr("id").replace("tab", "content");

        removePage(id);
    });
});
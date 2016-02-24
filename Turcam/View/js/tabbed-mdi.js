// name should be connections-content alike
function selectPage(name) {
    $("div.tab-content").hide();

    setTimeout(function () {
        $("div#" + name).show().attr("tabindex", "-1").focus();
    }, 20);

    $("div.tab").removeClass("active");
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
        }

        $.get("inc/" + name + ".html", function (data) {
            $("div#content").prepend(data);
        });

        selectPage(name + "-content");
    }
}

$(document).on("ready", function (e) {
    $("div.tabs").on("mousedown", "div.tab", function (e) {
        $("div#content").off("focusout", "div.tab-content", OnFocusOutContent); // disable focusout

        var id = $(this).attr("id").replace("tab", "content");

        selectPage(id);        

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
        var id = $(this).attr("id").replace("content", "tab");

        $("div#" + id).removeClass("active-focused").addClass("active");
        var hold = $(this).parent();
        $(hold).removeClass("active-focused").add("active");
    }

    $("div.tabs").on("click", "img", function (e) {
        var id = $(this).parent().attr("id").replace("tab", "content");

        removePage(id);
    });
});
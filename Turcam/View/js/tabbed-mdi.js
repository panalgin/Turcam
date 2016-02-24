$(document).on("ready", function (e) {
    $("div.tab").on("mousedown", function (e) {
        $("div.content").off("focusout", "div.tab-content", OnFocusOutContent); // disable focusout
        $("div.tab-content").hide(); // hide all contents

        var id = $(this).attr("id").replace("tab", "content");

        $("div#" + id).show().focus();
        $("div.tab").removeClass("active");
        e.preventDefault();
    });

    $("div.tab-content").on("focusin", function (e) {
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

    $("div.tab img").on("click", function (e) {
        alert("haha");
        var id = $(this).parent().attr("id").replace("tab", "content");

        $("div#" + id).remove();
        $(this).parent().remove();
    });
});
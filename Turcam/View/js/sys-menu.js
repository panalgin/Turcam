$(document).on("ready", function () {
    $("li#connections-item").on("click", function (e) {
        getPage("connections");
    });
    $("li#exit-item").on("click", function (e) {
        windowsApp.exit();
    });
});
$(document).on("ready", function () {
    $("li#connections-item").on("click", function (e) {
        getPage("connections");
    });

    $("li#drillbits-item").on("click", function (e) {
        getPage("drillbits");
    });

    $("li#exit-item").on("click", function (e) {
        windowsApp.exit();
    });

    $("li#cam-item").on("click", function (e) {
        getPage("cam");
    });
});
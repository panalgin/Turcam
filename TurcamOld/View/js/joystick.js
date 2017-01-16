$(document).on("ready", function (e) {
    $("div#right-bore-btn").on("click", function (e) {
        windowsApp.jogStart("A:Right");
        e.preventDefault();
    });

    $("div#left-bore-btn").on("click", function (e) {
        windowsApp.jogStart("A:Left");
        e.preventDefault();
    });

    $("div#right-plasma-btn").on("click", function (e) {
        windowsApp.jogStart("X:Right");
        e.preventDefault();
    });

    $("div#left-plasma-btn").on("click", function (e) {
        windowsApp.jogStart("X:Left");
        e.preventDefault();
    });

    $("div#forward-btn").on("click", function (e) {
        windowsApp.jogStart("Y:Forward");
        e.preventDefault();
    });

    $("div#backward-btn").on("click", function (e) {
        windowsApp.jogStart("Y:Backward");
        e.preventDefault();
    });

    $("div#up-bore-btn").on("mousedown", function (e) {
        windowsApp.jogStart("B:Up");
        e.preventDefault();
    }).on("mouseup", function (e) {
        windowsApp.jogEnd();
        e.preventDefault();
    });

    $("div#down-bore-btn").on("mousedown", function (e) {
        windowsApp.jogStart("B:Down");
        e.preventDefault();
    }).on("mouseup", function (e) {
        windowsApp.jogEnd();
        e.preventDefault();
    });

    $("div#up-plasma-btn").on("mousedown", function (e) {
        windowsApp.jogStart("Z:Up");
        e.preventDefault();
    }).on("mouseup", function (e) {
        windowsApp.jogEnd();
        e.preventDefault();
    });

    $("div#down-plasma-btn").on("mousedown", function (e) {
        windowsApp.jogStart("Z:Down");
        e.preventDefault();
    }).on("mouseup", function (e) {
        windowsApp.jogEnd();
        e.preventDefault();
    });
});
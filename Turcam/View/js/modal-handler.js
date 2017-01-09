function createModal(header, path, data, type) {
    var container = "<div class='modal'><div class='header'></div><div class='content'></div></div>";
    container = $("<div/>").html(container).contents();
    
    $.get(path, function (html) {
        container.children("div.header").append(header);
        container.children("div.content").append(html);

        if (data)
            container.find("input#actual-item").data("drill", data);

        container.modal({ closeClass: 'modal-close', closeText: '' });
    });

    if (type !== undefined) {
        console.log("4üncü parametreye girdi;")
        if (type === "Question") {
            console.log("Questiona girdi.");
            container.addClass("question");
        }
        else if (type === "Error") {
            container.addClass("error");
        }
    }
}
function createModal(header, path, data, type) {

    var container = "<div class='modal'><div class='header'></div><div class='content'></div></div>";
    container = $("<div/>").html(container).contents();

    if (type === undefined) {
        $.get(path, function (html) {
            container.children("div.header").append(header);
            container.children("div.content").append(html);

            if (data)
                container.find("input#actual-item").data("drill", data);

            container.modal({ closeClass: 'modal-close', closeText: '' });
        });
    }

    if (type !== undefined) {
        if (type === "Question") {

        }
        else if (type === "Error") {

        }
    }
}
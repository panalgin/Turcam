function createModal(header, path, type) {

    var container = "<div class='modal'><div class='header'></div><div class='content'></div></div>";
    container = $("<div/>").html(container).contents();

    if (type == undefined) {
        $.get(path, function (html) {
            container.children("div.header").append(header);
            container.children("div.content").append(html);
            container.modal({ closeClass: 'modal-close', closeText: '' });
        });
    }

    if (type !== undefined) {
        if (type == "Question") {

        }
        else if (type == "Error") {

        }
    }
}
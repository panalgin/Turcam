var btn = $("input#connect-button");

//we are disconnected so display connect

btn.removeClass("disconnect");
btn.addClass("connect");
btn.val("Bağlan");
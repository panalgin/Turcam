var btn = $("input#connect-button");

//we are connected so display disconnect

btn.removeClass("connect");
btn.addClass("disconnect");
btn.val("Çıkış");

//view connect board at top right corner
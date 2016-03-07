var btn = $("input#connect-button");

//we are connected so display disconnect

btn.removeClass("connect");
btn.addClass("disconnect");
btn.val("Çıkış");

$("div.top div.connection").show();
$("div.top div.connection span#port").text("{0}");

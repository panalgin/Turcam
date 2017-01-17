var area = $("textarea#signal-area");
var value = area.val();

area.val("Received: {0}\n" + value);